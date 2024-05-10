// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls;
[INotifyPropertyChanged]
public sealed partial class MangaBookBriefInfo : UserControl
{
    [ObservableProperty]
    private MangaBook mangaBook;

    public MangaBookBriefInfo ()
    {
        InitializeComponent();
    }
}