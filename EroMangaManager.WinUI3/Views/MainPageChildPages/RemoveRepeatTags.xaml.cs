// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板
using MyLibrary.Standard20;

namespace EroMangaManager.WinUI3.Views.FunctionChildPages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RemoveRepeatTags : Page
    {
        private ObservableCollection<MangaBook> RepaetBooks { get; } = new();

        /// <summary>
        ///
        /// </summary>
        public RemoveRepeatTags ()
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

            foreach (var book in App.Current.GlobalViewModel.MangaList)
            {
                if (book.MangaTagsIncludedInFileName.ContainRepeat())
                {
                    RepaetBooks.Add(book);
                }
            }

        }

        private void SingleMangaBookRename_New (object sender , RoutedEventArgs e)
        {

            var button = sender as Button;
            var stackpanel = button.Parent as StackPanel;

            var book = button.DataContext as MangaBook;

            var control = stackpanel.FindName("newnameBox") as TextBox;
            var text = control.Text;
            TrySetNewName(book , text);
            RemoveIfTagRepeat(book);

        }

        private void TrySetNewName (MangaBook book , string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            else
            {
                try
                {
                    // TODO 重命名可能存在bug，如重复名称
                    string oldname = book.FilePath;
                    string newname = Path.Combine(Path.GetDirectoryName(oldname) , text + ".zip");
                    System.IO.File.Move(oldname , newname);
                    book.FilePath = Path.Combine(book.FolderPath , text + ".zip");
                }
                catch
                {
                }
            }
        }

        void RemoveIfTagRepeat (MangaBook book)
        {
            if (!book.MangaTagsIncludedInFileName.ContainRepeat())
            {
                RepaetBooks.Remove(book);
            }

        }
        private void Button_Click (object sender , RoutedEventArgs e)
        {
            var button = sender as Button;

            var book = button.DataContext as MangaBook;

            RepaetBooks.Remove(book);
        }

        private void TagListOrder_Loaded (object sender , RoutedEventArgs e)
        {
            var order = sender as DJDQfff.TagListOrder;
            var manga = order.DataContext as MangaBook;
            var items = NameParser.SplitByBlank(manga.FileDisplayName);
            order.Sources = items;
        }

        private void newnameBox_TextChanged (object sender , TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            var book = textbox.DataContext as MangaBook;

            var text = textbox.Text;
            // TODO 这有严重bug，每次文字切换，会直接改名
            //TrySetNewName(book , text);
            RemoveIfTagRepeat(book);

        }

        private void UserControl_Loaded (object sender , RoutedEventArgs e)
        {
            var control = sender as UserControl;
            var newnamebox = control.FindName("newnamebox");
        }
    }
}