

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls;

public sealed partial class CoverWithContextFlyout : UserControl
{
    public MangaBook MangaBook { set; get; }
    public CoverWithContextFlyout ()
    {
        this.InitializeComponent();
    }
}
