using MauiApp1.Services.AppHelper;

namespace MauiApp1;

/// <summary>
/// �������� ����������� � ����������.
/// </summary>
public partial class ActivitiesPage : ContentPage
{
    /// <summary>
    /// ����������� ������ ActivitiesPage.
    /// </summary>
    public ActivitiesPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// ���������� ������� ������� �� ������ "�����".
    /// </summary>
    /// <param name="sender">������, ��������� �������.</param>
    /// <param name="e">��������� �������.</param>
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
}