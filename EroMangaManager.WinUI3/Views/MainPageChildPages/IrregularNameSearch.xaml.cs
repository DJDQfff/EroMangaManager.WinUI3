// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.Views.MainPageChildPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IrregularNameSearch : Page
    {
        readonly ObservableCollection<MangaBook> books = [];
        public IrregularNameSearch ()
        {
            InitializeComponent();
        }

        private void Button_Click (object sender , RoutedEventArgs e)
        {
            books.Clear();
            foreach (var book in App.Current.GlobalViewModel.MangaList)
            {
                var result = false;
                if (checkbox0.IsChecked == true && BracketBasedStringParser.CorrectBracketPairConut(book.FileDisplayName) == -1)
                {
                    result = true;
                }

                if (checkbox1.IsChecked == true && !BracketBasedStringParser.ContainAnyBrackets(book.FileDisplayName)) //.MangaTagsIncludedInFileName.ContainRepeat())
                {
                    result = true;
                }
                if (checkbox2.IsChecked == true && BracketBasedStringParser.Get_OutsideContent(book.FileDisplayName).Count == 0)
                {
                    result = true;
                }
                if (result == true)
                { books.Add(book); }



            }

        }
    }
}
