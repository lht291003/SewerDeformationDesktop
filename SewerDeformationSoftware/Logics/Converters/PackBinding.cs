namespace SewerDeformationSoftware.Logics.Converters;

public sealed class PackBinding : IMultiValueConverter
{
    public Object? Convert(IList<Object?> Values, Type TargetType, Object? Parameter, CultureInfo Culture)

                                                                             => new List<Object?>(Values);
}