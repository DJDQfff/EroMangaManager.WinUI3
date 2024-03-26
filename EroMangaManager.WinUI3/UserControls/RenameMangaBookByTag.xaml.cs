
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls;
public sealed partial class RenameMangaBookByTag : UserControl
{
    private MangaBook manga;
    ObservableCollection<StateTrigger> tags = new();
    public MangaBook MangaBook
    {
        get => manga;
        set
        {
            manga = value;

        }
    }
    public RenameMangaBookByTag ()
    {
        this.InitializeComponent();
    }
}
