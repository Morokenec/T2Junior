namespace MauiApp1.Services
{
    public static class BackClick
    {
        public static event EventHandler PageClicked;

        static BackClick()
        {
            PageClicked += HandleOfPageClicked;
        }

        public static void OnPageClicked()
        {
            PageClicked?.Invoke(null, EventArgs.Empty);
        }

        public async static void HandleOfPageClicked(object sender, EventArgs e)
        {
            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                if (navigationPage.Navigation.NavigationStack.Count > 1)
                {
                    await navigationPage.PopAsync();
                }
            }
            else if (Application.Current.MainPage is Shell shell)
            {
                await shell.GoToAsync("..");
            }
        }
    }
}
