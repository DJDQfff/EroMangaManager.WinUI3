// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

using System;

using CommonLibrary.CollectionFindRepeat;


using static CommonLibrary.BracketBasedStringParser;

namespace EroMangaManager.WinUI3.Views.FunctionChildPages;

/// <summary>
/// 可用于自身或导航至 Frame 内部的空白页。
/// </summary>
public sealed partial class FindSameManga : Page
{
    SameMangaSearchViewModel viewModel = new();
    /// <summary>
    ///
    /// </summary>
    public FindSameManga ()
    {
        InitializeComponent();
    }

    private async void DeleteFileClick (object sender , RoutedEventArgs e)
    {
        var button = sender as Button;
        var manga = button.DataContext as Manga;

        if (await DialogHelper.ConfirmDeleteSourceFileDialog(manga))
        {
            viewModel.mangaBookViewModel.DeleteStorageFileInRootObservable(manga);
            _ = App.Current.GlobalViewModel.TryRemoveManga(manga);
        }
    }

    private void Button_Click (object sender , RoutedEventArgs e)
    {
        var mangaList = App.Current.GlobalViewModel.MangaList;

        viewModel.StartSearch(mangaList , combobox.SelectedIndex);


    }
}
