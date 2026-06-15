namespace SewerDeformationSoftware.Logics.ViewModels;

public class Basis : INotifyPropertyChanged
{
    protected void NotifyToUIAndSetIfChanged<GenericType>(GenericType InputValue, ref GenericType FieldValue, [CallerMemberName] String? OwnerName = null)
    {
        if (!EqualityComparer<GenericType>.Default.Equals(FieldValue, InputValue))
        {
            FieldValue = InputValue;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(OwnerName));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
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