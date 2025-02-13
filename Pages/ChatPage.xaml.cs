using MauiApp1.Models;

namespace MauiApp1.Pages;

public partial class ChatPage : ContentPage
{
    public static int SelectedChatId { get; set; }
    public ChatPage()
	{
		InitializeComponent();
	}
}