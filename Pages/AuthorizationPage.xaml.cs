using MauiApp1.DataModel;
using MauiApp1.Services.UseCase;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Profile;
using System.Text.RegularExpressions;

namespace MauiApp1
{
    public partial class AuthorizationPage : ContentPage
    {
        private bool _isVisible = false;

        private readonly AuthorizationViewModel _viewModel;

        public AuthorizationPage(AuthorizationViewModel userViewModel)
        {
            _viewModel = userViewModel;
            BindingContext = _viewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();           
        }

        private async void OnForgotPasswordTapped(object sender, EventArgs e)
        {
        }

        private void OnPasswordVisibilityClicked(object sender, EventArgs e)
        {
            _isVisible = !_isVisible;
            PswdEntry.IsPassword = !_isVisible;
        }

        private void OnEnterClicked(object sender, EventArgs e)
        {
            string pattern = @"^(?=.*[a-zа-я])(?=.*[A-ZА-Я])(?=.*[!@#$%^=&*(),.?"":{}|<>]).{8,}$";
            string phonePattern = @"^(?:\+7|8)\d{10}$";

            if (string.IsNullOrWhiteSpace(EmailEntry?.Text?.Trim()) || string.IsNullOrWhiteSpace(PswdEntry?.Text?.Trim()))
            {
                ValidationLabel.Text = "Поля не могут оставаться пустыми!";
                ValidationLabel.IsVisible = true;

            }
            else
            { 
                ValidationLabel.IsVisible = true;
                ValidationLabel.TextColor = Colors.Green;
                _viewModel.LoginAsync();
            }

        }

        protected override bool OnBackButtonPressed()
        {
            return true; // Предотвращает навигацию назад
        }
    }

}
