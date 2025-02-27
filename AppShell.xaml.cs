using MauiApp1.Services;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase;
using MauiApp1.ViewModels;

namespace MauiApp1
{
    public partial class AppShell : Shell
    {
        public bool IsUserAuthorized { get; } = false;

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("LoginPage", typeof(AuthorizationPage));
            Routing.RegisterRoute("NotesPage", typeof(NotesPage));
            Routing.RegisterRoute("Main", typeof(AppShell));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!IsUserAuthorized)
            {
                await Navigation.PushModalAsync(new AuthorizationPage(new AuthorizationViewModel(new ProfileService(new HttpClient(), new JsonDeserializerService()))));
            }
        }
    }

}
