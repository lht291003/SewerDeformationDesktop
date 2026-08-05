namespace SewerDeformationSoftware.Views;

public partial class PhotoUserView : UserControl { public PhotoUserView() { InitializeComponent(); ((Photo)DataContext!).RegisterForDropEventForUIElement(DropCard); } }