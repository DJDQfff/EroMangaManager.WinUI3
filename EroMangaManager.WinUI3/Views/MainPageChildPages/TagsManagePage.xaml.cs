// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace EroMangaManager.WinUI3.Views.MainPageChildPages
{
    /// <summary>......................................................................................................................................................................
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TagsManagePage : Page
    {
        ManageTagsViewModel viewmodel = new();

        MenuFlyout menuFlyout = new MenuFlyout();

        /// <summary>
        ///
        /// </summary>
        public TagsManagePage()
        {
            InitializeComponent();
            var tags = App.Current.GlobalViewModel.AllTags;

            viewmodel.AddUnCategoryTags(tags);
            viewmodel.CategorysChanged += MenuFlyout_SetValue;

            MenuFlyout_SetValue();
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

        private void MenuFlyout_SetValue()
        {
            menuFlyout.Items.Clear();
            foreach (var b in viewmodel.Categorys)
            {
                var c = new MenuFlyoutItem() { DataContext = b };

                c.Text = b;
                //TODO c.IsEnabled = viewmodel.DisplayedCategory != b;
                c.Click += (s, args) =>
                {
                    var flyoutitem = s as MenuFlyoutItem;
                    var category = flyoutitem.DataContext as string;
                    var tags = Tag_ListView.SelectedItems.Select(x => x as string).ToArray();

                    viewmodel.TagChangeCategory(viewmodel.DisplayedCategory, category, tags);
                };

                menuFlyout.Items.Add(c);
            }
        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            viewmodel.DeleteCategory(viewmodel.DisplayedCategory);
        }

        private void MenuFlyoutItem_Click_2(object sender, RoutedEventArgs e) { }
    }
}
