// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls.SettingParts;

public sealed partial class MangaOpenWaySetting2 : UserControl
{
    public SettingViewModel SettingVM { set; get; }

    public MangaOpenWaySetting2 ()
    {
        this.InitializeComponent();
    }

    private void RadioButtons_Loaded (object sender , RoutedEventArgs e)
    {
        if (sender is RadioButtons radioButtons)
        {
        }
    }

    private void Button_Click (object sender , RoutedEventArgs e)
    {
    }
}