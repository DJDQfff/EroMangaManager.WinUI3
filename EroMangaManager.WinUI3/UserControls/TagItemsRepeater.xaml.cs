using System.ComponentModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls;

public sealed partial class TagItemsRepeater : UserControl, INotifyPropertyChanged
{
    private Manga _manga;

    public event PropertyChangedEventHandler PropertyChanged;

    public Manga Manga
    {
        get => _manga;
        set
        {
            _manga = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Manga)));
        }
    }

    public TagItemsRepeater()
    {
        InitializeComponent();
    }
}