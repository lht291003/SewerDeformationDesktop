namespace SewerDeformationSoftware.Logics.Converters;

public class None2Hidden : IValueConverter
{
    public Object? Convert(Object? Value, Type TargetType, Object? Parameter, CultureInfo Culture)
    {
        if (Value == null)
        {
            return false;
        }

        if (Value is String Path && String.IsNullOrWhiteSpace(Path))
        {
            return false;
        }

        return true;
    }

    public Object? ConvertBack(Object? Value, Type TargetType, Object? Parameter, CultureInfo Culture)

                                                                       => BindingOperations.DoNothing;
}