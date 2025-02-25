using MauiApp1.Services;

namespace MauiApp1.Pages;

public partial class DetailedNotePage : ContentPage
{
    public static int SelectedNoteId { get; set; }
    public DetailedNotePage()
	{
		InitializeComponent();
	}
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
}