using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels;

namespace MauiApp1.Pages;

public partial class ChatPage : ContentPage
{
    public bool Redirected { get; set; } = !BackNavigationState.IsDirectAccess;
    public static int SelectedChatId { get; set; }
    public ChatPage()
	{
		InitializeComponent();
        BindingContext = new MessageViewModel();
        LoadDialogDetails();
    }
    private void LoadDialogDetails()
    {
        var dialogViewModel = new MessageViewModel();
        var selectedChat = dialogViewModel.GetChatById(SelectedChatId);
        ((MessageViewModel)BindingContext).SelectedChat = selectedChat;
    }
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
}