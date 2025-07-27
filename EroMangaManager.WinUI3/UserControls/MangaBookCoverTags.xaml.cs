// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.Collections.Specialized;
using System.ComponentModel;

namespace EroMangaManager.WinUI3.UserControls;

public sealed partial class MangaCoverTags : UserControl, INotifyPropertyChanged
{
    private Manga mangaBook;

    public event PropertyChangedEventHandler PropertyChanged;

    public Manga Manga
    {
        get => mangaBook;
        set
        {
            mangaBook = value;
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(nameof(Manga)));
        }
    }

    public MangaCoverTags ()
    {
        this.InitializeComponent();
    }
}
