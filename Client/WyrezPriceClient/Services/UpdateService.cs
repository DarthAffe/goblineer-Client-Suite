using System;
using System.IO;
using System.Net;
using LsonLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WyrezPriceClient.Services
{
    public class UpdateService
    {
        #region Properties & Fields

        private readonly string _timestampUrl;
        private readonly string _dataUrl;
        private readonly string _addonDirectory;

        private int _lastUpdate = 0;

        #endregion

        #region Events

        public EventHandler DataUpdated;

        #endregion

        #region Constructors

        public UpdateService()
        {
            _timestampUrl = Properties.Settings.Default.timestampURL;
            _dataUrl = Properties.Settings.Default.dataURL;
            string wowPath = Properties.Settings.Default.wowPath;
            _addonDirectory = Path.Combine(wowPath, "Interface", "AddOns", "WyrezPrice");
        }

        #endregion

        #region Methods

        public void Update(bool force = false)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    int timestamp = int.Parse(webClient.DownloadString(_timestampUrl));
                    if ((timestamp <= _lastUpdate) && !force) return;

                    JArray auctionData = (JArray)JsonConvert.DeserializeObject(webClient.DownloadString(_dataUrl));
                    LsonDict luaData = new LsonDict();
                    foreach (JToken auction in auctionData)
                    {
                        LsonDict luaAuctionData = new LsonDict
                                                  {
                                                      {"marketvalue", auction["marketvalue"].Value<string>()},
                                                      {"quantity", auction["quantity"].Value<string>()},
                                                      {"MIN", auction["MIN"].Value<string>()}
                                                  };
                        luaData.Add(auction["item"].Value<string>(), luaAuctionData);
                    }

                    using (FileStream fs = File.Create(Path.Combine(_addonDirectory, "data.lua")))
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.Write("goblineer_data = ");
                        writer.Write(luaData.ToStringIndented());
                    }

                    try { DataUpdated?.Invoke(this, new EventArgs()); } catch { /* catch'em all */ }

                    Logger.Log("Data updated!");
                    _lastUpdate = timestamp;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("[Exception] " + ex.Message);
            }
        }

        #endregion
    }
}
