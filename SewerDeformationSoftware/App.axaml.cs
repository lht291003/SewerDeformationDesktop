namespace SewerDeformationSoftware;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime A)
        {
            A.MainWindow = new MainSDDWindow();

            A.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        Array.ForEach(new RoutedEvent[] { InputElement.PointerReleasedEvent, InputElement.KeyUpEvent, InputElement.GotFocusEvent, InputElement.LostFocusEvent }, AddCommandRequeryHandlers);
    }

    public void AddCommandRequeryHandlers(RoutedEvent Event)

                                   => Event.AddClassHandler(typeof(TopLevel), (_, _) => CommandManager.NotifyCommandRequery(), RoutingStrategies.Direct | RoutingStrategies.Bubble, 0 == 0);
}