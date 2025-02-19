using MauiApp1.DataModel;
using MauiApp1.Pages;
using MauiApp1.Services;
using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class NotesPage : ContentPage
{
    public bool HolderIsVisible { get; set; } = true; //добавить условия
    public bool Redirected { get; set; } = BackNavigationState.IsDirectAccess;
    public NotesPage()
    {
        InitializeComponent();
        BindingContext = new NoteViewModel();
    }
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
    private void OnSearch(object sender, EventArgs e)
    {
        var noteContext = (NoteViewModel)BindingContext;
        noteContext.FilterNotes();
    }
    private void OnNoteTapped(object sender, EventArgs e)
    {
        var tappedNote = (sender as StackLayout)?.BindingContext as Note;
        if (tappedNote != null)
        {
            DetailedNotePage.SelectedNoteId = tappedNote.IdNote;
            Application.Current.MainPage.Navigation.PushAsync(new DetailedNotePage());
        }
    }
}