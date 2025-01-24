using T2JuniorMobile.ViewModel;

namespace T2JuniorMobile.View.Pages;

public partial class ConfimPage : ContentPage
{
	public ConfimPage()
	{
		InitializeComponent();
		BindingContext = new ConfirmViewModel();
	}
}