using MauiApp1.Services.AppHelper;

namespace MauiApp1;

public partial class RulesPage : ContentPage
{
	public RulesPage()
	{
		InitializeComponent();
	}
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
}