// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls;

public sealed partial class MangaBookDetailInfo : UserControl
{
    private MangaBook mangaBook;

    public MangaBook MangaBook
    {
        get => mangaBook;
        set
        {
            mangaBook = value;
            this.Bindings.Update();
        }
    }

    public MangaBookDetailInfo ()
    {
        InitializeComponent();
    }
}