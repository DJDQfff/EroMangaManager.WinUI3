// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.ComponentModel;

namespace EroMangaManager.WinUI3.UserControls;

public sealed partial class MangaBookBriefInfo : UserControl, INotifyPropertyChanged
{
    private MangaBook mangaBook;
    public MangaBook MangaBook
    {
        get => mangaBook;
        set
        {
            mangaBook = value;
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(nameof(MangaBook)));
        }
    }

    public MangaBookBriefInfo ()
    {
        InitializeComponent();
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
