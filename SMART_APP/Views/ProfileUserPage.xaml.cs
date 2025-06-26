using CommunityToolkit.Mvvm.Input;
using SMART_APP.Services.Profile;
using SMART_APP.ViewModels;

namespace SMART_APP;

public partial class ProfileUserPage : ContentPage
{
    private readonly ProfileService _profileService;
    private readonly ProfileUserViewModel _viewModel;

    public ProfileUserPage()
    {
        InitializeComponent();
        _profileService = new ProfileService();
        _viewModel = new ProfileUserViewModel();
        BindingContext = _viewModel;
        LoadProfile();
    }

    private async void LoadProfile()
    {
        var result = await _profileService.GetUserProfileAsync();

        if (result.IsSuccess)
        {
            _viewModel.FullName = result.Data.fullName;
            _viewModel.EmailAddress = result.Data.emailAddress;
            _viewModel.UserName = result.Data.userName;
      //      _viewModel.PhoneNumber = result.Data.PhoneNumber;

        }
        else
        {
            await DisplayAlert("Error", result.ErrorMessage, "OK");
        }
    }
    public async void BackAndLogoutClicked(object sender, EventArgs e)
    {
        var confirm = await DisplayAlert("Confirm", "Do you want to logout?", "Yes", "Cancel");
        if (!confirm)
            return;

        var result = await _profileService.LogoutAsync();

        if (result.IsSuccess)
        {
            ClearAllSecureStorage();
            await Navigation.PushAsync(new NewsLoginPage());
        }
        else
        {
            await DisplayAlert("Error", result.ErrorMessage, "OK");
        }
    }
    public static void ClearAllSecureStorage()
    {
        SecureStorage.Remove("Token");
        SecureStorage.Remove("Email");
        SecureStorage.Remove("UserName");
    }


}
