// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace EroMangaManager.WinUI3.Views.MainPageChildPages
{
    /// <summary>......................................................................................................................................................................
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TagsManagePage : Page
    {
        ManageTagsViewModel2 viewmodel = new();

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
            //viewmodel.DisplayedTags.Remove(args.Items[0] as string);
        }

        private void Category_ListVIew_Drop(object sender, DragEventArgs e) { }

        private void Category_ListVIew_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
        }

        private void MenuFlyout_SetValue()
        {
            menuFlyout.Items.Clear();
            foreach (var tagcategory in viewmodel.CategoryTags)
            {
                var item = new MenuFlyoutItem() { DataContext = tagcategory };

                item.Text = tagcategory.CategoryName;
                //TODO item.IsEnabled = viewmodel.DisplayedCategory != tagcategory;
                item.Click += (s, args) =>
                {
                    var flyoutitem = s as MenuFlyoutItem;
                    var category = flyoutitem.DataContext as TagCategory;
                    var tags = Tag_ListView.SelectedItems.Select(x => x as string).ToArray();

                    var selectedCategory = Category_ListVIew.SelectedItem as TagCategory;

                    viewmodel.TagChangeCategory(selectedCategory, category, tags);
                };

                menuFlyout.Items.Add(item);
            }
        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuFlyoutItem;
            var tagcategory = item.DataContext as TagCategory;

            viewmodel.DeleteCategory(tagcategory.CategoryName);
        }

        private void MenuFlyoutItem_Click_2(object sender, RoutedEventArgs e) { }

        private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Tag_ListView.ItemsSource = viewmodel.ImCategoryedTags;
            Category_ListVIew.SelectedItem = null;
        }

        private void Category_ListVIew_ItemClick(object sender, ItemClickEventArgs e)
        {
            var a = e.ClickedItem as TagCategory;
            Tag_ListView.ItemsSource = a.Tags;
        }

        private void Category_ListVIew_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e
        ) { }
    }
}
