// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls.SettingParts;
public sealed partial class WhetherShowDialogBeforeDeleteSetting : UserControl
{
    public SettingViewModel SettingVM { get; set; }
    public WhetherShowDialogBeforeDeleteSetting ()
    {
        this.InitializeComponent();
    }
    private void DeleteConfirmSetting_Toggled (object sender , RoutedEventArgs e)
    {
        var toggleSwitch = sender as ToggleSwitch;

        SettingVM.AppConfig.General.WhetherShowDialogBeforeDelete = toggleSwitch.IsOn;
    }

    private void DeleteConfirmSetting_Loaded (object sender , RoutedEventArgs e)
    {
        var toggleSwitch = sender as ToggleSwitch;

        toggleSwitch.IsOn = SettingVM.AppConfig.General.WhetherShowDialogBeforeDelete;
    }
}
