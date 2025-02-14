using MauiApp1.Services.AppHelper;

namespace MauiApp1;

public partial class RatingPage : ContentPage
{
    public RatingPage()
    {
        InitializeComponent();
    }
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
}