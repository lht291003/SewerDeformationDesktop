namespace SewerDeformationSoftware.Logics.Converters;

public class None2Unbind : IValueConverter
{
    public Object? Convert(Object? Value, Type TargetType, Object? Parameter, CultureInfo Culture)
    {
        if (Value == null)
        {
            return BindingOperations.DoNothing;
        }

        if (Value is String Path && String.IsNullOrWhiteSpace(Path))
        {
            return BindingOperations.DoNothing;
        }

        return Value;
    }

    public Object? ConvertBack(Object? Value, Type TargetType, Object? Parameter, CultureInfo Culture)

                                                                       => BindingOperations.DoNothing;
}