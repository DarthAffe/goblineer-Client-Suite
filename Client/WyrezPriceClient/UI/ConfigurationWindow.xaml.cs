using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WyrezPriceClient.UI
{
    public partial class ConfigurationWindow : Window
    {
        public ConfigurationWindow()
        {
            InitializeComponent();
        }

        private void ListView_OnSourceUpdated(object sender, DataTransferEventArgs e)
        {
            if (!(sender is ListView listView)) return;

            if (VisualTreeHelper.GetChildrenCount(listView) > 0)
            {
                Border border = (Border)VisualTreeHelper.GetChild(listView, 0);
                ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToBottom();
            }
        }
    }
}
