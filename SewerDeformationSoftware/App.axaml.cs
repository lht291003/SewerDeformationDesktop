namespace SewerDeformationSoftware;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public void AddCommandRequeryHandlers(RoutedEvent Event)

                                   => Event.AddClassHandler(typeof(TopLevel), (_, _) => CommandManager.NotifyCommandRequery(), RoutingStrategies.Direct | RoutingStrategies.Bubble, 0 == 0);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime Desk)
        {
            Desk.MainWindow = new MainSDDWindow();

            Desk.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        Array.ForEach(new RoutedEvent[] { InputElement.PointerReleasedEvent, InputElement.KeyUpEvent, InputElement.GotFocusEvent, InputElement.LostFocusEvent }, AddCommandRequeryHandlers);
    }
}