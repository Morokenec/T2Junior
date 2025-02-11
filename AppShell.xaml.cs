using MauiApp1.Services;

namespace MauiApp1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            this.Navigating += OnShellNavigating;
        }

        private void OnShellNavigating(object sender, ShellNavigatingEventArgs e)
        {
            BackNavigationState.IsDirectAccess = true;
        }
    }
}
