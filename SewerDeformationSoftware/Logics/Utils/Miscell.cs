namespace SewerDeformationSoftware.Logics.Utils;

public class Miscell
{
    public static Window? TopmostWindow()
        
        => (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.Windows.LastOrDefault(W => W.IsVisible);

    public static WriteableBitmap ToWriteableBitmap(Mat MatSrc) => WriteableBitmapConverter.ToWriteableBitmap(MatSrc, AlphaFormat.Opaque);
}