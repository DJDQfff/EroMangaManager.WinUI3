﻿// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace EroMangaManager.WinUI3.Views.MainPageChildPages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Bookcase : Page
    {
        private MangasGroup _data;

        internal MangasGroup BindMangasFolder
        {
            set
            {
                _data = value;
                Bookcase_GridView.ItemsSource = value.MangaBooks;
                Bookcase_HintTextBlock.Visibility = Visibility.Collapsed;
            }
            get
            {
                return _data;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Bookcase ()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 导航时，传入要绑定的数据
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo (NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            MainPage.Current.MainNavigationView.SelectedItem = MainPage
                .Current
                .MainNavigationView
                .MenuItems[0];

            switch (e.Parameter)
            {
                case MangasGroup mangasFolder:
                    BindMangasFolder = mangasFolder;
                    CommandText.Text = mangasFolder.ShowString;
                    break;
            }
        }

        //TODO 本子名翻译功能。 因为原来的Bookcase被拆分为Bookcase和Bookcase两个类，所以这个方法现在有bug
        private async void TranslateEachMangaName (object sender , RoutedEventArgs e)
        {
            var button = sender as AppBarButton;
            button.IsEnabled = false;

            try
            {
                await Translator.TranslateAllMangaName();
            }
            catch { }

            var items = Bookcase_GridView.Items;
            foreach (var item in items)
            {
                var manga = item as MangaBook;
                var grid = Bookcase_GridView.ContainerFromItem(item) as GridViewItem;
                var root = grid.ContentTemplateRoot as Grid;
                var run = root.FindName("runtext") as Microsoft.UI.Xaml.Documents.Run;
                run.Text = manga.TranslatedMangaName;
            }

            button.IsEnabled = true;
        }

        private async void ExportPDF (object sender , RoutedEventArgs e)
        {
            var menuFlyout = sender as MenuFlyoutItem;
            var mangaBook = menuFlyout.DataContext as MangaBook;

            var fileSavePicker = new FileSavePicker();
            fileSavePicker.FileTypeChoices.Add("PDF" , new List<string> { ".pdf" });
            fileSavePicker.SuggestedFileName = mangaBook.FileDisplayName;

            var handle = WindowNative.GetWindowHandle(App.Current.MainWindow);
            InitializeWithWindow.Initialize(fileSavePicker , handle);

            var storageFile = await fileSavePicker.PickSaveFileAsync();
            try
            {
                await Task.Run(() => Exporter.ExportAsPDF(mangaBook , storageFile.Path));
                string done = ResourceLoader.GetForViewIndependentUse("Strings").GetString("ExportDone");
                App.Current.GlobalViewModel.WorkDone(done);
            }
            catch
            {
                string failed = ResourceLoader.GetForViewIndependentUse("Strings").GetString("ExportFailed");
                App.Current.GlobalViewModel.WorkFailed(failed);
            }
        }

        private void Order (object sender , RoutedEventArgs e)
        {
            BindMangasFolder?.SortMangaBooks(x => x.FileSize);
        }

        private async void OverviewInformation (object sender , RoutedEventArgs e)
        {
            var menuFlyout = sender as MenuFlyoutItem;
            var manga = menuFlyout.DataContext as MangaBook;
            var dialog = new OverviewInformation(manga)
            {
                XamlRoot = App.Current.MainWindow.Content.XamlRoot
            };
            await dialog.ShowAsync();
        }

        private void MangaBookBriefInfo_DoubleTapped (object sender , DoubleTappedRoutedEventArgs e)
        {
            var control = sender as GridView;

            var mangaBook = control.SelectedItem as MangaBook;

            Commands.MangaBookCommands.Instance.OpenManga.Execute(mangaBook);
        }

        private void Bookcase_GridView_Loaded (object sender , RoutedEventArgs e)
        {
            var index = App.Current.AppConfig.AppConfig.General.BookcaseTemplateKey;
            DataTemplateList.SelectedIndex = index;
            var key = "BookcaseTemplate" + index;
            Bookcase_GridView.ItemTemplate = this.Resources[key] as DataTemplate;
        }

        private void DataTemplateList_SelectionChanged (object sender , SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            var index = listbox.SelectedIndex;
            var key = "BookcaseTemplate" + index;

            Bookcase_GridView.ItemTemplate = this.Resources[key] as DataTemplate;
            var template = new DataTemplate();
            App.Current.AppConfig.AppConfig.General.BookcaseTemplateKey = index;
        }
    }
}