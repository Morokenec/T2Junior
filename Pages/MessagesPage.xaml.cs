using MauiApp1.Models;
using MauiApp1.Pages;
using MauiApp1.Services;
using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class MessagesPage : ContentPage
{
    public bool Redirected { get; set; } = BackNavigationState.IsDirectAccess;
    public MessagesPage()
    {
        InitializeComponent();
        BindingContext = new MessageViewModel();
    }
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }

    private void OnSearch(object sender, EventArgs e)
    {
        var clubContext = (MessageViewModel)BindingContext;
        clubContext.FilterChats();
    }

    private async void OnChatTapped(object sender, EventArgs e)
    {
        var tappedChat = (sender as StackLayout)?.BindingContext as Message;
        if (tappedChat != null)
        {
            ChatPage.SelectedChatId = tappedChat.IdChat;
            await Application.Current.MainPage.Navigation.PushAsync(new ChatPage());
        }
    }
}