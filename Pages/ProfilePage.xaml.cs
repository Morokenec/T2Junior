using MauiApp1.DataModel;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase;
using MauiApp1.Services.UseCase.Interface;
using MauiApp1.ViewModels.Profile;
using System.Collections.ObjectModel;

namespace MauiApp1;

/// <summary>
/// �������� ������� ������������ � ����������.
/// </summary>
public partial class ProfilePage : ContentPage
{
    int netStatus = 1;

    private readonly UserProfileViewModel _viewModel;

    /// <summary>
    /// ����, �����������, ��� �� ������ � �������� ��������.
    /// </summary>
    public bool DirectAccessed { get; set; } = BackNavigationState.IsDirectAccess;

    /// <summary>
    /// ����������� ������ ProfilePage.
    /// </summary>
    /// <param name="userProfileViewModel">������ ������������� ������� ������������.</param>
    public ProfilePage(UserProfileViewModel userProfileViewModel)
    {
        InitializeComponent();
        _viewModel = userProfileViewModel;
        BindingContext = _viewModel;
        NetStatus();
    }

    /// <summary>
    /// �����, ���������� ��� ����������� ��������.
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
    /// ����� ��� ��������� ������� ����.
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
    /// ���������� ������� ������� �� ���� �������.
    /// </summary>
    /// <param name="sender">������, ��������� �������.</param>
    /// <param name="e">��������� �������.</param>
    private async void OnProfilePhotoTapped(object sender, EventArgs e)
    {
        try
        {
            var chosenImage = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "�������� �����������"
            });

            if (chosenImage != null)
            {
                var stream = await chosenImage.OpenReadAsync();
                //AvatarImage.Source = ImageSource.FromStream(() => stream);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("���� �� ���� �������.", ex.Message, "OK");
        }
    }

    /// <summary>
    /// ���������� ������� ������� �� ������ �����.
    /// </summary>
    /// <param name="sender">������, ��������� �������.</param>
    /// <param name="e">��������� �������.</param>
    private async void OnCoinButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NotesPage());
    }

    /// <summary>
    /// ���������� ������� ������� �� ������ ��������.
    /// </summary>
    /// <param name="sender">������, ��������� �������.</param>
    /// <param name="e">��������� �������.</param>
    private async void OnRatingButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RatingPage());
    }

    /// <summary>
    /// ���������� ������� ������� �� ������ �����������.
    /// </summary>
    /// <param name="sender">������, ��������� �������.</param>
    /// <param name="e">��������� �������.</param>
    private async void OnSubscribersButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SubscribersPage());
    }

    /// <summary>
    /// ���������� ������� ������� �� ������ ��������.
    /// </summary>
    /// <param name="sender">������, ��������� �������.</param>
    /// <param name="e">��������� �������.</param>
    private async void OnFollowingButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FollowingPage());
    }

    /// <summary>
    /// ���������� ������� ������� �� ������ ������.
    /// </summary>
    /// <param name="sender">������, ��������� �������.</param>
    /// <param name="e">��������� �������.</param>
    private async void OnClubsButtonTapped(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// ���������� ������� ������� �� ����� ��������.
    /// </summary>
    /// <param name="sender">������, ��������� �������.</param>
    /// <param name="e">��������� �������.</param>
    private async void OnProjectsFrameTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProjectsPage());
    }

    /// <summary>
    /// ���������� ������� ������� �� ����� �����������.
    /// </summary>
    /// <param name="sender">������, ��������� �������.</param>
    /// <param name="e">��������� �������.</param>
    private async void OnActivitiesFrameTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ActivitiesPage());
    }

    /// <summary>
    /// ���������� ������� ������� �� ����� ���������.
    /// </summary>
    /// <param name="sender">������, ��������� �������.</param>
    /// <param name="e">��������� �������.</param>
    private async void OnCalendarFrameTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CalendarPage());
    }

    /// <summary>
    /// ���������� ������� ������� �� ����� ��������.
    /// </summary>
    /// <param name="sender">������, ��������� �������.</param>
    /// <param name="e">��������� �������.</param>
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
    //            SubscribeButton.Text = "�� ���������";
    //            SubscribeButton.TextColor = Color.FromArgb("#0057A6");
    //        }
    //        else if (clickCount == 2)
    //        {
    //            SubscribeButton.BackgroundColor = Color.FromArgb("#0057A6");
    //            SubscribeButton.BorderColor = Colors.White;
    //            SubscribeButton.Text = "�����������";
    //            SubscribeButton.TextColor = Colors.White;
    //            clickCount = 0;
    //        }
    //    }
    //}
}