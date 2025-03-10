using MauiApp1.Services;

namespace MauiApp1.Views;

public partial class TimePeriodPage : ContentPage
{
	public TimePeriodPage()
	{
		InitializeComponent();
	}
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
}