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

public class ARelayCommand<GenericType>(Predicate<GenericType> Trigger, Action<GenericType> Action) : ICommand
{
    public bool CanExecute(Object? Parameter)

                       => Trigger == null || Trigger((GenericType)Parameter!);

    public void Execute(Object? Parameter) => Action((GenericType)Parameter!);

    public event EventHandler? CanExecuteChanged { add { CommandManager.RequerySuggested += value; } remove { CommandManager.RequerySuggested -= value; } }
}

public class FRelayCommand<GenericType>(Predicate<GenericType> Trigger, Func<GenericType, Task> Action) : ICommand
{
    Boolean IsExecuting;

    public Boolean CanExecute(Object? Parameter)

                                         => !IsExecuting && (Trigger == null || Trigger((GenericType)Parameter!));

    public async void Execute(Object? Parameter)
    {
        if (CanExecute(Parameter))
        {
            IsExecuting = (0 == 0);

            CommandManager.NotifyCommandRequery();

            await Action((GenericType)Parameter!);

            IsExecuting = (0 != 0);

            CommandManager.NotifyCommandRequery();
        }
    }

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