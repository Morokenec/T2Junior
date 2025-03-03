using MauiApp1.DataModel;
using MauiApp1.Services;
using MauiApp1.Services.AppHelper;
using MauiApp1.ViewModels;

namespace MauiApp1;

/// <summary>
/// Страница заметок в приложении.
/// </summary>
public partial class NotesPage : ContentPage
{
    /// <summary>
    /// Флаг, указывающий, видим ли держатель.
    /// </summary>
    public bool HolderIsVisible { get; set; } = true; //�������� ������� (что это???)

    /// <summary>
    /// Флаг, указывающий, был ли выполнен редирект.
    /// </summary>
    public bool Redirected { get; set; } = BackNavigationState.IsDirectAccess;

    /// <summary>
    /// Конструктор класса NotesPage.
    /// </summary>
    public NotesPage()
    {
        InitializeComponent();
        BindingContext = new NoteViewModel();
    }

    /// <summary>
    /// Обработчик события нажатия на кнопку "Назад".
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }

    /// <summary>
    /// Обработчик события поиска.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private void OnSearch(object sender, EventArgs e)
    {
        var noteContext = (NoteViewModel)BindingContext;
        noteContext.FilterNotes();
    }

    /// <summary>
    /// Обработчик события нажатия на заметку.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
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