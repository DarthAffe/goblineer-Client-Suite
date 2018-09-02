using System.Collections.ObjectModel;
using WyrezPriceClient.Misc;
using WyrezPriceClient.Services;

namespace WyrezPriceClient.UI
{
    public class ConfigurationWindowViewModel : AbstractBindable
    {
        #region Properties & Fields

        private readonly UpdateService _updateService;
        public ObservableCollection<string> Log { get; }

        private ActionCommand _updateCommand;
        public ActionCommand UpdateCommand => _updateCommand ?? (_updateCommand = new ActionCommand(ForceUpdate));

        #endregion

        #region Constructors

        public ConfigurationWindowViewModel()
        {
            _updateService = ApplicationManager.Instance.UpdateService;
            Log = Logger.LoggingService.Log;
        }

        #endregion

        #region Methods

        private void ForceUpdate() => _updateService.Update(true);

        #endregion
    }
}
