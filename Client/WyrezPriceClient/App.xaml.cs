using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace WyrezPriceClient
{
    public partial class App : Application
    {
        #region Properties & Fields

        private TaskbarIcon _taskbarIcon;

        #endregion

        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _taskbarIcon = (TaskbarIcon)FindResource("TaskbarIcon");
            _taskbarIcon.DoubleClickCommand = ApplicationManager.Instance.OpenConfigurationCommand;

            ApplicationManager.Instance.UpdateService.DataUpdated += (sender, args) => _taskbarIcon.ShowBalloonTip("Wyrez-Price", "Data updated!", BalloonIcon.Info);
            ApplicationManager.Instance.Initialize();
        }

        #endregion
    }
}
