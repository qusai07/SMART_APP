using Microsoft.Extensions.Logging;
using UXDivers.Grial;
using CommunityToolkit.Maui;

namespace SMART_APP
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Poppins-Regular.ttf","Poppins");
                    fonts.AddFont("materialdesignicons-webfont.ttf","Material Design Icons");
                })

                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler<NavigationPage, UXDivers.Grial.GrialNavigationPageHandler>();
                    handlers.AddHandler<ScrollView, SMART_APP.ScrollViewHandler>();
                    handlers.AddHandler<Label, SMART_APP.LabelHandler>();
                    handlers.AddHandler<Entry, UXDivers.Grial.EntryHandler>();
                })
                .UseGrial()
                .UseMauiCommunityToolkit();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
