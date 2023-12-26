// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace EroMangaManager.WinUI3.Views.SettingPageChildPages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CommonSettingPage : Page
    {
        /// <summary>
        /// 一般设置页面
        /// </summary>
        public CommonSettingPage ()
        {
            InitializeComponent();
        }

        private void DeleteConfirmSetting_Toggled (object sender , RoutedEventArgs e)
        {
            var toggleSwitch = sender as ToggleSwitch;

            App.Current.AppConfig.General.WhetherShowDialogBeforeDelete = toggleSwitch.IsOn;
        }

        private void DeleteModeSetting_Toggled (object sender , RoutedEventArgs e)
        {
            var toggleSwitch = sender as ToggleSwitch;

            App.Current.AppConfig.General.StorageDeleteOption = toggleSwitch.IsOn;
        }

        private void DeleteConfirmSetting_Loaded (object sender , RoutedEventArgs e)
        {
            var toggleSwitch = sender as ToggleSwitch;

            toggleSwitch.IsOn = App.Current.AppConfig.General.WhetherShowDialogBeforeDelete;
        }

        private void DeleteModeSetting_Loaded (object sender , RoutedEventArgs e)
        {
            var toggleSwitch = sender as ToggleSwitch;
            toggleSwitch.IsOn = App.Current.AppConfig.General.StorageDeleteOption;
        }

        private async void MangaOpenWaySetting_SelectionChanged (object sender , SelectionChangedEventArgs e)
        {
            var control = sender as ComboBox;

            switch (control.SelectedIndex)
            {
                case 0:
                    App.Current.AppConfig.MangaOpenWaySetting.ReadMangaWay = ReadMangaWayStrings.InternalReadPage;
                    break;

                case 1:
                    App.Current.AppConfig.MangaOpenWaySetting.ReadMangaWay = ReadMangaWayStrings.OSRelated;
                    break;

                case 2:
                    App.Current.AppConfig.MangaOpenWaySetting.ReadMangaWay = ReadMangaWayStrings.UserSelected;
                    break;

                case 3:
                    var picker = new FileOpenPicker();
                    picker.FileTypeFilter.Add(".exe");
                    var handle = WindowNative.GetWindowHandle(App.Current.MainWindow);
                    InitializeWithWindow.Initialize(picker , handle);
                    var file = await picker.PickSingleFileAsync();
                    if (file is not null)
                    {
                        App.Current.AppConfig.MangaOpenWaySetting.UserSelectedReadMangaExePath = file.Path;
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
            var way = App.Current.AppConfig.MangaOpenWaySetting.ReadMangaWay;

            var filepath = App.Current.AppConfig.MangaOpenWaySetting.UserSelectedReadMangaExePath;

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

        private void UILanguageSetting_SelectionChanged (object sender , SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var index = comboBox.SelectedIndex;

            switch (index)
            {
                case 0:
                    App.Current.AppConfig.General.DefaultAppUILanguage = DefaultAppUILanguageStrings.zhCN;
                    break;

                case 1:
                    App.Current.AppConfig.General.DefaultAppUILanguage = DefaultAppUILanguageStrings.en;
                    break;
            }
        }

        private void UILanguageSetting_Loaded (object sender , RoutedEventArgs e)
        {
            var combobox = sender as ComboBox;
            var items = App.Current.AppConfig.General.DefaultAppUILanguage;

            if (items == DefaultAppUILanguageStrings.en)
            {
                combobox.SelectedIndex = 1;
            }
            else
            {
                combobox.SelectedIndex = 0;
            }
        }

        private void ThemeSetting_Loaded (object sender , RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.SelectedIndex = App.Current.AppConfig.General.Theme;
        }

        private void ThemeSetting_SelectionChanged (object sender , SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            App.Current.AppConfig.General.Theme = combo.SelectedIndex;
        }

        private void WhetherPickSubFolder_Loaded (object sender , RoutedEventArgs e)
        {
            WhetherPickSubFolder.IsOn = App.Current.AppConfig.General.WhetherPickSubFolder;
        }

        private void WhetherPickSubFolder_Toggled (object sender , RoutedEventArgs e)
        {
            App.Current.AppConfig.General.WhetherPickSubFolder = WhetherPickSubFolder.IsOn;
        }
    }
}