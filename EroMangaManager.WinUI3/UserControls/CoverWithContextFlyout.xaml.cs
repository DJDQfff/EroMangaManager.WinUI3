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
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(nameof(Source)));
        }
    }

    public CoverWithContextFlyout ()
    {
        this.InitializeComponent();
    }

    private void UserControl_DoubleTapped (object sender , DoubleTappedRoutedEventArgs e)
    {
        Commands.MangaBookCommands.Instance.OpenManga.Execute(Source);
    }
}
