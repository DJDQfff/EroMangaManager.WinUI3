// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using Microsoft.EntityFrameworkCore.Internal;

namespace EroMangaManager.WinUI3.UserControls.SettingParts;

public sealed partial class MangaOpenWaySetting2 : UserControl
{
    public SettingViewModel SettingVM { set; get; }

    public MangaOpenWaySetting2 ()
    {
        this.InitializeComponent();
    }

    private async void Button_Click (object sender , RoutedEventArgs e)
    {
        var picker = new FileOpenPicker();
        picker.FileTypeFilter.Add(".exe");
        var handle = WindowNative.GetWindowHandle(App.Current.MainWindow);
        InitializeWithWindow.Initialize(picker , handle);
        var file = await picker.PickSingleFileAsync();
        if (file is not null)
        {
            SettingVM.AddExePath(file.Path);
        }
    }

    private void MangaOpenWays_SelectionChanged (object sender , SelectionChangedEventArgs e)
    {
        if (MangaOpenWays.SelectedItem is string item)
        {
            SettingVM.SetExePath(item);
        }
    }

    private void MangaOpenWays_Loaded (object sender , RoutedEventArgs e)
    {
        var exepath = SettingVM.AppConfig.MangaOpenWaySetting.UserSelectedReadMangaExePath;
        var index = SettingVM.ExePaths.IndexOf(exepath);
        MangaOpenWays.SelectedIndex = index;
    }

    private void MenuFlyoutItem_Click (object sender , RoutedEventArgs e)
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