using MauiApp1.Services.AppHelper;

namespace MauiApp1;

public partial class CoinsPage : ContentPage
{
    public CoinsPage()
    {
        InitializeComponent();
    }
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
}