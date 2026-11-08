namespace SewerDeformationSoftware.Logics.ViewModels;

public class Video : Basis
{
    public String VideoName { get; set => NotifyToUIAndSetIfChanged(value, ref field); } = String.Empty;

    public String VideoPath { get; set => NotifyToUIAndSetIfChanged(value, ref field); } = String.Empty;

    public ICommand Browse { get; set; } = null!;

    public ICommand Remove { get; set; } = null!;

    public ICommand Accept { get; set; } = null!;

    public Video()
    {
        Browse = new FRelayCommand<Object>(Obj => Obj == null, async Obj => await BrowseVideo());

        Remove = new FRelayCommand<Object>(Obj => Obj == null, async Obj => await RemoveVideo());
    }

    async Task RemoveVideo()
    {
        if (await Message.ShowConfirm("Bạn có muốn xóa video này?"))
        {
            if (YOLOSeg.Models.IsRunning)
            {
                await Message.ShowErrors("Không thể xóa, vui lòng chờ video suy luận hoàn tất!");
            }
            else
            {
                VideoPath = String.Empty;

                VideoName = String.Empty;
            }
        }
    }

    async Task BrowseVideo()
    {
        IStorageProvider GetStorageProvider = Miscell.TopmostWindow()!.StorageProvider;

        IReadOnlyList<IStorageFile> Files = await GetStorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            FileTypeFilter = [new FilePickerFileType("Video / Clip") { Patterns = ["*.MP4", "*.MOV", "*.WEBM"] }],

            Title = "Tải dữ liệu",

            AllowMultiple = false,
        });

        if (Files.Count >= 1)
        {
            VideoName = Files[0].Name;

            VideoPath = Files[0].Path.LocalPath;
        }
    }
}