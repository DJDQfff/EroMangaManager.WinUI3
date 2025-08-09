// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.ComponentModel;
using System.Threading.Tasks;

namespace EroMangaManager.WinUI3.UserControls;

public sealed partial class CoverWithContextFlyout : UserControl, INotifyPropertyChanged
{
    Manga source;

    public event PropertyChangedEventHandler PropertyChanged;

    public Manga Source
    {
        get => source;
        set
        {
            source = value;
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(nameof(Source)));
        }
    }

    public CoverWithContextFlyout ()
    {
        InitializeComponent();
    }

    private void UserControl_DoubleTapped (object sender , DoubleTappedRoutedEventArgs e)
    {
        MangaCommands.Instance.OpenManga.Execute(Source);
    }

    private void moveto_Loaded (object sender , RoutedEventArgs e)
    {
        moveto.Items.Clear();
        var ways = App.Current.GlobalViewModel.MangaFolders;
        foreach (var way in ways)
        {
            var item = new MenuFlyoutItem { Text = way.FolderPath };
            moveto.Items.Add(item);

            if (way.FolderPath == Source.FolderPath)
            {
                item.IsEnabled = false;
                continue;
            }
            item.Click += (object sender , RoutedEventArgs e) =>
            {
                _ = MangaFileOperation.MoveManga(
                     Source ,
                     way.FolderPath ,
                     null
                 );
                App.Current.GlobalViewModel.PlaceInCorrectGroup(Source);
            };
        }
    }

    private void image_ImageFailed (object sender , ExceptionRoutedEventArgs e)
    {
        image.Source = CoverHelper.ErrorCoverImage;
    }
}
