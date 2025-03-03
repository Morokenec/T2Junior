using MauiApp1.Services.AppHelper;

namespace MauiApp1;

/// <summary>
/// �������� � ���������� � ����������.
/// </summary>
public partial class RatingPage : ContentPage
{
    /// <summary>
    /// ����������� ������ RatingPage.
    /// </summary>
    public RatingPage()
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