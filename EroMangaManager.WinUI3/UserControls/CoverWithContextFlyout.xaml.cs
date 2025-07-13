// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.ComponentModel;

namespace EroMangaManager.WinUI3.UserControls;

public sealed partial class CoverWithContextFlyout : UserControl, INotifyPropertyChanged
{
    MangaBook source;

    public event PropertyChangedEventHandler PropertyChanged;

    public MangaBook Source
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
        this.InitializeComponent();
    }

    private void UserControl_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        Commands.MangaBookCommands.Instance.OpenManga.Execute(Source);
    }

    private void moveto_Loaded(object sender, RoutedEventArgs e)
    {
        moveto.Items.Clear();
        var ways = App.Current.GlobalViewModel.MangaFolders
            .SkipWhile(x => x.FolderPath == Source.FolderPath)
            .ToList();

        foreach (var way in ways)
        {
            var item = new MenuFlyoutItem { Text = way.FolderPath };
            item.Click += (sender, e) =>
            {
                EroMangaManager.Core.IOOperation.MangaBookFileOperation.MoveManga(
                    Source,
                    way.FolderPath,
                    null
                );
                App.Current.GlobalViewModel.PlaceInCorrectGroup(Source);
            };
            moveto.Items.Add(item);
        }
    }
}
