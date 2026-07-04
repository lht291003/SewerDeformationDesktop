namespace SewerDeformationSoftware.Logics.Utils;

public class Interop
{
    public static Window? TopmostWindow() => (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.Windows.LastOrDefault(W => W.IsVisible);
}