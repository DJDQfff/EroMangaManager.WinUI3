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

            listview.ItemsSource = RepaetBooks;
        }

        private void SingleMangaBookRename_New (object sender , RoutedEventArgs e)
        {
            var button = sender as Button;
            var stackpanel = button.Parent as StackPanel;

            var book = button.DataContext as MangaBook;

            var control = stackpanel.FindName("renamecontrol") as RenameMangaBookByString;

            if (control.IsNewnameOK)
            {
                var text = control.NewDisplayName;
                try
                {
                    // TODO 重命名可能存在bug，如重复名称
                    string oldname = book.FilePath;
                    string newname = Path.Combine(Path.GetDirectoryName(oldname) , text + ".zip");
                    System.IO.File.Move(oldname , newname);

                    book.MangaName = text;
                    book.FilePath = Path.Combine(book.FolderPath , text + ".zip");
                }
                catch
                {
                }
            }
        }

        private void Button_Click (object sender , RoutedEventArgs e)
        {
            var button = sender as Button;

            var book = button.DataContext as MangaBook;

            RepaetBooks.Remove(book);
        }
    }
}