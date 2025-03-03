using MauiApp1.Models;
using MauiApp1.Models.MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Services.AppHelper;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.NoteViewModels;
using Microsoft.Maui.Controls;

namespace MauiApp1;

/// <summary>
/// Страница сообщений в приложении.
/// </summary>
public partial class MessagesPage : ContentPage
{
    private Dictionary<object, int> clickCounts = new Dictionary<object, int>();
    private bool bothFramesClickedOnce = false;

    /// <summary>
    /// Флаг, указывающий, был ли выполнен редирект.
    /// </summary>
    public bool Redirected { get; set; } = BackNavigationState.IsDirectAccess;

    /// <summary>
    /// Конструктор класса MessagesPage.
    /// </summary>
    public MessagesPage()
    {
        InitializeComponent();
        BindingContext = new MessageViewModel();
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
        var clubContext = (MessageViewModel)BindingContext;
        clubContext.FilterChats();
    }


    /// <summary>
    /// Обработчик события нажатия на чат.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private async void OnChatTapped(object sender, EventArgs e)
    {
        var tappedChat = (sender as StackLayout)?.BindingContext as Message;
        if (tappedChat != null)
        {
            //ChatPage.SelectedChatId = tappedChat.IdChat;
            //await Application.Current.MainPage.Navigation.PushAsync(new ChatPage());
        }
    }

    /// <summary>
    /// Обработчик события нажатия на тип чата.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
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

    /// <summary>
    /// Получает текст метки из фрейма.
    /// </summary>
    /// <param name="frameOfType">Фрейм, из которого извлекается текст метки.</param>
    /// <returns>Текст метки.</returns>
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

    /// <summary>
    /// Находит фрейм внутри StackLayout.
    /// </summary>
    /// <param name="stackLayout">StackLayout, в котором выполняется поиск.</param>
    /// <returns>Найденный фрейм.</returns>
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