namespace SewerDeformationSoftware.Logics.Utils;

public class Interop
{
    public static Window TopmostWindow() => ((IClassicDesktopStyleApplicationLifetime)Application.Current!.ApplicationLifetime!).Windows.Last(W => W.IsVisible);
}