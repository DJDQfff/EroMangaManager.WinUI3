// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls.SettingParts;
public sealed partial class ThemeSetting : UserControl
{
    public SettingViewModel SettingVM { set; get; }

    public ThemeSetting ()
    {
        this.InitializeComponent();
    }

    private void ThemeSetting_Loaded (object sender , RoutedEventArgs e)
    {
        var combo = sender as ComboBox;
        combo.SelectedIndex = SettingVM.AppConfig.General.Theme;
    }

    private void ThemeSetting_SelectionChanged (object sender , SelectionChangedEventArgs e)
    {
        var combo = sender as ComboBox;
        SettingVM.AppConfig.General.Theme = combo.SelectedIndex;
    }
}
