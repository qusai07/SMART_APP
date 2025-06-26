using SMART_APP.Models;
using SMART_APP.Services;
using SMART_APP.Services.Auth;
using SMART_APP.Services.Base;

namespace SMART_APP;

public partial class ForgetPasswordPage : ContentPage
{
    private readonly AuthService authService ;

	public ForgetPasswordPage()
	{
		InitializeComponent();
        authService = new AuthService();

    }
    private async void SendResetLinkClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            await DisplayAlert("Error", "Please enter your email address.", "OK");
            return;
        }

        await DisplayAlert("Success", $"A reset link has been sent to {EmailEntry.Text}", "OK");

        await Navigation.PopAsync(); 
    }

    private async void ButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }


}
