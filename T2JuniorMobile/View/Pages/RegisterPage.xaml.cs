using System.Collections.ObjectModel;
using T2JuniorMobile.ViewModel;

namespace T2JuniorMobile.View.Pages;

public partial class RegisterPage : ContentPage
{
    public ObservableCollection<string> GenderOptions { get; set; }
    public ObservableCollection<string> OrganizationOptions { get; set; }
    public RegisterPage()
	{
        BindingContext = new RegisterViewModel();
        InitializeComponent();
        GenderOptions = new ObservableCollection<string>
            {
                "�������",
                "�������",
                "�� �������"
            };

        OrganizationOptions = new ObservableCollection<string>
            {
                "�� ������� �����-2",
                "��� �� �� ���",
                "��� �� �� ��"
            };
    }
}