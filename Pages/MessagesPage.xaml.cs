using MauiApp1.Services;

namespace MauiApp1;

public partial class MessagesPage : ContentPage
{
    public bool DirectAccessed { get; set; } = BackNavigationState.IsDirectAccess;
    public MessagesPage()
    {
        InitializeComponent();
    }
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
}