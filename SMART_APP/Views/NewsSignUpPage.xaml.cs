/* [grial-metadata] id: Grial#NewsSignUpPage.cs version: 1.0.1 */
namespace SMART_APP;

public partial class NewsSignUpPage : ContentPage
{
	public NewsSignUpPage()
	{
		InitializeComponent();
	}

    private void CreateButtonClicked(object sender, EventArgs e)
    {

    }

    private void LoginButtonClicked(object sender, EventArgs e)
    {

    }

    private void OnPasswordEyeTapped(object sender, TappedEventArgs e)
    {
        passwordEntry.IsPassword = !passwordEntry.IsPassword;

        var icon = (Label)sender;
        icon.Text = passwordEntry.IsPassword
            ? MaterialCommunityIconsFont.EyeOutline
            : MaterialCommunityIconsFont.EyeOffOutline;
    }

    private void OnConfirmPasswordEyeTapped(object sender, TappedEventArgs e)
    {
        confirmPasswordEntry.IsPassword = !confirmPasswordEntry.IsPassword;

        var icon = (Label)sender;
        icon.Text = confirmPasswordEntry.IsPassword
            ? MaterialCommunityIconsFont.EyeOutline
            : MaterialCommunityIconsFont.EyeOffOutline;
    }



}
