using System.Collections.ObjectModel;
using T2JuniorMobile.ViewModel;

namespace T2JuniorMobile.View.Pages;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
	{
        BindingContext = new RegisterViewModel();
        InitializeComponent();
    }
}