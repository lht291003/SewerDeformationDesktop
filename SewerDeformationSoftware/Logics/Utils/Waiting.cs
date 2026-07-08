namespace SewerDeformationSoftware.Logics.Utils;

public class Waiting
{
    public static Lazy<WaitIndicator> CircleBar { get; } = new(() => new WaitIndicator());

    public static void ShowProgressRing()
    {
        WaitIndicator Instance = CircleBar.Value;

        Window GetDad = Interop.TopmostWindow()!;

        Instance.Show(GetDad);

        Instance.Owner!.IsEnabled = false;
    }

    public static void HideProgressRing()
    {
        WaitIndicator Instance = CircleBar.Value;

        if (Instance.IsVisible)
        {
            Instance.Owner!.IsEnabled = (0 == 0);

            Instance.Hide();
        }
    }
}