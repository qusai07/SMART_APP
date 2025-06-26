using CommunityToolkit.Mvvm.Input;
using SMART_APP.Models;
using SMART_APP.Services;
using SMART_APP.Services.Auth;
using SMART_APP.Services.Base;

namespace SMART_APP;

public partial class NewsLoginPage : ContentPage
{
    private readonly AuthService authService ;

	public NewsLoginPage()
	{
		InitializeComponent();
        authService = new AuthService();

    }

    private async void LoginButtonClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text;       
        var password = PasswordEntry.Text; 

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please enter email and password", "OK");
            return;
        }

        var loginModel = new Models.Login.LoginRequest
        {
            UserNameOrEmail = email,
            Password = password
        };

        var loginResponse = await authService.LoginAsync(loginModel);

        if (loginResponse == null)
        {
            await DisplayAlert("Login Failed", "Invalid email or password", "OK");
            return;
        }

        await DisplayAlert("Success", "Login successful!", "OK");

        await Navigation.PushAsync(new ProfileUserPage());
    }

    private void SignupButtonClicked(object sender, EventArgs e)
    {

    }
    [RelayCommand]
    public async Task ForgetPasswordCommand()
    {
        await Navigation.PushAsync(new ForgetPasswordPage());

    }

    private void OnEyeTapped(object sender, TappedEventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
    }
}
