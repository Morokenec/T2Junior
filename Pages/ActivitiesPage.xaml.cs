using MauiApp1.Services;

namespace MauiApp1;

public partial class ActivitiesPage : ContentPage
{
    public ActivitiesPage()
    {
        InitializeComponent();
    }
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
}