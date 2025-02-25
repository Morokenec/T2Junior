using CommunityToolkit.Maui.Views;
using MauiApp1.Pages.PopupPage;
using MauiApp1.Services;
using MauiApp1.ViewModel;

namespace MauiApp1;

public partial class ClubProfilePage : ContentPage
{
    int clickCount = 0;

    private bool isLabelClickable = false;
    public bool HolderIsVisible { get; set; } = true; //добавить условия
    public static int SelectedClubId { get; set; }

    public PopupView ActivePopup { get; set; }
    public ClubProfilePage()
    {
        InitializeComponent();
        BindingContext = new ClubProfileViewModel();
        LoadClubDetails();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }

    private void OnRatingButtonTapped(object sender, EventArgs e)
    {
         Navigation.PushAsync(new RatingPage());
    }

    private void OnCalendarButtonTapped(object sender, EventArgs e)
    {
         Navigation.PushAsync(new CalendarPage());
    }

    private void OnRulesButtonTapped(object sender, EventArgs e)
    {
         Navigation.PushAsync(new RulesPage());
    }

    private void OnInitiumButtonTapped(object sender, EventArgs e)
    {
         Navigation.PushAsync(new InitiumPage());
    }

    private void OnSubUnsubButtonTapped(object sender, EventArgs e)
    {
        var clubViewModel = (ClubProfileViewModel)BindingContext;
        {
            clickCount++;

            var club = clubViewModel.SelectedClub;

            if (clickCount == 1)
            {
                club.SubCount += 1;
                SubscribeButton.BackgroundColor = Colors.White;
                SubscribeButton.BorderColor = Color.FromArgb("#0057A6");
                SubscribeButton.Text = "Проекты";
                SubscribeButton.TextColor = Color.FromArgb("#0057A6");
                JobLabel.Text = $"Вы подписаны • Количество участников {club.SubValue}";
                isLabelClickable = true;
                UpdateLabelClickability();
            }
            else if (clickCount == 2)
            {
                Navigation.PushAsync(new ProjectsPage());
                clickCount = 1;
            }
        }
    }

    private void UpdateLabelClickability()
    {
        if (isLabelClickable)
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnLabelTapped;
            JobLabel.GestureRecognizers.Add(tapGestureRecognizer);
        }
        else
        {
            JobLabel.GestureRecognizers.Clear();
        }
    }

    private void OnLabelTapped(object sender, EventArgs e)
    {
        var popup = new PopupView();
        popup.UnsubscribeConfirmed += OnUnsubscribeConfirmed;
        popup.SubscribeConfirmed += OnSubscribeConfirmed;
        ActivePopup = popup;

        Application.Current.MainPage.ShowPopup(ActivePopup);
    }

    private void OnSubscribeConfirmed(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new SubscribersPage());
    }

    private void OnUnsubscribeConfirmed(object? sender, EventArgs e)
    {
        var clubViewModel = (ClubProfileViewModel)BindingContext;
        var club = clubViewModel.SelectedClub;

        club.SubCount -= 1;
        SubscribeButton.BackgroundColor = Color.FromArgb("#0057A6");
        SubscribeButton.BorderColor = Colors.White;
        SubscribeButton.Text = "Подписаться";
        SubscribeButton.TextColor = Colors.White;
        JobLabel.Text = $"Количество участников {club.SubValue}";
        clickCount = 0;

        isLabelClickable = false;
        UpdateLabelClickability();
    }

    private void OnSearch(object sender, EventArgs e)
    {
        //добавить
    }

    private void OnNewsTapped(object sender, EventArgs e)
    {
        //переход к новости
    }

    private void LoadClubDetails()
    {
        var clubViewModel = new ClubViewModel();
        var selectedClub = clubViewModel.GetClubById(SelectedClubId);
        ((ClubProfileViewModel)BindingContext).SelectedClub = selectedClub;
    }
}