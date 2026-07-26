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
}