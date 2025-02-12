using MauiApp1.Services;

namespace MauiApp1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //this.Navigating += OnShellNavigating;
        }

        //private void OnShellNavigating(object sender, ShellNavigatingEventArgs e)
        //{
        //    if (e.Target.Location.OriginalString.Contains("NotesPage") ||
        //    e.Target.Location.OriginalString.Contains("MessagesPage") ||
        //    e.Target.Location.OriginalString.Contains("ClubsPage") ||
        //    e.Target.Location.OriginalString.Contains("ProfilePage"))
        //    {
        //        BackNavigationState.IsDirectAccess = true;
        //    }
        //}
    }
}
