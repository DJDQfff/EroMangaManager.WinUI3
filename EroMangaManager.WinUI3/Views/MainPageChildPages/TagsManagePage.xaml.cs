// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace EroMangaManager.WinUI3.Views.MainPageChildPages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TagsManagePage : Page
    {
        public ObservableCollection<string> ColorsCollection;

        /// <summary>
        ///
        /// </summary>
        public TagsManagePage ()
        {
            InitializeComponent();
            var tags = App.Current.GlobalViewModel.AllTags;
            ColorsCollection = new(tags);
            Debug.WriteLine(ColorsCollection.Count);
        }

        private void MenuFlyoutItem_Click (object sender , RoutedEventArgs e)
        {
            var item = sender as MenuFlyoutItem;
            var str = item.DataContext as string;
            if (str != null)
            {
                var a = new SearchParameter()
                {
                    Tags = new List<string> { str }
                };
                MainPage.Current.MainFrame.Navigate(typeof(SearchMangaPage) , a);
            }
        }
    }
}