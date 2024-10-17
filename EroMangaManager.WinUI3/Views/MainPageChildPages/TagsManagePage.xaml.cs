// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace EroMangaManager.WinUI3.Views.MainPageChildPages
{
    /// <summary>......................................................................................................................................................................
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TagsManagePage : Page
    {
        private MenuFlyout menuFlyout = new MenuFlyout();

        // 这个本来是用tagcategory.selecteditem属性的，但是不知道为什么一直为null，才额外加了一个变量
        private TagCategory tagCategory;

        private ManageTagsViewModel2 viewmodel = new();

        /// <summary>
        ///
        /// </summary>
        public TagsManagePage ()
        {
            InitializeComponent();
            var tags = App.Current.GlobalViewModel.AllTags;

            viewmodel.AddUnCategoryTags(tags);
            viewmodel.CategorysChanged += MenuFlyout_SetValue;

            MenuFlyout_SetValue();
        }

        private void Category_ListVIew_DragOver (object sender , DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
        }

        private void Category_ListVIew_Drop (object sender , DragEventArgs e)
        { }

        private void Category_ListVIew_ItemClick (object sender , ItemClickEventArgs e)
        {
            var a = e.ClickedItem as TagCategory;
            tagCategory = a;
            Tag_ListView.ItemsSource = a.Tags;
        }

        private void ListViewItem_Tapped (object sender , TappedRoutedEventArgs e)
        {
            Tag_ListView.ItemsSource = viewmodel.ImCategoryedTags;
            tagCategory = null;
        }

        private void MenuFlyout_SetValue ()
        {
            menuFlyout.Items.Clear();
            foreach (var tagcategory in viewmodel.CategoryTags)
            {
                var item = new MenuFlyoutItem() { DataContext = tagcategory };

                item.Text = tagcategory.CategoryName;
                //TODO item.IsEnabled = viewmodel.DisplayedCategory != tagcategory;
                item.Click += (s , args) =>
                {
                    var flyoutitem = s as MenuFlyoutItem;
                    var category = flyoutitem.DataContext as TagCategory;
                    var tags = Tag_ListView.SelectedItems.Select(x => x as string).ToArray();

                    var selectedCategory = Category_ListVIew.SelectedItem as TagCategory;

                    viewmodel.TagChangeCategory(selectedCategory , category , tags);
                };

                menuFlyout.Items.Add(item);
            }
        }

        private void MenuFlyoutItem_Click (object sender , RoutedEventArgs e)
        {
            var item = sender as MenuFlyoutItem;

            if (item.DataContext is string str)
            {
                var a = new SearchParameter() { Tags = new List<string> { str } };
                MainPage.Current.MainFrame.Navigate(typeof(SearchMangaPage) , a);
            }
        }

        private void MenuFlyoutItem_Click_1 (object sender , RoutedEventArgs e)
        {
            var item = sender as MenuFlyoutItem;
            var tagcategory = item.DataContext as TagCategory;

            viewmodel.DeleteCategory(tagcategory.CategoryName);
            if (tagcategory == tagCategory)
            {
                Tag_ListView.ItemsSource = null;
            }
        }

        private void Tag_ListView_DragItemsCompleted (
                            ListViewBase sender ,
            DragItemsCompletedEventArgs args
        )
        {
            //viewmodel.DisplayedTags.Remove(args.Items[0] as string);
        }
    }
}