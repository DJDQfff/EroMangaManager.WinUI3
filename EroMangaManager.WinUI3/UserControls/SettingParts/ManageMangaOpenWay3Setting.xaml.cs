// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls.SettingParts
{
    public sealed partial class ManageMangaOpenWay3Setting : UserControl
    {
        public SettingViewModel SettingVM { set; get; }

        public ManageMangaOpenWay3Setting()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".exe");
            var handle = WindowNative.GetWindowHandle(App.Current.MainWindow);
            InitializeWithWindow.Initialize(picker, handle);
            var file = await picker.PickSingleFileAsync();
            if (file is not null)
            {
                SettingVM.AddExePath(file.Path);
            }
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem item)
            {
                if (item.DataContext is string exepath)
                {
                    SettingVM.RemoveExePath(exepath);
                }
            }
        }
    }
}
