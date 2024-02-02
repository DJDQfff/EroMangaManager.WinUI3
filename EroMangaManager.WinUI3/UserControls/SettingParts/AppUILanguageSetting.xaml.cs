// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls.SettingParts;

public sealed partial class AppUILanguageSetting : UserControl
{
    public SettingViewModel SettingVM { set; get; }

    public AppUILanguageSetting ()
    {
        this.InitializeComponent();
    }

    private void UILanguageSetting_Loaded (object sender , RoutedEventArgs e)
    {
        var combobox = sender as ComboBox;
        var items = SettingVM.AppConfig.General.DefaultAppUILanguage;

        if (items == DefaultAppUILanguageStrings.en)
        {
            combobox.SelectedIndex = 1;
        }
        else
        {
            combobox.SelectedIndex = 0;
        }
    }

    private void UILanguageSetting_SelectionChanged (object sender , SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        var index = comboBox.SelectedIndex;

        switch (index)
        {
            case 0:
                SettingVM.AppConfig.General.DefaultAppUILanguage = DefaultAppUILanguageStrings.zhCN;
                break;

            case 1:
                SettingVM.AppConfig.General.DefaultAppUILanguage = DefaultAppUILanguageStrings.en;
                break;
        }
    }
}