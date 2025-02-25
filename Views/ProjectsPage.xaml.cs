using MauiApp1.Services;

namespace MauiApp1;

public partial class ProjectsPage : ContentPage
{
    public ProjectsPage()
    {
        InitializeComponent();
    }
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
}