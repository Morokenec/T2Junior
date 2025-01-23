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
        BindingContext = this;
    }
}