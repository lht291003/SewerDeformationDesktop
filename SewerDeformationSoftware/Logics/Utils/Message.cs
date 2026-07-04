namespace SewerDeformationSoftware.Logics.Utils;

public class Message
{
    public static async Task ShowErrors(String Message) => await MessageBoxManager.GetMessageBoxStandard("Thông báo", Message, ButtonEnum.Ok, Icon.Error).ShowWindowDialogAsync(Interop.TopmostWindow()!);

    public static async Task ShowSuccess(String Message) => await MessageBoxManager.GetMessageBoxStandard("Thông báo", Message, ButtonEnum.Ok, Icon.Info).ShowWindowDialogAsync(Interop.TopmostWindow()!);

    public static async Task<Boolean> ShowConfirm(String Message)

                              => await MessageBoxManager.GetMessageBoxStandard("Thông báo", Message, ButtonEnum.YesNo, Icon.Question).ShowWindowDialogAsync(Interop.TopmostWindow()!) == ButtonResult.Yes;
}