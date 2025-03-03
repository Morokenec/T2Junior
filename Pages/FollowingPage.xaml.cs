using MauiApp1.Services.AppHelper;
using MauiApp1.ViewModels.Profile;

namespace MauiApp1;

/// <summary>
/// Страница подписок пользователя в приложении.
/// </summary>
public partial class FollowingPage : ContentPage
{
    string portProfileName;
    private UserProfileViewModel _userViewModel;

    /// <summary>
    /// Конструктор класса FollowingPage.
    /// </summary>
    public FollowingPage()
    {
        InitializeComponent();
        ElementsProperties();
        ResizeFunction();
        GetLabelTextFromFrame();
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
    /// Метод для получения текста метки из фрейма.
    /// </summary>
    private void GetLabelTextFromFrame()
    {
        foreach (var childOfLayout in ProfilesLayout.Children)
        {
            if (childOfLayout is StackLayout stackLayout)
            {
                foreach (var clickedFrame in stackLayout.Children)
                {
                    if (clickedFrame is Frame frame)
                    {
                        var tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += async (s, e) => await OnProfileFrameTapped(frame);
                        frame.GestureRecognizers.Add(tapGestureRecognizer);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Обработчик события нажатия на фрейм профиля.
    /// </summary>
    /// <param name="choosedFrame">Выбранный фрейм.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    private async Task OnProfileFrameTapped(Frame choosedFrame)
    {
        var hsl = (HorizontalStackLayout)choosedFrame.Content;

        var fullNameLabel = (Label)hsl.Children[1];

        var profilePage = new ProfilePage(_userViewModel);

        await Navigation.PushAsync(profilePage);
    }

    /// <summary>
    /// Установка свойств элементов интерфейса.
    /// </summary>
    private void ElementsProperties()
    {
        SubFrame.WidthRequest = 343;
        SubFrame.HeightRequest = 40;
        SubFrame.Margin = new Thickness(20, 16);
        SubFrame.Padding = new Thickness(0, 8);

        BackFrame.WidthRequest = 24;
        BackFrame.HeightRequest = 24;
        BackFrame.Padding = new Thickness(0);

        FollowTextFrame.WidthRequest = 319;
        FollowTextFrame.HeightRequest = 24;
        FollowTextFrame.Padding = new Thickness(0, 0, 24, 0);

        ProfilesLayout.Spacing = 4;

        ProfContainerFrame.WidthRequest = 343;
        ProfContainerFrame.HeightRequest = 312;
        ProfContainerFrame.Padding = new Thickness(8, 10);
    }

    /// <summary>
    /// Метод для выбора профиля.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private void ProfileChoosingFactor(object sender, EventArgs e)
    {
        if (sender is Frame clickedFrame)
        {
            foreach (var childOfFrame in clickedFrame.Children)
            {
                if (childOfFrame is HorizontalStackLayout hsl)
                {
                    hsl.Spacing = 10;

                    foreach (var child in hsl.Children)
                    {
                        if (child is Label label)
                        {
                            portProfileName = label.Text;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Метод для изменения размеров элементов интерфейса.
    /// </summary>
    private void ResizeFunction()
    {
        foreach (var childOfLayout in ProfilesLayout.Children)
        {
            if (childOfLayout is StackLayout stackLayout)
            {
                foreach (var childFrame in stackLayout.Children)
                {
                    if (childFrame is Frame frame)
                    {
                        frame.WidthRequest = 327;
                        frame.HeightRequest = 56;
                        frame.Padding = new Thickness(8);

                        foreach (var childOfFrame in frame.Children)
                        {
                            if (childOfFrame is HorizontalStackLayout hsl)
                            {
                                hsl.Spacing = 10;

                                foreach (var child in hsl.Children)
                                {

                                    if (child is Label label)
                                    {
                                        label.WidthRequest = 231;
                                        label.HeightRequest = 24;
                                        label.FontSize = 15;
                                    }

                                    if (child is Image image)
                                    {
                                        if (image.Source is FileImageSource fileImageSource)
                                        {
                                            if (fileImageSource.File == "redirect.svg")
                                            {
                                                image.WidthRequest = 24;
                                                image.HeightRequest = 24;
                                            }
                                            else if (fileImageSource.File == "profile_placeholder.svg")
                                            {
                                                image.WidthRequest = 36;
                                                image.HeightRequest = 36;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (childFrame is BoxView boxView)
                    {
                        boxView.HeightRequest = 2;
                    }
                }
            }
        }
    }
}