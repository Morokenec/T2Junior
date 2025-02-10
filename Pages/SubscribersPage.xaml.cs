namespace MauiApp1;

public partial class SubscribersPage : ContentPage
{
    string portProfileName;
    public SubscribersPage()
    {
        InitializeComponent();
        ElementsProperties();
        ResizeFunction();
        GetLabelTextFromFrame();
    }

    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }

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

    private async Task OnProfileFrameTapped(Frame choosedFrame)
    {
        var hsl = (HorizontalStackLayout)choosedFrame.Content;

        var fullNameLabel = (Label)hsl.Children[1];

        var profilePage = new ProfilePage();
        profilePage.FullName = fullNameLabel.Text;

        await Navigation.PushAsync(profilePage);
    }

    private void ElementsProperties()
    {
        SubFrame.WidthRequest = 343;
        SubFrame.HeightRequest = 40;
        SubFrame.Margin = new Thickness(20, 16);
        SubFrame.Padding = new Thickness(0, 8);

        BackFrame.WidthRequest = 24;
        BackFrame.HeightRequest = 24;
        BackFrame.Padding = new Thickness(0);

        SubTextFrame.WidthRequest = 319;
        SubTextFrame.HeightRequest = 24;
        SubTextFrame.Padding = new Thickness(0, 0, 24, 0);

        ProfilesLayout.Spacing = 4;

        ProfContainerFrame.WidthRequest = 343;
        ProfContainerFrame.HeightRequest = 312;
        ProfContainerFrame.Padding = new Thickness(8, 10);
    }

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
                                            if (fileImageSource.File == "redirect.png")
                                            {
                                                image.WidthRequest = 24;
                                                image.HeightRequest = 24;
                                            }
                                            else if (fileImageSource.File == "profile_placeholder.png")
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