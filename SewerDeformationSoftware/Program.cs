namespace SewerDeformationSoftware;

internal class Program
{
    [STAThread]
    public static void Main(String[] Args) => BuildAvaloniaApp().StartWithClassicDesktopLifetime(Args);
    public static AppBuilder BuildAvaloniaApp()

                       => AppBuilder.Configure<App>().UsePlatformDetect().WithInterFont().LogToTrace();
}