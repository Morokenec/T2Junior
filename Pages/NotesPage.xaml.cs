using MauiApp1.DataModel;
using MauiApp1.Services;
using MauiApp1.Services.AppHelper;
using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class NotesPage : ContentPage
{
    public bool HolderIsVisible { get; set; } = true; 
    public bool Redirected { get; set; } = BackNavigationState.IsDirectAccess;
    private readonly NoteViewModel _viewModel;

    public NotesPage(NoteViewModel noteViewModel)
    {
        InitializeComponent();
        _viewModel = noteViewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadNotes();
    }

    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }

    private void OnNoteTapped(object sender, EventArgs e)
    {
        var tappedNote = (sender as StackLayout)?.BindingContext as Note;
        if (tappedNote != null)
        {
            //DetailedNotePage.SelectedNoteId = tappedNote.IdNote;
            //Application.Current.MainPage.Navigation.PushAsync(new DetailedNotePage());
        }
    }
}