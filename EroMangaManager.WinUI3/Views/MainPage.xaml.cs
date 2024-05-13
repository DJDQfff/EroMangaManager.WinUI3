// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804
// 上介绍了“空白页”项模板

namespace EroMangaManager.WinUI3.Views
{
    /// <summary> 可用于自身或导航至 Frame 内部的空白页。 </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// MainPage的单一实例
        /// </summary>
        internal static MainPage Current { set; get; }

        /// <summary>
        ///
        /// </summary>
        public MainPage ()
        {
            InitializeComponent();

            Current = this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo (NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var defaultfolder = App.Current.GlobalViewModel.MangaFolders.FirstOrDefault();

            MainFrame.Navigate(typeof(Bookcase) , defaultfolder);
        }

        private void MainNavigationView_ItemInvoked (NavigationView sender , NavigationViewItemInvokedEventArgs args)
        {
            //if (args.IsSettingsInvoked)
            //{
            //    var dialog = new SettingDialog()
            //    {
            //        XamlRoot = App.Current.MainWindow.Content.XamlRoot ,
            //    };
            //    await dialog.ShowAsync();
            //    return;
            //}
            Type type = args.InvokedItemContainer.Name switch
            {
                nameof(BookcaseItem) => typeof(Bookcase),
                nameof(ListItem) => typeof(LibraryPage),
                nameof(TagsManagePage) => typeof(TagsManagePage),
                nameof(SearchMangaPage) => typeof(SearchMangaPage),
                nameof(FindSameMangaByName) => typeof(FindSameManga),
                nameof(RemoveRepeatTags) => typeof(RemoveRepeatTags),
                nameof(IrregularFileNames) => typeof(IrregularNameSearch),
                nameof(SeriesMangas) => typeof(SeriesMangas),
                _ => typeof(SettingPage)
            };

            if (!type.Equals(MainFrame.CurrentSourcePageType))
            {
                MainFrame.Navigate(type , App.Current.GlobalViewModel.MangaList);
            }
        }

        private void UpdateRecordItem_Tapped (object sender , TappedRoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(UpdateRecordsPage));
        }

        private void UsageButton_Tapped (object sender , TappedRoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(UsageDocumentPage));
        }
    }
}