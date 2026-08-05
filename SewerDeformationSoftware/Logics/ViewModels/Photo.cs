namespace SewerDeformationSoftware.Logics.ViewModels;

public class Photo : Basis
{
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

    public ICommand Dragop { get; set; } = null!;

    public Photo()
    {
        Browse = new FRelayCommand<Object>(Obj => Obj == null, async Obj => await BrowsePhoto());

        Remove = new FRelayCommand<Object>(Obj => Obj == null, async Obj => await RemovePhoto());
    }

    async Task RemovePhoto()
    {
        if (await Message.ShowConfirm("Bạn có muốn xóa hình ảnh này?"))
        {
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