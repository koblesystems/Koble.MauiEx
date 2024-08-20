using Microsoft.Maui.LifecycleEvents;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility;

namespace SampleApp;

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
          })
#if WINDOWS
            .ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(wndLifeCycleBuilder =>
                {
                    wndLifeCycleBuilder.OnWindowCreated(window =>
                    {
                        var manager = WinUIEx.WindowManager.Get(window);
                        manager.PersistenceId = "MainWindowPersistanceId"; // Remember window position and size across runs
                        manager.Width = 480;
                        manager.Height = 640;
                    });
                });
            })
#endif
          .UseMauiEx()
          .UseTelerikControls()
          .UseTelerik();
        return builder.Build();
    }
}