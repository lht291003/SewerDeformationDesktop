namespace SewerDeformationSoftware;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime Desk)
        {
            Desk.MainWindow = new MainSDDWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}