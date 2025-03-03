using MauiApp1.DataModel;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase;
using MauiApp1.Services.UseCase.Interface;
using MauiApp1.ViewModels.Profile;
using System.Collections.ObjectModel;

namespace MauiApp1;

/// <summary>
/// Страница профиля пользователя в приложении.
/// </summary>
public partial class ProfilePage : ContentPage
{
    int netStatus = 1;

    private readonly UserProfileViewModel _viewModel;

    /// <summary>
    /// Флаг, указывающий, был ли доступ к странице напрямую.
    /// </summary>
    public bool DirectAccessed { get; set; } = BackNavigationState.IsDirectAccess;

    /// <summary>
    /// Конструктор класса ProfilePage.
    /// </summary>
    /// <param name="userProfileViewModel">Модель представления профиля пользователя.</param>
    public ProfilePage(UserProfileViewModel userProfileViewModel)
    {
        InitializeComponent();
        _viewModel = userProfileViewModel;
        BindingContext = _viewModel;
        NetStatus();
    }

    /// <summary>
    /// Метод, вызываемый при отображении страницы.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is UserProfileViewModel viewModel)
        {
            await viewModel.LoadDataAsync();

        }
    }

    /// <summary>
    /// Метод для установки статуса сети.
    /// </summary>
    private void NetStatus()
    {
        if (netStatus == 0)
        {
            CircleOverlay.BackgroundColor = Colors.Grey;
        }
        else
        {
            CircleOverlay.BackgroundColor = Color.FromArgb("#3DC47C");
        }
    }

    /// <summary>
    /// Обработчик события нажатия на фото профиля.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private async void OnProfilePhotoTapped(object sender, EventArgs e)
    {
        try
        {
            var chosenImage = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Выберите изображение"
            });

            if (chosenImage != null)
            {
                var stream = await chosenImage.OpenReadAsync();
                //AvatarImage.Source = ImageSource.FromStream(() => stream);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Фото не было выбрано.", ex.Message, "OK");
        }
    }

    /// <summary>
    /// Обработчик события нажатия на кнопку монет.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private async void OnCoinButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NotesPage());
    }

    /// <summary>
    /// Обработчик события нажатия на кнопку рейтинга.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private async void OnRatingButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RatingPage());
    }

    /// <summary>
    /// Обработчик события нажатия на кнопку подписчиков.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private async void OnSubscribersButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SubscribersPage());
    }

    /// <summary>
    /// Обработчик события нажатия на кнопку подписок.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private async void OnFollowingButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FollowingPage());
    }

    /// <summary>
    /// Обработчик события нажатия на кнопку клубов.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private async void OnClubsButtonTapped(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Обработчик события нажатия на фрейм проектов.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private async void OnProjectsFrameTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProjectsPage());
    }

    /// <summary>
    /// Обработчик события нажатия на фрейм активностей.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private async void OnActivitiesFrameTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ActivitiesPage());
    }

    /// <summary>
    /// Обработчик события нажатия на фрейм календаря.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private async void OnCalendarFrameTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CalendarPage());
    }

    /// <summary>
    /// Обработчик события нажатия на фрейм новостей.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private async void OnNewsFrameTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NotesPage());
    }

    //private void OnSubUnsubButtonTapped(object sender, EventArgs e)
    //{
    //    {
    //        clickCount++;

    //        if (clickCount == 1)
    //        {
    //            SubscribeButton.BackgroundColor = Colors.White;
    //            SubscribeButton.BorderColor = Color.FromArgb("#0057A6");
    //            SubscribeButton.Text = "Вы подписаны";
    //            SubscribeButton.TextColor = Color.FromArgb("#0057A6");
    //        }
    //        else if (clickCount == 2)
    //        {
    //            SubscribeButton.BackgroundColor = Color.FromArgb("#0057A6");
    //            SubscribeButton.BorderColor = Colors.White;
    //            SubscribeButton.Text = "Подписаться";
    //            SubscribeButton.TextColor = Colors.White;
    //            clickCount = 0;
    //        }
    //    }
    //}
}