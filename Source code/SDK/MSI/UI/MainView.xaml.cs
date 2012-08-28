namespace Elysium.SDK.MSI.UI
{
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void MainViewClosed(object sender, System.EventArgs e)
        {
            App.Current.Dispatcher.InvokeShutdown();
        }
    }
}
