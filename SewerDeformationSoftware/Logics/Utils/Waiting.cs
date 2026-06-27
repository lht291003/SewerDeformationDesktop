namespace SewerDeformationSoftware.Logics.Utils;

public class Waiting
{
    public static Lazy<WaitIndicator> CircleBar { get; } = new(() => new WaitIndicator());

    public static void ShowProgressRing()
    {
        WaitIndicator Instance = CircleBar.Value;

        Window Parents = Interop.TopmostWindow();

        Instance.Show(Parents);

        Instance.Owner!.IsEnabled = false;
    }

    public static void HideProgressRing()
    {
        WaitIndicator Instance = CircleBar.Value;

        if (Instance.IsVisible)
        {
            Instance.Owner!.IsEnabled = (1 == 1);

            Instance.Hide();
        }
    }
}