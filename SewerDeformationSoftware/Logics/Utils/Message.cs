namespace SewerDeformationSoftware.Logics.Utils;

public class Message
{
    public static async void ShowErrors(String Message)

                                     => await Dispatcher.UIThread.InvokeAsync(async () => await MessageBoxManager.GetMessageBoxStandard("Thông báo", Message, ButtonEnum.Ok, Icon.Error).ShowWindowDialogAsync(Interop.TopmostWindow()));

    public static async void ShowSuccess(String Message)

                                     => await Dispatcher.UIThread.InvokeAsync(async () => await MessageBoxManager.GetMessageBoxStandard("Thông báo", Message, ButtonEnum.Ok, Icon.Info).ShowWindowDialogAsync(Interop.TopmostWindow()));

    public static async Task<Boolean> ShowConfirm(String Message)

        => await Dispatcher.UIThread.InvokeAsync(async () => (await MessageBoxManager.GetMessageBoxStandard("Thông báo", Message, ButtonEnum.YesNo, Icon.Question).ShowWindowDialogAsync(Interop.TopmostWindow())) == ButtonResult.Yes);
}