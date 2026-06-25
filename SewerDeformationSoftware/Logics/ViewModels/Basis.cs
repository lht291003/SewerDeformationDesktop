namespace SewerDeformationSoftware.Logics.ViewModels;

public class CommandManager
{
    public static DispatcherOperation? RequeryOperation;

    public static event EventHandler? RequerySuggested;

    public static void Requery()
    {
        RequeryOperation = null;

        RequerySuggested?.Invoke(null, EventArgs.Empty);
    }

    public static void NotifyCommandRequery()

                => RequeryOperation ??= Dispatcher.UIThread.InvokeAsync(Requery, DispatcherPriority.Background);
}

public class RelayCommand<GenericType>(Predicate<GenericType> Trigger, Action<GenericType> Function) : ICommand
{
    Predicate<GenericType> Trigger = Trigger;

    Action<GenericType> Execution = Function;

    public bool CanExecute(Object? Parameter)

                          => Trigger == null || Trigger((GenericType)Parameter!);

    public void Execute(Object? Parameter) => Execution((GenericType)Parameter!);

    public event EventHandler? CanExecuteChanged { add { CommandManager.RequerySuggested += value; } remove { CommandManager.RequerySuggested -= value; } }
}

public class Basis : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyToUIAndSetIfChanged<GenericType>(GenericType InputValue, ref GenericType FieldValue, [CallerMemberName] String? OwnerName = null)
    {
        if (!EqualityComparer<GenericType>.Default.Equals(FieldValue, InputValue))
        {
            FieldValue = InputValue;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(OwnerName));
        }
    }
}