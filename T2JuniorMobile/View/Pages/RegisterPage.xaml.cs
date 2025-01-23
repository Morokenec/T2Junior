using System.Collections.ObjectModel;

namespace T2JuniorMobile.View.Pages;

public partial class RegisterPage : ContentPage
{
    public ObservableCollection<string> GenderOptions { get; set; }
    public ObservableCollection<string> OrganizationOptions { get; set; }
    public RegisterPage()
	{
		InitializeComponent();
        GenderOptions = new ObservableCollection<string>
            {
                "Мужской",
                "Женский",
                "Не указано"
            };

        OrganizationOptions = new ObservableCollection<string>
            {
                "АО КОНЦЕРН ТИТАН-2",
                "ООО АЙ БИ КЕЙ",
                "ООО ПИ ЭМ ДИ"
            };
        BindingContext = this;
    }
}