using SMART_APP.Models;
using SMART_APP.Services;

namespace SMART_APP;

public partial class NewsLoginPage : ContentPage
{
    private readonly ApiService apiService;

	public NewsLoginPage()
	{
		InitializeComponent();
        apiService = new ApiService();

    }

    private async void LoginButtonClicked(object sender, EventArgs e)
    {
        // مثلاً بتاخد القيم من Entrys موجودين في الصفحة
        var email = EmailEntry.Text;       
        var password = PasswordEntry.Text; 

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please enter email and password", "OK");
            return;
        }

        var loginModel = new LoginModel
        {
            UserNameOrEmail = email,
            Password = password
        };

        var loginResponse = await apiService.LoginAsync(loginModel);

        if (loginResponse == null)
        {
            await DisplayAlert("Login Failed", "Invalid email or password", "OK");
            return;
        }

        await DisplayAlert("Success", "Login successful!", "OK");

        await Navigation.PushAsync(new MainPage());
    }

    private void SignupButtonClicked(object sender, EventArgs e)
    {

    }

    private void OnEyeTapped(object sender, TappedEventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
        icon.Text = PasswordEntry.IsPassword ? MaterialCommunityIconsFont.EyeOutline : MaterialCommunityIconsFont.EyeOffOutline;
    }
}
