// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.ComponentModel;

namespace EroMangaManager.WinUI3.UserControls.MangaBasicInfo;

public sealed partial class CoverWithContextFlyout : UserControl, INotifyPropertyChanged
{
    private Manga source;

    public event PropertyChangedEventHandler PropertyChanged;

    public Manga Source
    {
        get => source;
        set
        {
            source = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Source)));
        }
    }

    public CoverWithContextFlyout()
    {
        InitializeComponent();
    }

    private void UserControl_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        MangaCommands.Instance.OpenManga.Execute(Source);
    }

    private void Moveto_Loaded(object sender, RoutedEventArgs e)
    {
        moveto.Items.Clear();
        var ways = App.Current.GlobalViewModel.MangaFolders;
        foreach (var way in ways)
        {
            var item = new MenuFlyoutItem { Text = way.FolderPath };
            moveto.Items.Add(item);

            if (way.FolderPath == Source.FolderPath || Path.GetPathRoot(way.FolderPath).ToLower() != Path.GetPathRoot(Source.FilePath).ToLower())
            {
                item.IsEnabled = false;
                continue;
            }
            item.Click += async (sender, e) =>
            {
                try
                {
                    string newpath = await Task.Run(() => MangaFileOperation.MoveManga(Source, way.FolderPath, null));
                    Source.FilePath = newpath;

                    App.Current.GlobalViewModel.PlaceInCorrectGroup(Source);
                }
                catch (UnauthorizedAccessException) { App.Current.GlobalViewModel.AccessDenied(); }
                catch (System.IO.IOException ex)
                {
                    App.Current.GlobalViewModel.AccessDenied();
                }
            };
        }
    }

    private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
    {
        image.Source = CoverHelper.ErrorCoverImage;
    }
}