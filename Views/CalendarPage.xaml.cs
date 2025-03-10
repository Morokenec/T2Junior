using MauiApp1.Services;
using MauiApp1.ViewModels;
using MauiApp1.Views;

namespace MauiApp1;

public partial class CalendarPage : ContentPage
{
    private CalendarViewModel _calendarViewModel;

    static Dictionary<int, string> monthNames = new Dictionary<int, string>
    {
        { 1, "������" },
        { 2, "�������" },
        { 3, "����" },
        { 4, "������" },
        { 5, "���" },
        { 6, "����" },
        { 7, "����" },
        { 8, "������" },
        { 9, "��������" },
        { 10, "�������" },
        { 11, "������" },
        { 12, "�������" }
    };

    public static DateTime choosedTimeline { get; set; } = DateTime.Now;
    public string TimePeriod { get; set; } = $"{monthNames[choosedTimeline.Month] + " " + choosedTimeline.Year}";
    public string EventName { get; set; }
    public Color ColorOfAButton { get; set; }

    public CalendarPage()
    {
        InitializeComponent();
        _calendarViewModel = new CalendarViewModel();
        BindingContext = _calendarViewModel;
        EventName = EventViewModel.Current.SelectedEvent;
        ColorOfAButton = EventViewModel.Current.ButtonBackgroundColor;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BindingContext = new CalendarViewModel();
        EventName = EventViewModel.Current.SelectedEvent;
        ColorOfAButton = EventViewModel.Current.ButtonBackgroundColor;
    }

    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }

    private void ChoosingATimePeriod(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new TimePeriodPage());
    }

    private void EventRedirectTapped(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new EventsPage());
    }
}