// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace EroMangaManager.WinUI3.Views.MainPageChildPages
{
    /// <summary>......................................................................................................................................................................
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TagsManagePage : Page
    {
        ManageTagsViewModel viewmodel = new();

        /// <summary>
        ///
        /// </summary>
        public TagsManagePage()
        {
            InitializeComponent();
            var tags = App.Current.GlobalViewModel.AllTags;

            viewmodel.AddUnCategoryTags(tags);
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuFlyoutItem;

            if (item.DataContext is string str)
            {
                var a = new SearchParameter() { Tags = new List<string> { str } };
                MainPage.Current.MainFrame.Navigate(typeof(SearchMangaPage), a);
            }
        }

        private void Tag_ListView_DragItemsCompleted(
            ListViewBase sender,
            DragItemsCompletedEventArgs args
        )
        {
            viewmodel.DisplayedTags.Remove(args.Items[0] as string);
        }

        private void Category_ListVIew_Drop(object sender, DragEventArgs e) { }

        private void Category_ListVIew_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
        }

        private void MenuFlyout_Opened(object sender, object e)
        {
            var control = sender as MenuFlyout;
            control.Items.Clear();
            foreach (var b in viewmodel.KeyValuePairs.Keys)
            {
                var c = new MenuFlyoutItem() { DataContext = b };
                c.Text = b;

                c.Click += (s, args) =>
                {
                    var flyoutitem = s as MenuFlyoutItem;
                    var category = flyoutitem.DataContext as string;
                    var tags = listview.SelectedItems as List<string>;

                    viewmodel.TagChangeCategory(viewmodel.DisplayedCategory, category, tags);
                };
                control.Items.Add(c);
            }
        }
    }
}
