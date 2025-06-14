namespace SMART_APP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // هنا تضع التنقل لصفحة تسجيل الدخول
            await Navigation.PushAsync(new NewsLoginPage());
        }
        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            // هنا تضع التنقل لصفحة تسجيل الدخول
            await Navigation.PushAsync(new NewsSignUpPage());
        }

     
    }
}


