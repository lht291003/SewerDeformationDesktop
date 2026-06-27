namespace SewerDeformationSoftware.Logics.Utils;

public class WorkDir
{
    public static String Warehouse { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SewerAppData");
}