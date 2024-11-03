// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.Views.MainPageChildPages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MangasWithoutBrackets : Page
{
    private ObservableCollection<MangaBook> RepaetBooks { get; } = new();

    public MangasWithoutBrackets()
    {
        this.InitializeComponent();
        foreach (var book in App.Current.GlobalViewModel.MangaList)
        {
            if (!BracketBasedStringParser.ContainAnyBrackets(book.FileDisplayName)) //.MangaTagsIncludedInFileName.ContainRepeat())
            {
                //book.FileDisplayName.ToArray().Contain(,)
                RepaetBooks.Add(book);
            }
        }
    }

    void RemoveIfTagRepeat(MangaBook book)
    {
        if (BracketBasedStringParser.ContainAnyBrackets(book.FileDisplayName))
        {
            _ = RepaetBooks.Remove(book);
        }
    }
}
