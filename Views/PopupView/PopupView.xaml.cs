using CommunityToolkit.Maui.Views;

namespace MauiApp1.Pages.PopupPage;

public partial class PopupView : Popup
{
    public event EventHandler UnsubscribeConfirmed;
    public event EventHandler SubscribeConfirmed;

    public PopupView()
	{
		InitializeComponent();
        VerticalOptions = Microsoft.Maui.Primitives.LayoutAlignment.End;
	}

    private void UnsubTapped(object sender, TappedEventArgs e)
    {
        UnsubscribeConfirmed?.Invoke(this, EventArgs.Empty);
        Close();
    }

    private void SubTapped(object sender, TappedEventArgs e)
    {
        SubscribeConfirmed?.Invoke(this, EventArgs.Empty);
        Close();
    }
}