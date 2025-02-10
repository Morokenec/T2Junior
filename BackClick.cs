
/* Необъединенное слияние из проекта "AuthAndSoPagesMauiApp (net8.0-windows10.0.19041.0)"
До:
using System;
После:
using MauiApp1;
using Microsoft.Maui.Controls;
using System;
*/

/* Необъединенное слияние из проекта "AuthAndSoPagesMauiApp (net8.0-android)"
До:
using System;
После:
using MauiApp1;
using Microsoft.Maui.Controls;
using System;
*/

/* Необъединенное слияние из проекта "AuthAndSoPagesMauiApp (net8.0-maccatalyst)"
До:
using System;
После:
using MauiApp1;
using Microsoft.Maui.Controls;
using System;
*/

/* Необъединенное слияние из проекта "AuthAndSoPagesMauiApp (net8.0-windows10.0.19041.0)"
До:
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using MauiApp1;
После:
using System.Threading.Tasks;
*/

/* Необъединенное слияние из проекта "AuthAndSoPagesMauiApp (net8.0-android)"
До:
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using MauiApp1;
После:
using System.Threading.Tasks;
*/

/* Необъединенное слияние из проекта "AuthAndSoPagesMauiApp (net8.0-maccatalyst)"
До:
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using MauiApp1;
После:
using System.Threading.Tasks;
*/
namespace MauiApp1
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
