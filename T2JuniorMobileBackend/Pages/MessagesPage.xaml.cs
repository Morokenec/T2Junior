using MauiApp1.Models;
using MauiApp1.Models.MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Services.AppHelper;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.NoteViewModels;
using Microsoft.Maui.Controls;

namespace MauiApp1;

public partial class MessagesPage : ContentPage
{
    private Dictionary<object, int> clickCounts = new Dictionary<object, int>();

    private bool bothFramesClickedOnce = false;
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
            //ChatPage.SelectedChatId = tappedChat.IdChat;
            //await Application.Current.MainPage.Navigation.PushAsync(new ChatPage());
        }
    }

    private void OnTypeTapped(object sender, EventArgs e)
    {
        var chatContext = (MessageViewModel)BindingContext;
        Frame typeFrame = null;
        if (sender is Frame frame)
        {
            typeFrame = frame;
        }
        else if (sender is StackLayout stackLayout)
        {
            typeFrame = FindAFrame(stackLayout);
        }

        string labelText = GetLabelTextFromFrame(typeFrame);

        if (!clickCounts.ContainsKey(labelText))
        {
            clickCounts[labelText] = 0;
        }

        clickCounts[labelText]++;

        if (labelText == "Всё" || labelText == "Сообщества")
        {
            if (!bothFramesClickedOnce)
            {
                if (clickCounts.Values.All(count => count == 1))
                {
                    typeFrame.BorderColor = Color.FromArgb("#0057A6");
                    bothFramesClickedOnce = true;
                    var types = chatContext.StringToType(labelText);
                    foreach (var type in types)
                    {
                        chatContext.FilterChatsByType(type);
                    }
                }
                else
                {
                    typeFrame.BorderColor = Color.FromArgb("#E0E0E0");
                    bothFramesClickedOnce = true;
                    clickCounts[labelText] = 1;
                    chatContext.FilterChatsByType(Message.TypeName.Сообщества);
                }

            }
            else
            {
                if (clickCounts[labelText] == 2)
                {
                    typeFrame.BorderColor = Color.FromArgb("#E0E0E0");
                    if (bothFramesClickedOnce)
                    {
                        var types = chatContext.GetOppositeType(labelText);
                        foreach (var type in types)
                        {
                            chatContext.FilterChatsByType(type);
                        }
                        bothFramesClickedOnce = false;
                    }
                    else
                    {
                        chatContext.FilterChatsByType(Message.TypeName.Личные);
                    }

                    clickCounts[labelText] = 0;
                }
                else if (clickCounts[labelText] == 1)
                {
                    typeFrame.BorderColor = Color.FromArgb("#0057A6");
                    if (bothFramesClickedOnce)
                    {
                        chatContext.FilterChatsByType(Message.TypeName.Все);
                    }
                    else
                    {
                        var types = chatContext.StringToType(labelText);
                        foreach (var type in types)
                        {
                            chatContext.FilterChatsByType(type);
                        }
                    }
                }
            }
        }

        else if (labelText == "Сообщества")
        {
            if (clickCounts[labelText] == 2)
            {
                typeFrame.BorderColor = Color.FromArgb("#E0E0E0");
                chatContext.FilterChatsByType(Message.TypeName.Все);
                clickCounts[labelText] = 0;
            }
            else if (clickCounts[labelText] == 1)
            {
                typeFrame.BorderColor = Color.FromArgb("#0057A6");
                chatContext.FilterChatsByType(Message.TypeName.Личные);
            }
        }
    }

    private string GetLabelTextFromFrame(Frame frameOfType)
    {
        foreach (var child in frameOfType.Children)
        {
            if (child is StackLayout stackLayoutOfType)
            {
                foreach (var typeChild in stackLayoutOfType.Children)
                {
                    if (typeChild is Frame innerFrame)
                    {
                        foreach (var innerChild in innerFrame.Children)
                        {
                            if (innerChild is StackLayout innerStackLayout)
                            {
                                foreach (var layoutChild in innerStackLayout.Children)
                                {
                                    if (layoutChild is Label label)
                                    {
                                        return label.Text;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return null;
    }

    private Frame FindAFrame(StackLayout stackLayout)
    {
        while (stackLayout != null)
        {
            if (stackLayout.Parent is Frame frame)
            {
                if (frame.Parent is StackLayout frameLayout)
                {
                    if (frameLayout.Parent is Frame typeFrame)
                    {
                        return typeFrame;
                    }
                }
            }
            stackLayout = stackLayout.Parent as StackLayout;
        }
        return null;
    }
}