
using MyLibrary.Standard20;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.Views.MainPageChildPages;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class RemoveRepeatTags2 : Page
{
    private ObservableCollection<MangaBook> RepaetBooks { get; } = new();

    public RemoveRepeatTags2 ()
    {
        this.InitializeComponent();
        foreach (var book in App.Current.GlobalViewModel.MangaList)
        {
            if (book.MangaTagsIncludedInFileName.ContainRepeat())
            {
                RepaetBooks.Add(book);
            }

        }

    }

    void RemoveIfTagRepeat (MangaBook book)
    {
        if (!book.MangaTagsIncludedInFileName.ContainRepeat())
        {
            _ = RepaetBooks.Remove(book);
        }

    }

}
