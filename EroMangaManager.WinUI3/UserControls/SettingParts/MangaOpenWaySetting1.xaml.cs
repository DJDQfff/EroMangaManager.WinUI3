// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls.SettingParts;

public sealed partial class MangaOpenWaySetting1 : UserControl
{
    public SettingViewModel SettingVM { set; get; }

    public MangaOpenWaySetting1 ()
    {
        this.InitializeComponent();
    }

    private async void MangaOpenWaySetting_SelectionChanged (object sender , SelectionChangedEventArgs e)
    {
        var control = sender as ComboBox;

        switch (control.SelectedIndex)
        {
            case 0:
                SettingVM.AppConfig.MangaOpenWaySetting.ReadMangaWay = ReadMangaWayStrings.InternalReadPage;
                break;

            case 1:
                SettingVM.AppConfig.MangaOpenWaySetting.ReadMangaWay = ReadMangaWayStrings.OSRelated;
                break;

            case 2:
                SettingVM.AppConfig.MangaOpenWaySetting.ReadMangaWay = ReadMangaWayStrings.UserSelected;
                break;

            case 3:
                var picker = new FileOpenPicker();
                picker.FileTypeFilter.Add(".exe");
                var handle = WindowNative.GetWindowHandle(App.Current.MainWindow);
                InitializeWithWindow.Initialize(picker , handle);
                var file = await picker.PickSingleFileAsync();
                if (file is not null)
                {
                    SettingVM.AppConfig.MangaOpenWaySetting.UserSelectedReadMangaExePath = file.Path;
                    SettingVM.AppConfig.MangaOpenWaySetting.PossibleExePaths += $"|{file.Path}";
                    ThirdComboItem.Content = file.DisplayName;
                    control.SelectedIndex = 2;
                    ThirdComboItem.IsEnabled = true;
                }
                break;
        }
    }

    private void MangaOpenWaySetting_Loaded (object sender , RoutedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        var way = SettingVM.AppConfig.MangaOpenWaySetting.ReadMangaWay;

        var filepath = SettingVM.AppConfig.MangaOpenWaySetting.UserSelectedReadMangaExePath;

        if (string.IsNullOrEmpty(filepath))
        {
            ThirdComboItem.Content = "Not defined";
            ThirdComboItem.IsEnabled = false;
        }
        else if (File.Exists(filepath))
        {
            ThirdComboItem.Content = Path.GetFileNameWithoutExtension(filepath);
        }
        else
        {
            ThirdComboItem.Content = Path.GetFileNameWithoutExtension(filepath);
            ThirdComboItem.IsEnabled = false;
        }
        comboBox.SelectedIndex = way switch
        {
            ReadMangaWayStrings.InternalReadPage => 0,
            ReadMangaWayStrings.OSRelated => 1,
            ReadMangaWayStrings.UserSelected => 2,
            _ => -1
        };
        ;
    }
}