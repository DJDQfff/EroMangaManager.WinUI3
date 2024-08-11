// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace EroMangaManager.WinUI3.UserControls;

[INotifyPropertyChanged]
public sealed partial class RenameMangaBookByDragTag : UserControl
{
    [ObservableProperty]
    private MangaBook mangaBook;
    public event Action<MangaBook> NameChanged;

    partial void OnMangaBookChanged(MangaBook value)
    {
        order.Sources = NameParser.SplitByBrackets_Reserve(MangaBook.FileDisplayName); //value.MangaTagsIncludedInFileName;
    }

    public RenameMangaBookByDragTag()
    {
        this.InitializeComponent();
    }

    private void SingleMangaBookRename_New(object sender, RoutedEventArgs e)
    {
        var text = newnameBox.Text;
        StorageOperation.RenameMange(MangaBook, text);
        NameChanged?.Invoke(MangaBook);
    }

    private void newnameBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var text = newnameBox.Text;
        StorageOperation.RenameMange(MangaBook, text);
        NameChanged?.Invoke(MangaBook);
    }

    [Obsolete("ע���������bug������loaded������ʱ��Mangabook�������Ի�ûˢ��", true)]
    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        // ע���������bug������loaded������ʱ��mangabook�������Ի�ûˢ��
        //var a = NameParser.SplitByBrackets_Reserve(MangaBook.FileDisplayName);
        //textblock.Text = MangaBook.FileDisplayName;
        //order.Sources = a;
        //orde.ItemsSource = a;
    }
}
