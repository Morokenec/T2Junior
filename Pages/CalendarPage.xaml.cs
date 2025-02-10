namespace MauiApp1;

public partial class CalendarPage : ContentPage
{
	public CalendarPage()
	{
		InitializeComponent();
	}
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
}