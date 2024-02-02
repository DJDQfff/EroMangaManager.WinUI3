// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls.SettingParts;

public sealed partial class WhetherPickSubFolderSetting : UserControl
{
    public SettingViewModel SettingVM { set; get; }

    public WhetherPickSubFolderSetting ()
    {
        this.InitializeComponent();
    }

    private void WhetherPickSubFolder_Loaded (object sender , RoutedEventArgs e)
    {
        WhetherPickSubFolder.IsOn = SettingVM.AppConfig.General.WhetherPickSubFolder;
    }

    private void WhetherPickSubFolder_Toggled (object sender , RoutedEventArgs e)
    {
        SettingVM.AppConfig.General.WhetherPickSubFolder = WhetherPickSubFolder.IsOn;
    }
}