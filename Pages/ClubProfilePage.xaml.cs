using MauiApp1.ViewModels.ClubProfileViewModel;
using MauiApp1.Services.UseCase.Interface;

namespace MauiApp1;

/// <summary>
/// �������� ������� ����� � ����������.
/// </summary>
public partial class ClubProfilePage : ContentPage
{
    private readonly ClubProfileViewModel _viewModel;

    /// <summary>
    /// ����������� ������ ClubProfilePage.
    /// </summary>
    /// <param name="clubProfileViewModel">������ ������������� ��� ���������� �������� �����.</param>
    public ClubProfilePage(ClubProfileViewModel clubProfileViewModel)
    {
        InitializeComponent();
        _viewModel = clubProfileViewModel;
        BindingContext = _viewModel;
    }

    /// <summary>
    /// �����, ���������� ��� ����������� ��������.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadClubsAsync();
    }
}
