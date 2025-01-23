using T2JuniorMobile.View.Pages;

namespace T2JuniorMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
            Routing.RegisterRoute("ConfimPage", typeof(ConfimPage));
            InitializeComponent();
        }
    }
}
