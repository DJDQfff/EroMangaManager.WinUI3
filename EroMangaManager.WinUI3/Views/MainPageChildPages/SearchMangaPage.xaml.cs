// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板
namespace EroMangaManager.WinUI3.Views.MainPageChildPages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SearchMangaPage : Page
    {
        private readonly MangaSearchViewModel viewmodel = new(App.Current.GlobalViewModel);

        /// <summary>
        ///
        /// </summary>
        public SearchMangaPage ()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo (NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // 如果从mainpage导航过来的话，这里会是本子集合，不知道为什么
            if (e.Parameter is SearchParameter searchParameter)
            {
                var tags = searchParameter.Tags;

                foreach (var tag in tags)
                {
                    MangaTagTokenizingTextBox.AddTokenItem(tag);
                }
                //SearchStartButton_Click(SearchStartButton , new RoutedEventArgs());

            }

        }


        private void TagTokenBox_TokenItemAdding (
            TokenizingTextBox sender ,
            TokenItemAddingEventArgs args
        )
        {
            var t = args.TokenText;
            if (!viewmodel.AllTags.Contains(t))
            {
                args.Cancel = true;
            }
        }

        private void TagTokenBox_TokenItemAdded (TokenizingTextBox sender , object args)
        {
            var token = args as string;
            viewmodel.RequiredTags.Add(token);
            viewmodel.SearchResult(MangaNameAugoSuggestBox.Text);

        }

        private void TagTokenBox_TokenItemRemoved (TokenizingTextBox sender , object args)
        {
            var token = args as string;
            _ = viewmodel.RequiredTags.Remove(token);
            viewmodel.SearchResult(MangaNameAugoSuggestBox.Text);

        }

        private void NameBox_TextChanged (
            AutoSuggestBox sender ,
            AutoSuggestBoxTextChangedEventArgs args
        )
        {
            viewmodel.SearchResult(MangaNameAugoSuggestBox.Text);
        }


        //private void ShowInBookcaseButton_Click (object sender , RoutedEventArgs e)
        //{
        //    var result = ResultGridView.ItemsSource;

        //    var condition = result as IEnumerable<Manga>;

        //    var mangasfolder = new MangasGroup()
        //    {
        //        ShowString = (
        //            MangaNameAugoSuggestBox.Text + "+" + MangaTagTokenizingTextBox.SelectedTokenText
        //        ).Trim('+') ,
        //    };
        //    mangasfolder.Mangas.AddRange(condition);
        //    MainPage.Current.MainFrame.Navigate(typeof(Bookcase) , mangasfolder);
        //}

        private void TagTokenizingTextBox_TextChanged (AutoSuggestBox sender , AutoSuggestBoxTextChangedEventArgs args)
        {
            viewmodel.FiltTags(MangaNameAugoSuggestBox.Text);

        }
    }
}
