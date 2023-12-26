﻿using System.Diagnostics;

using Windows.UI.ViewManagement;

// https://go.microsoft.com/fwlink/?LinkId=234238
// 上介绍了“空白页”项模板

namespace EroMangaManager.WinUI3.Views
{
    /// <summary> 可用于自身或导航至 Frame 内部的空白页。 </summary>
    public sealed partial class ReadPage : Page
    {
        private MangaBook currentManga = null;

        /// <summary>
        /// 当前ReaderViewModel
        /// </summary>
        public ReaderVM currentReader = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReadPage ()
        {
            InitializeComponent();
        }

        public void SetSource (object source)
        {
            switch (source)
            {
                // 当从LibraryFolders打开漫画时，传入MangaBook
                case MangaBook manga:
                    if (manga != currentManga)                  // 传入新漫画，则设置新源
                    {
                        currentManga = manga;
                        var oldreader = currentReader;
                        currentReader = new ReaderVM(manga);
                        oldreader?.Dispose();
                    }
                    break;

                // 当从文件关联打开时，传入StorageFile
                case StorageFile storageFile:
                    {
                        currentManga = ModelFactory.CreateMangaBook(storageFile.Path);

                        currentReader = new ReaderVM(currentManga);
                    }
                    break;
            }

            FLIP.ItemsSource = currentReader.FilteredArchiveImageEntries;
            bool isfilterimage = App.Current.AppConfig.General.IsFilterImageOn;
            FilteredImage[] filteredImages = null;
            if (isfilterimage)
            {
                filteredImages = DatabaseController.database.FilteredImages.ToArray();
            }
            currentReader.SelectEntries(filteredImages);

            ReadPositionSlider.Maximum = currentReader.FilteredArchiveImageEntries.Count;
        }

        /// <summary>
        /// 导航前初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo (NavigationEventArgs e)
        {
            SetSource(e.Parameter);
        }

        /// <summary>
        /// 还是存在切换是闪烁的bug
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void FLIP_SelectionChanged_ByEntry (object sender , SelectionChangedEventArgs e)
        {
            Debug.WriteLine($"SelectionChanged事件开始");

            var selectedEntry = FLIP.SelectedItem as IArchiveEntry;

            if (FLIP.ContainerFromItem(selectedEntry) is FlipViewItem container)
            {
                var root = container.ContentTemplateRoot as Grid;

                var image = root.FindName("image") as Image;

                var stream = currentReader.GetOrAddShowedEntry(selectedEntry);

                switch (stream)
                {
                    // TODO 这个逻辑有问题，做了多余的事
                    case Stream _:
                        image.Source = await selectedEntry.ShowEntryAsync();
                        break;

                    case null:
                        image.Source = new SvgImageSource(new Uri(CoverHelper.ErrorSVGPath));
                        break;
                }
            }

            Debug.WriteLine($"SelectionChanged事件结束");
        }

        /// <summary> 添加此图片到过滤图库 </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>

        private async void FilteThisImageByEntry (object sender , RoutedEventArgs e)
        {
            var entry = FLIP.SelectedItem as IArchiveEntry;

            currentReader.FilteredArchiveImageEntries.Remove(entry);
            var hash = entry.ComputeHash();
            var length = entry.Size;
            await DatabaseController.ImageFilter_Add(hash , length);

            var storageFolder = await GetChildTemporaryFolder(nameof(Filters));
            var path = Path.Combine(storageFolder.Path , hash + ".jpg");
            entry.WriteToFile(path);
        }

        private async void SaveImageByEntry (object e , RoutedEventArgs args)
        {
            var entry = FLIP.SelectedItem as IArchiveEntry;

            var picker = new FileSavePicker();
            picker.FileTypeChoices.Add("图片" , new List<string>() { ".jpg" , ".png" });
            picker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            InitializeWithWindow.Initialize(picker , WindowNative.GetWindowHandle(App.Current.MainWindow));
            var storageFile = await picker.PickSaveFileAsync();

            if (storageFile != null)
            {
                entry.WriteTo(await storageFile.OpenStreamForWriteAsync());
            }
        }

        private void FLIP_Tapped (object sender , TappedRoutedEventArgs e)
        {
            if (ControlsGrid.Visibility == Visibility.Collapsed)
            {
                ControlsGrid.Visibility = Visibility.Visible;
            }
            else
            {
                ControlsGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void AppBarToggleButton_Checked (object sender , RoutedEventArgs e)
        {
            App.Current.MainWindow.AppWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
        }

        private void AppBarToggleButton_Unchecked (object sender , RoutedEventArgs e)
        {
            App.Current.MainWindow.AppWindow.SetPresenter(AppWindowPresenterKind.Default);
        }

        private void ReadPositionSlider_ValueChanged (object sender , RangeBaseValueChangedEventArgs e)
        {
            var entryindex = Convert.ToInt32(ReadPositionSlider.Value - 1);
            FLIP.SelectedIndex = entryindex;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom (NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            currentReader?.Dispose();
        }
    }
}