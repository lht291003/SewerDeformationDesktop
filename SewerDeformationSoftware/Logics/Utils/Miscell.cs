namespace SewerDeformationSoftware.Logics.Utils;

public class Miscell
{
    public static Window? TopmostWindow()

                => (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.Windows.LastOrDefault(W => W.IsVisible);

    public static unsafe WriteableBitmap ToWriteableBitmap(Mat Src)
    {
        WriteableBitmap BitmapImage = new(new PixelSize(Src.Width, Src.Height), new Vector(96.0, 96.0), PixelFormat.Bgra8888, AlphaFormat.Opaque);

        using ILockedFramebuffer Framebuffer = BitmapImage.Lock();

        using Mat BgraMat = new();

        Cv2.CvtColor(Src, BgraMat, ColorConversionCodes.BGR2BGRA);

        Byte* SrcPointer = BgraMat.DataPointer;

        Byte* DstPointer = (Byte*)Framebuffer.Address.ToPointer();

        Int64 RowBytes = (Int64)BgraMat.Cols * BgraMat.ElemSize();

        Int64 SStep = BgraMat.Step();

        Int32 MatRows = BgraMat.Rows;

        Int64 DStep = Framebuffer.RowBytes;

        Int64 TotalBytes = DStep * MatRows;

        if (BgraMat.IsContinuous() && !BgraMat.IsSubmatrix() && SStep == DStep)
        {
            Buffer.MemoryCopy(SrcPointer, DstPointer, TotalBytes, TotalBytes);
        }

        else for (Int32 Row = 0; Row < MatRows; Row++) Buffer.MemoryCopy(SrcPointer + Row * SStep, DstPointer + Row * DStep, RowBytes, RowBytes);

        return BitmapImage;
    }
}