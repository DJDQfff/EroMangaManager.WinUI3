// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.ComponentModel;

namespace EroMangaManager.WinUI3.UserControls.MangaComplexDisplay;

public sealed partial class BriefInfo : UserControl, INotifyPropertyChanged
{
    private Manga mangaBook;

    public Manga Manga
    {
        get => mangaBook;
        set
        {
            mangaBook = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Manga)));
        }
    }

    public BriefInfo()
    {
        InitializeComponent();
    }

    public event PropertyChangedEventHandler PropertyChanged;
}