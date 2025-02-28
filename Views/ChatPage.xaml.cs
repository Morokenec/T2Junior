using MauiApp1.Services;
using MauiApp1.ViewModel;
using MauiApp1.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;

namespace MauiApp1.Pages;

public partial class ChatPage : ContentPage
{
    public static MessagesPage timePage = new MessagesPage();
    public bool Redirected { get; set; } = !BackNavigationState.IsDirectAccess;
    public static int SelectedChatId { get; set; }

    public string WriteTime { get; set; } = timePage.MessageTime;
    public ChatPage()
    {
        InitializeComponent();
        var messageView = new MessageViewModel();
        BindingContext = messageView;
    }

    private void SendBtnClicked(object sender, EventArgs e)
    {
        string messageText = TextEntry.Text;
        double editorCharWidth = (double)TextEntry.Width / (TextEntry.FontSize * 0.7);

        if (!string.IsNullOrWhiteSpace(messageText))
        {
            string trimmedText = messageText.TrimEnd('\n');
            int lineCount = (int)Math.Ceiling((double)trimmedText.Split(new[] { '\n' }).Length + (int)Math.Ceiling((double)(trimmedText.Length - trimmedText.Split(new[] { '\n' }).Length) / editorCharWidth) - 2);
            double lineHeight = 15;

            double imageHeight = lineCount * lineHeight;

            Grid messageGrid = new Grid
            {
                Margin = new Thickness(16,4,16,0),
            };

            Frame userMessageFrame = new Frame
            {
                BorderColor = Colors.Transparent,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Margin = new Thickness(60, 0, 0, 0),
                HasShadow = false,
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        new Image
                        {
                            Source = "your_message_holder.svg",
                            Aspect = Aspect.AspectFill,
                            HorizontalOptions = LayoutOptions.Fill,
                            VerticalOptions = LayoutOptions.Fill,
                            HeightRequest = imageHeight + 47
                        }
                    }
                }
            };

            StackLayout labelStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Margin = new Thickness(60, 0, 0, 0),
                Spacing = 2,
                Padding = new Thickness(8, 6, 6, 6),
                Children = {
                new Label
                {
                    Text = messageText,
                    FontSize = 15,
                    HorizontalOptions = LayoutOptions.Start,
                    TextColor = Color.FromArgb("#838281")
                },
                new Label
                {
                    Text = DateTime.Now.ToString("HH:MM"), 
                    FontSize = 12,
                    HorizontalOptions = LayoutOptions.End,
                    TextColor = Color.FromArgb("#A0A6AC")
                }
            }
            };

            messageGrid.Children.Add(userMessageFrame);
            messageGrid.Children.Add(labelStackLayout);

            FramesContainer.Children.Add(messageGrid);

            TextEntry.Text = string.Empty;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadDialogDetails();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        BindingContext = null;
    }
    private void LoadDialogDetails()
    {
        var dialogViewModel = new MessageViewModel();
        var selectedChat = dialogViewModel.GetChatById(SelectedChatId);
        ((MessageViewModel)BindingContext).SelectedChat = selectedChat;
    }
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
    private void OnAvatarTapped(object sender, EventArgs e)
    {
        var tappedElement = (sender as Frame)?.BindingContext as IChatElementType;

        if (tappedElement != null)
        {
            string tappedName = tappedElement.Name;

            var userViewModel = new UserViewModel();
            var clubViewModel = new ClubViewModel();

            var userProfile = userViewModel.UserProfiles.FirstOrDefault(u => u.Name == tappedName);
            var club = clubViewModel.Clubs.FirstOrDefault(c => c.Name == tappedName);

            if (userProfile != null)
            {
                ProfileService.SelectedUser = userProfile;
                ProfilePage profPage = new ProfilePage
                {
                    SelectedProfileId = userProfile.IdUser
                };

                Navigation.PushAsync(profPage);
            }
            else if (club != null)
            {
                ProfileService.SelectedClub = club;
                ClubProfilePage clubPage = new ClubProfilePage
                {
                    SelectedClubId = club.IdClub
                };

                Navigation.PushAsync(clubPage);
            }
        }
    }

    private void OnTextLinesCountChanged(object sender, TextChangedEventArgs e)
    {
        var editor = sender as Editor;
        int avgHeight = 44;
        double editorHeight;
        double maxHeight = (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) / 2.5;

        double editorCharWidth = (double)editor.Width / (editor.FontSize * 0.7);

        if (editor != null)
        {
            string trimmedText = editor.Text.TrimEnd('\n');
            double lineHeight = 13;
            var lineCount = (int)Math.Ceiling((double)trimmedText.Split(new[] { '\n' }).Length + (int)Math.Ceiling((double)(trimmedText.Length - trimmedText.Split(new[] { '\n' }).Length) / editorCharWidth) - 2);
            
            if (lineCount > 5)
            {
                lineCount = 5;
            }

            if (lineCount < 1)
            {
                lineHeight = 1;
            }
            else 
            {
                lineHeight = 13;
            }

            editorHeight = lineCount * lineHeight;

            editor.HeightRequest = editorHeight + avgHeight;

            var parentFrame = (Frame)editor.Parent.Parent;
            parentFrame.HeightRequest = editorHeight + avgHeight;
            var totalParentFrame = (Frame)editor.Parent.Parent.Parent.Parent;
            totalParentFrame.HeightRequest = 1.227 * avgHeight + editorHeight;

            if (totalParentFrame.HeightRequest > maxHeight)
            {
                editor.HeightRequest = maxHeight;
                parentFrame.HeightRequest = maxHeight;
                totalParentFrame.HeightRequest = 1.227 * maxHeight;
            }

            editor.VerticalOptions = LayoutOptions.Start;
        }
    }
}