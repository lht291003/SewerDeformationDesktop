namespace SewerDeformationSoftware.Logics.ViewModels;

public class Photo : Basis
{
    public WriteableBitmap? PlotImage { get; set => NotifyToUIAndSetIfChanged(value, ref field); } = null;

    public WriteableBitmap? MaskImage { get; set => NotifyToUIAndSetIfChanged(value, ref field); } = null;

    public String AspectRatio { get; set => NotifyToUIAndSetIfChanged(value, ref field); } = String.Empty;

    public String Orientation { get; set => NotifyToUIAndSetIfChanged(value, ref field); } = String.Empty;

    public String Deformation { get; set => NotifyToUIAndSetIfChanged(value, ref field); } = String.Empty;

    public String Shape { get; set => NotifyToUIAndSetIfChanged(value, ref field); } = String.Empty;

    public String State { get; set => NotifyToUIAndSetIfChanged(value, ref field); } = String.Empty;

    public String PhotoName { get; set => NotifyToUIAndSetIfChanged(value, ref field); } = String.Empty;

    public String PhotoPath { get; set => NotifyToUIAndSetIfChanged(value, ref field); } = String.Empty;

    public ICommand Browse { get; set; } = null!;

    public ICommand Remove { get; set; } = null!;

    public ICommand Accept { get; set; } = null!;

    public Photo()
    {
        Browse = new FRelayCommand<Object>(Obj => Obj == null, async Obj => await BrowsePhoto());

        Remove = new FRelayCommand<Object>(Obj => Obj == null, async Obj => await RemovePhoto());

        Accept = new FRelayCommand<Object>(Obj => Obj == null, async Obj => await AnalyzePhoto());
    }

    async Task AnalyzePhoto()
    {
        if (YOLOSeg.Models.KeyYSModel != null)
        {
            (Mat, Mat, RotatedRect?, Dictionary<String, Object>?) Result = await Compute.Quantifies(YOLOSeg.Models.KeyYSModel, PhotoPath);

            using Mat DrawedPlotImage = Result.Item1;

            using Mat BinaryMaskImage = Result.Item2;

            Dictionary<String, Object>? DigitalData = Result.Item4;

            PlotImage = Miscell.ToWriteableBitmap(DrawedPlotImage);

            Shape = DigitalData?["Shape"].ToString()!;

            State = DigitalData?["State"].ToString()!;

            Orientation = DigitalData?["Orientation"].ToString()!;

            Double GetAspectRatioValue = Convert.ToDouble(DigitalData?["AspectRatio"]);

            Double GetDeformationValue = Convert.ToDouble(DigitalData?["Deformation"]);

            MaskImage = Compute.GetPlotMaskAsWriteableBitmap(BinaryMaskImage, Result.Item3, Shape);

            AspectRatio = (GetAspectRatioValue == -1) ? String.Empty : $"{(GetAspectRatioValue * 100):F9}%";

            Deformation = (GetDeformationValue == -1) ? String.Empty : $"{(GetDeformationValue * 100):F9}%";
        }
        else
        {
            await Message.ShowErrors("Không thể thực hiện phân tích hình ảnh do mô hình chưa được tải lên");
        }
    }

    async Task RemovePhoto()
    {
        if (await Message.ShowConfirm("Bạn có muốn xóa hình này?"))
        {
            PlotImage = null;

            MaskImage = null;

            PhotoPath = String.Empty;

            PhotoName = String.Empty;

            Shape = String.Empty;

            State = String.Empty;

            AspectRatio = String.Empty;

            Orientation = String.Empty;

            Deformation = String.Empty;
        }
    }

    public Task RegisterForDropEventForUIElement(Control UIElement)
    {
        UIElement.AddHandler(DragDrop.DropEvent, async (_, Event) =>
        {
            if (Event is DragEventArgs Drag && Drag.DataTransfer.TryGetFiles() is { } File)
            {
                if (File.Length == 1)
                {
                    String[] ExtList = [".PNG", ".JPG", ".JPEG", ".WEBP", ".BMP", ".TIFF"];

                    if (ExtList.Any(Tail => Tail.Equals(Path.GetExtension(File[0].Path.LocalPath), StringComparison.OrdinalIgnoreCase)))
                    {
                        PhotoName = File[0].Name;

                        PhotoPath = File[0].Path.LocalPath;
                    }
                    else
                    {
                        await Message.ShowErrors("Tệp tin không hợp lệ hoặc không đúng định dạng của ảnh");
                    }
                }
                else
                {
                    await Message.ShowErrors("Chức năng này chỉ cho phép nhận vào một hình ảnh duy nhất");
                }
            }
        });

        return Task.CompletedTask;
    }

    async Task BrowsePhoto()
    {
        IReadOnlyList<IStorageFile> Files = await Miscell.TopmostWindow()!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Tải dữ liệu",

            AllowMultiple = false,

            FileTypeFilter = [new FilePickerFileType("Dữ liệu") { Patterns = ["*.PNG", "*.JPG", "*.JPEG", "*.WEBP", "*.BMP", "*.TIFF"] }]
        });

        if (Files.Count >= 1)
        {
            PhotoName = Files[0].Name;

            PhotoPath = Files[0].Path.LocalPath;
        }
    }
}