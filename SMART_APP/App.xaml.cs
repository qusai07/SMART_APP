/* [grial-metadata] id: Grial#App.xaml version: 1.1.3 */
using UXDivers.Grial;
namespace SMART_APP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SMART_APP.MainPage());
        }
    }
}
