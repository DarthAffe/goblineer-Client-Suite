using System;
using System.Collections.ObjectModel;
using WyrezPriceClient.Misc;

namespace WyrezPriceClient.Services
{
    public class LoggingService : AbstractBindable
    {
        #region Properties & Fields

        public ObservableCollection<string> Log { get; } = new ObservableCollection<string>();

        #endregion

        #region Methods

        public void AddLogEntry(string message) => Log.Add($"{DateTime.Now:dd.MM.yyy HH:mm:ss}: {message}");

        #endregion
    }

    public static class Logger
    {
        #region Properties & Fields

        public static LoggingService LoggingService { get; } = new LoggingService();

        #endregion

        #region Methods

        public static void Log(string message) => LoggingService.AddLogEntry(message);

        #endregion
    }
}
