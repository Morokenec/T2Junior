using MauiApp1.ViewModel;

namespace MauiApp1;

public partial class ClubProfilePage : ContentPage
{
    int clickCount = 0;
    public string NoteMediaSource { get; set; } = "news_media_holder.png"; //добавить условия
    public bool HolderIsVisible { get; set; } = true; //добавить условия
    public static int SelectedClubId { get; set; }
    public ClubProfilePage()
	{
		InitializeComponent();
        BindingContext = new ClubProfileViewModel();
        LoadClubDetails();
    }

    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }

    private async void OnRatingButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RatingPage());
    }

    private async void OnCalendarButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CalendarPage());
    }

    private async void OnRulesButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RulesPage());
    }

    private async void OnInitiumButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InitiumPage());
    }

    private void OnSubUnsubButtonTapped(object sender, EventArgs e)
    {
        {
            clickCount++;

            if (clickCount == 1)
            {
                SubscribeButton.BackgroundColor = Colors.White;
                SubscribeButton.BorderColor = Color.FromArgb("#0057A6");
                SubscribeButton.Text = "Проекты";
                SubscribeButton.TextColor = Color.FromArgb("#0057A6");
            }
            else if (clickCount == 2)
            {
                SubscribeButton.BackgroundColor = Color.FromArgb("#0057A6");
                SubscribeButton.BorderColor = Colors.White;
                SubscribeButton.Text = "Подписаться";
                SubscribeButton.TextColor = Colors.White;
                clickCount = 0;
            }
        }
    }

    private void OnSearch(object sender, EventArgs e)
    {
        
    }

    private void OnNewsTapped(object sender, EventArgs e)
    {

    }

    private void LoadClubDetails()
    {
        var clubViewModel = new ClubViewModel();
        var selectedClub = clubViewModel.GetClubById(SelectedClubId);
        ((ClubProfileViewModel)BindingContext).SelectedClub = selectedClub;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

    }
}