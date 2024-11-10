// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

using CommonLibrary.CollectionFindRepeat;

using static CommonLibrary.StringParser.BracketBasedStringParser;

namespace EroMangaManager.WinUI3.Views.FunctionChildPages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FindSameManga : Page
    {
        private ItemsGroupsViewModel<string, MangaBook, RepeatMangaBookGroup> mangaBookViewModel;

        /// <summary>
        ///
        /// </summary>
        public FindSameManga()
        {
            InitializeComponent();
        }

        private async void DeleteFileClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var manga = button.DataContext as MangaBook;

            if (await DialogHelper.ConfirmDeleteSourceFileDialog(manga))
            {
                mangaBookViewModel.DeleteStorageFileInRootObservable(manga);
                App.Current.GlobalViewModel.RemoveManga(manga);
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var index = combobox.SelectedIndex;
            var mangaList = App.Current.GlobalViewModel.MangaList;
            switch (index)
            {
                case 0:

                    {
                        mangaBookViewModel = new ItemsGroupsViewModel<
                            string,
                            MangaBook,
                            RepeatMangaBookGroup
                        >(mangaList, n => n.MangaName, x => !string.IsNullOrWhiteSpace(x.Key));

                        listView.ItemsSource = mangaBookViewModel.RepeatPairs;
                    }
                    ;
                    break;
                case 1:

                    {
                        var dic = StringArrayCollection.Run<MangaBook>(
                            mangaList,
                            x =>
                                Get_OutsideContent(x.FileDisplayName)
                                    .SelectMany(x => x.Split(' ', '-', '+', '~'))
                        );
                        Func<MangaBook, string> func = x =>
                        {
                            var piece = Get_OutsideContent(x.FileDisplayName)
                                .FirstOrDefault(x => dic.ContainsKey(x));
                            return piece;
                        };

                        mangaBookViewModel = new ItemsGroupsViewModel<
                            string,
                            MangaBook,
                            RepeatMangaBookGroup
                        >(mangaList, func, x => !string.IsNullOrWhiteSpace(x.Key));

                        listView.ItemsSource = mangaBookViewModel.RepeatPairs;
                    }
                    break;
            }
        }
    }
}
