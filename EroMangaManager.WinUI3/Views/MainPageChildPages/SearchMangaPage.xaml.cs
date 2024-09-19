// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板
using MyLibrary.Standard20;

namespace EroMangaManager.WinUI3.Views.MainPageChildPages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SearchMangaPage : Page
    {
        private MangaSearchViewModel viewmodel;

        /// <summary>
        ///
        /// </summary>
        public SearchMangaPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // 如果从mainpage导航过来的话，这里会是本子集合，不知道为什么
            switch (e.Parameter)
            {
                case SearchParameter searchParameter:
                    var tags = searchParameter.Tags;
                    viewmodel = new MangaSearchViewModel(tags);

                    foreach (var tag in tags)
                    {
                        MangaTagTokenizingTextBox.AddTokenItem(tag);
                    }
                    SearchStartButton_Click(SearchStartButton, new RoutedEventArgs());
                    break;

                //TODO 这里如果用var的话，tags会一直为null
                default:
                    var tags2 = App.Current.GlobalViewModel.AllTags;
                    viewmodel = new MangaSearchViewModel(tags2);

                    break;
            }
        }

        private void MangaTagTokenBox_TextChanged(
            AutoSuggestBox sender,
            AutoSuggestBoxTextChangedEventArgs args
        )
        {
            var a = sender.Text;
            MangaTagTokenizingTextBox.SuggestedItemsSource = viewmodel.Search(a);
        }

        private void MangaTagTokenBox_TokenItemAdding(
            TokenizingTextBox sender,
            TokenItemAddingEventArgs args
        )
        {
            var t = args.TokenText;
            if (!viewmodel.AllTags.Contains(t))
            {
                args.Cancel = true;
            }
        }

        private void MangaTagTokenBox_TokenItemAdded(TokenizingTextBox sender, object args)
        {
            var token = args as string;
            viewmodel.SelectedTags.Add(token);
            viewmodel.HideTag(token);
        }

        private void MangaTagTokenBox_TokenItemRemoved(TokenizingTextBox sender, object args)
        {
            var token = args as string;

            viewmodel.SelectedTags.Remove(token);
            viewmodel.CancelHideTag(token);
        }

        private void MangaNameAugoSuggestBox_TextChanged(
            AutoSuggestBox sender,
            AutoSuggestBoxTextChangedEventArgs args
        )
        {
            var text = sender.Text;
            if (text != null)
            {
                // 修改MangaName解析方法后，MangaName可能为null，这里可能报错
                var a = App.Current.GlobalViewModel.MangaList
                    .Where(x => x.MangaName != null && x.MangaName.Contains(text))
                    .Select(x => x.MangaName)
                    .Distinct()
                    .ToList();
                sender.ItemsSource = a;
            }
        }

        private void SearchStartButton_Click(object sender, RoutedEventArgs e)
        {
            var manganame = MangaNameAugoSuggestBox.Text;

            var tags = new List<string>(viewmodel.SelectedTags) { manganame };

            var requiredMatchCount = tags.Count;

            var allmangas = App.Current.GlobalViewModel.MangaList;

            var conditions = allmangas.Where(
                x => tags.Count(y => x.FileDisplayName.Contains(y)) == requiredMatchCount
            );

            ResultGridView.ItemsSource = conditions;
        }

        private void ShowInBookcaseButton_Click(object sender, RoutedEventArgs e)
        {
            var result = ResultGridView.ItemsSource;

            var condition = result as IEnumerable<MangaBook>;

            var mangasfolder = new MangasGroup()
            {
                ShowString = (
                    MangaNameAugoSuggestBox.Text + "+" + MangaTagTokenizingTextBox.SelectedTokenText
                ).Trim('+'),
            };
            mangasfolder.MangaBooks.AddRange(condition);
            MainPage.Current.MainFrame.Navigate(typeof(Bookcase), mangasfolder);
        }
    }
}
