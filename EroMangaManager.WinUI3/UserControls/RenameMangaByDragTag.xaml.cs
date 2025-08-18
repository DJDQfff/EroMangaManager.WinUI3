// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
using System.ComponentModel;

namespace EroMangaManager.WinUI3.UserControls;

public sealed partial class RenameMangaByDragTag : UserControl, INotifyPropertyChanged
{
    private Manga mangaBook;

    public Manga Manga
    {
        get => mangaBook;
        set
        {
            mangaBook = value;
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(nameof(Manga)));
            order.Sources = BracketBasedStringParser.SplitByBrackets_KeepBracket(
                Manga.FileDisplayName
            ); //value.Tags;
        }
    }

    public event Action<Manga> NameChanged;
    public event PropertyChangedEventHandler PropertyChanged;

    public RenameMangaByDragTag ()
    {
        InitializeComponent();
    }

    private void SingleMangaRename_New (object sender , RoutedEventArgs e)
    {
        var text = newnameBox.Text;
        var result = MangaFileOperation.MoveManga(Manga , null , text);
        if (result is not null)
        {
            Manga.FilePath = result;

        }

        NameChanged?.Invoke(Manga);
    }


    [Obsolete("注意这里出过bug，调用loaded函数的时候，Mangabook可能属性还没刷新" , true)]
    private void UserControl_Loaded (object sender , RoutedEventArgs e)
    {
        // 注意这里出过bug，调用loaded函数的时候，mangabook可能属性还没刷新
        //var a = BracketBasedStringParser.SplitByBrackets_Reserve(Manga.FileDisplayName);
        //textblock.Text = Manga.FileDisplayName;
        //order.Sources = a;
        //orde.ItemsSource = a;
    }
}
