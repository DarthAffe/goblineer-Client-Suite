using System;
using System.Timers;
using System.Windows;
using WyrezPriceClient.Misc;
using WyrezPriceClient.Services;
using WyrezPriceClient.UI;

namespace WyrezPriceClient
{
    public class ApplicationManager
    {
        #region Properties & Fields

        public static ApplicationManager Instance { get; } = new ApplicationManager();

        public UpdateService UpdateService { get; } = new UpdateService();

        private static Timer _updateTimer;
        private ConfigurationWindow _configurationWindow;

        #endregion

        #region Commands

        private ActionCommand _openConfiguration;
        public ActionCommand OpenConfigurationCommand => _openConfiguration ?? (_openConfiguration = new ActionCommand(OpenConfiguration));

        private ActionCommand _exitCommand;
        public ActionCommand ExitCommand => _exitCommand ?? (_exitCommand = new ActionCommand(Exit));

        #endregion

        #region Methods

        public void Initialize()
        {
            _updateTimer = new Timer(new TimeSpan(0, 1, 0).TotalMilliseconds) { AutoReset = true };
            _updateTimer.Elapsed += (sender, args) => UpdateService.Update();
            _updateTimer.Start();
            UpdateService.Update();
        }

        private void OpenConfiguration()
        {
            if (_configurationWindow == null) _configurationWindow = new ConfigurationWindow();
            _configurationWindow.Show();
        }

        private void Exit()
        {
            _updateTimer.Stop();
            Application.Current.Shutdown();
        }

        #endregion
    }
}
