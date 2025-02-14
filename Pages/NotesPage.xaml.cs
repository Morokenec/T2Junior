using MauiApp1.Services.AppHelper;

namespace MauiApp1;

public partial class NotesPage : ContentPage
{
    public bool Redirected { get; set; } = BackNavigationState.IsDirectAccess;
    public NotesPage()
    {
        InitializeComponent();
    }
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
}