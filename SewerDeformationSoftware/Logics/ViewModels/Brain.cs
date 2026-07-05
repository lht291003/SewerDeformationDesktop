namespace SewerDeformationSoftware.Logics.ViewModels;

public class Brain : Basis
{
    public ICommand ControlDisplayCommand { get; set; }

    public Brain()
    {
        ControlDisplayCommand = new ARelayCommand<List<Object>>(Object => Object != null, Package => ShowWorkForm(Package));
    }

    Task LoadOrDisplayControlWithName(Panel Screen, UserControl View)
    {
        Screen.Children.ToList().ForEach(UV => UV.IsVisible = false);

        UserControl? Existing = Screen.Children.OfType<UserControl>().FirstOrDefault(UV => UV.GetType() == View.GetType());

        if (Existing != null)
        {
            Existing.IsVisible = true;
        }
        else
        {
            Screen.Children.Add(View);
        }

        return Task.CompletedTask;
    }

    Task ShowWorkForm(List<Object> Package)
    {
        Panel Displays = Package.OfType<Panel>().FirstOrDefault()!;

        ListBox Menu = Package.OfType<ListBox>().FirstOrDefault()!;

        String GetName = (Menu.SelectedItem as ListBoxItem)?.Name!;

        switch (GetName)
        {
            case "BuildView":

                LoadOrDisplayControlWithName(Displays, new BuildUserView());

                break;

            case "PhotoView":

                LoadOrDisplayControlWithName(Displays, new PhotoUserView());

                break;

            case "VideoView":

                LoadOrDisplayControlWithName(Displays, new VideoUserView());

                break;
        }

        return Task.CompletedTask;
    }
}