// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls.SettingParts;
public sealed partial class DeleteModeSetting : UserControl
{
    public SettingViewModel SettingVM { set; get; }
    public DeleteModeSetting ()
    {
        this.InitializeComponent();
    }

    private void DeleteModeSetting_Toggled (object sender , RoutedEventArgs e)
    {
        var toggleSwitch = sender as ToggleSwitch;

        SettingVM.AppConfig.General.StorageDeleteOption = toggleSwitch.IsOn;
    }
    private void DeleteModeSetting_Loaded (object sender , RoutedEventArgs e)
    {
        var toggleSwitch = sender as ToggleSwitch;
        toggleSwitch.IsOn = SettingVM.AppConfig.General.StorageDeleteOption;
    }

}
