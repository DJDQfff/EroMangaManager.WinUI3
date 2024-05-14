// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace EroMangaManager.WinUI3.UserControls;
[INotifyPropertyChanged]
public sealed partial class RenameMangaBookByDragTag : UserControl
{
    [ObservableProperty]
    private MangaBook mangaBook;
    public RenameMangaBookByDragTag ()
    {
        this.InitializeComponent();
    }
    private void SingleMangaBookRename_New (object sender , RoutedEventArgs e)
    {

        var text = newnameBox.Text;

    }



    private void newnameBox_TextChanged (object sender , TextChangedEventArgs e)
    {
        var book = newnameBox.DataContext as MangaBook;

        var text = newnameBox.Text;
        // TODO 这有严重bug，每次文字切换，会直接改名
        //TrySetNewName(book , text);

    }

    private void UserControl_Loaded (object sender , RoutedEventArgs e)
    {
        var manga = order.DataContext as MangaBook;
        var items = NameParser.SplitByBlank(manga.FileDisplayName);
        order.ItemsSource = items;

    }


}
