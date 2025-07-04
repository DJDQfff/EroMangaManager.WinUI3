// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
using System.ComponentModel;

namespace EroMangaManager.WinUI3.UserControls;

public sealed partial class RenameMangaBookByDragTag : UserControl, INotifyPropertyChanged
{
    private MangaBook mangaBook;

    public MangaBook MangaBook
    {
        get => mangaBook;
        set
        {
            mangaBook = value;
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(nameof(MangaBook)));
            order.Sources = BracketBasedStringParser.SplitByBrackets_KeepBracket(
                MangaBook.FileDisplayName
            ); //value.MangaTagsIncludedInFileName;
        }
    }

    public event Action<MangaBook> NameChanged;
    public event PropertyChangedEventHandler PropertyChanged;

    public RenameMangaBookByDragTag ()
    {
        this.InitializeComponent();
    }

    private void SingleMangaBookRename_New (object sender , RoutedEventArgs e)
    {
        var text = newnameBox.Text;
        StorageOperation.RenameMange(MangaBook , text);
        NameChanged?.Invoke(MangaBook);
    }

    private void newnameBox_TextChanged (object sender , TextChangedEventArgs e)
    {
        var text = newnameBox.Text;
        StorageOperation.RenameMange(MangaBook , text);
        NameChanged?.Invoke(MangaBook);
    }

    [Obsolete("注意这里出过bug，调用loaded函数的时候，Mangabook可能属性还没刷新" , true)]
    private void UserControl_Loaded (object sender , RoutedEventArgs e)
    {
        // 注意这里出过bug，调用loaded函数的时候，mangabook可能属性还没刷新
        //var a = BracketBasedStringParser.SplitByBrackets_Reserve(MangaBook.FileDisplayName);
        //textblock.Text = MangaBook.FileDisplayName;
        //order.Sources = a;
        //orde.ItemsSource = a;
    }
}
