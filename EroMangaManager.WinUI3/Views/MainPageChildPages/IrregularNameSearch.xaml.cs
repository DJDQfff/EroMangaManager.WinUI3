// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.Views.MainPageChildPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IrregularNameSearch : Page
    {
        readonly ObservableCollection<MangaBook> books = new();
        public IrregularNameSearch ()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo (NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            foreach (var book in App.Current.GlobalViewModel.MangaList)
            {
                if (BracketBasedStringParser.CorrectBracketPairConut(book.FileDisplayName) == -1)
                {
                    books.Add(book);
                }
            }
        }
    }
}
