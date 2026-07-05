namespace SewerDeformationSoftware.Logics.ViewModels;

public class Build : Basis
{
    public String ModelName { get; set => NotifyToUIAndSetIfChanged(value, ref field); } = String.Empty;

    public String ModelPath { get; set => NotifyToUIAndSetIfChanged(value, ref field); } = String.Empty;

    public String GetDevice { get; set => NotifyToUIAndSetIfChanged(value, ref field); } = String.Empty;

    public ICommand Browse { get; set; } = null!;

    public ICommand Remove { get; set; } = null!;

    public ICommand Upload { get; set; } = null!;

    public Build()
    {
        Browse = new FRelayCommand<Object>(Obj => Obj == null, async Obj => await BrowseYOLOSeg());

        Remove = new FRelayCommand<Object>(Obj => Obj == null, async Obj => await RemoveYOLOSeg());

        Upload = new FRelayCommand<Object>(Obj => CanAccept(), async Obj => await UploadYOLOSeg());
    }

    Boolean CanAccept() => (!String.IsNullOrEmpty(ModelPath) && !String.IsNullOrEmpty(GetDevice));

    async Task BrowseYOLOSeg()
    {
        IStorageProvider SP = Interop.TopmostWindow()!.StorageProvider;

        IReadOnlyList<IStorageFile> Files = await SP.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Tải mô hình",

            AllowMultiple = false,

            FileTypeFilter = [new FilePickerFileType("YOLO Model ONNX") { Patterns = ["*.ONNX"] }]
        });

        if (Files.Count >= 1)
        {
            ModelName = Files[0].Name;

            ModelPath = Files[0].Path.LocalPath;
        }
    }

    async Task RemoveYOLOSeg()
    {
        if (await Message.ShowConfirm("Bạn có muốn xóa mô hình này không?"))
        {
            if (YOLOSeg.Models.IsRunning)
            {
                await Message.ShowErrors("Không thể xóa, mô hình đang chạy");
            }
            else
            {
                ModelPath = String.Empty;

                ModelName = String.Empty;

                YOLOSeg.Models.KeyYSModel = null;

                YOLOSeg.Models.DeviceType = null;
            }
        }
    }

    async Task UploadYOLOSeg()
    {
        if (YOLOSeg.Models.IsRunning)
        {
            await Message.ShowErrors("Không thể tải, mô hình cũ đang sử dụng");
        }
        else
        {
            Waiting.ShowProgressRing();

            YOLOSeg.Models.DeviceType = GetDevice;

            YOLOSeg.Models.KeyYSModel = await Task.Run(() =>
            {
                if (YOLOSeg.Models.DeviceType.Equals("CPU"))
                {
                    return new YoloSharp(new ExecutionProviderCPU(ModelPath));
                }

                if (OperatingSystem.IsLinux())
                {
                    return new YoloSharp(new ExecutionProviderCUDA(ModelPath));
                }

                return new YoloSharp(new ExecutionProviderDirectML(ModelPath));
            });

            Waiting.HideProgressRing();

            await Message.ShowSuccess($"Mô hình {ModelName} đã được tải lên");
        }
    }

    public Task RegisterForDropEventForUIElement(Control UIElement)
    {
        UIElement.AddHandler(DragDrop.DropEvent, async (_, Event) =>
        {
            if (Event is DragEventArgs Drag && Drag.DataTransfer.TryGetFiles() is { } DroppedFiles)
            {
                if (DroppedFiles.Length == 1)
                {
                    StringComparison TheSame = StringComparison.OrdinalIgnoreCase;

                    if (Path.GetExtension(DroppedFiles[0].Path.LocalPath).Equals(".ONNX", TheSame))
                    {
                        ModelName = DroppedFiles[0].Name;

                        ModelPath = DroppedFiles[0].Path.LocalPath;
                    }
                    else
                    {
                        await Message.ShowErrors("Tệp tin không hợp lệ hoặc không đúng định dạng .ONNX");
                    }
                }
                else
                {
                    await Message.ShowErrors("Chức năng này chỉ cho phép nhận vào một tệp tin duy nhất");
                }
            }
        });

        return Task.CompletedTask;
    }
}