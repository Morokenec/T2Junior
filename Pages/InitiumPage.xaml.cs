using MauiApp1.Services;

namespace MauiApp1;

public partial class InitiumPage : ContentPage
{
	public InitiumPage()
	{
		InitializeComponent();
	}
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
}