using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class EventsPage : ContentPage
{
    int clickCount = 0;
    public EventsPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        BindingContext = new EventViewModel();
    }
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
    private void CalendarRedirectTapped(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new CalendarPage());
    }
    private void OnEventSubRequest(object sender, EventArgs e)
    {
        clickCount++;
        var button = sender as Button;

        if (clickCount == 1)
        {
            button.BackgroundColor = Colors.White;
            button.BorderColor = Color.FromArgb("#0057A6");
            button.TextColor = Color.FromArgb("#0057A6");
            button.Text = "Отписаться";

            var eventModel = button?.CommandParameter as Event;

            if (eventModel != null)
            {
                var viewModel = (EventViewModel)BindingContext;
                viewModel.OnEventSubRequest(eventModel);
            }
        }
        else if (clickCount == 2)
        {
            button.BackgroundColor = Color.FromArgb("#0057A6");
            button.BorderColor = Colors.White;
            button.TextColor = Colors.White;
            button.Text = "Записаться";

            var eventModel = button?.CommandParameter as Event;

            if (eventModel != null)
            {
                var viewModel = (EventViewModel)BindingContext;
                viewModel.OnEventSubRequest(eventModel);
            }

            clickCount = 0;
        }
    }
}