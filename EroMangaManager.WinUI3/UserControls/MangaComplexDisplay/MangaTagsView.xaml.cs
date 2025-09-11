// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.ComponentModel;

using Microsoft.UI.Xaml.Controls;

using Windows.ApplicationModel.DataTransfer;

namespace EroMangaManager.WinUI3.UserControls.MangaComplexDisplay;

public sealed partial class MangaTagsView : UserControl, INotifyPropertyChanged
{
    private Manga mangaBook;

    public event PropertyChangedEventHandler PropertyChanged;

    public Manga Manga
    {
        get => mangaBook;
        set
        {
            mangaBook = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Manga)));
        }
    }

    public MangaTagsView()
    {
        InitializeComponent();
    }
}