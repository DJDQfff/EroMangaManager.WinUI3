// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.Collections.Specialized;
using System.ComponentModel;

namespace EroMangaManager.WinUI3.UserControls;

public sealed partial class MangaBookCoverTags : UserControl, INotifyPropertyChanged
{
    private MangaBook mangaBook;

    public event PropertyChangedEventHandler PropertyChanged;

    public MangaBook MangaBook
    {
        get => mangaBook;
        set
        {
            mangaBook = value;
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(nameof(MangaBook)));
        }
    }

    public MangaBookCoverTags ()
    {
        this.InitializeComponent();
    }
}
