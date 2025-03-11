using MauiApp1.ViewModels;

namespace MauiApp1.Pages;

public partial class NoteEditorPage : ContentPage
{
	private readonly NoteEditorViewModel _viewmodel;
	public NoteEditorPage(NoteEditorViewModel noteEditorViewModel)
	{
		InitializeComponent();
		_viewmodel = noteEditorViewModel;
		BindingContext = _viewmodel;
	}

    private async void SendNewsButton_Clicked(object sender, EventArgs e)
    {
		await _viewmodel.SendPost();
		await Navigation.PopToRootAsync();
    }

    private async void AddMediaFileButton_Clicked(object sender, EventArgs e)
    {
		await _viewmodel.SetMediaFile();
    }
}