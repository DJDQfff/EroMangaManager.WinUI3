// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace EroMangaManager.WinUI3.Views.FunctionChildPages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FindSameManga : Page
    {
        private ItemsGroupsViewModel<string , MangaBook , RepeatMangaBookGroup> mangaBookViewModel;

        private List<MangaBook> mangaBooks;

        /// <summary>
        ///
        /// </summary>
        public FindSameManga ()
        {
            InitializeComponent();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo (NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is List<MangaBook> books)
            {
                mangaBooks = books;
                mangaBookViewModel = new ItemsGroupsViewModel<string , MangaBook , RepeatMangaBookGroup>(mangaBooks , n => n.MangaName);
                listView.ItemsSource = mangaBookViewModel.RepeatPairs;
            }
        }

        private async void DeleteFileClick (object sender , RoutedEventArgs e)
        {
            var button = sender as Button;
            var manga = button.DataContext as MangaBook;

            if (await StorageHelper.DeleteSourceFile(manga))
            {
                mangaBookViewModel.DeleteStorageFileInRootObservable(manga);
            }
        }
    }
}