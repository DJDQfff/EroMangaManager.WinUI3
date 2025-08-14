// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

using System;
using System.Threading.Tasks;

using CommonLibrary.CollectionFindRepeat;

using EroMangaManager.Core.Models;

using static CommonLibrary.BracketBasedStringParser;

namespace EroMangaManager.WinUI3.Views.FunctionChildPages;

/// <summary>
/// 可用于自身或导航至 Frame 内部的空白页。
/// </summary>
public sealed partial class FindSameManga : Page
{
    readonly SameMangaSearchViewModel2 viewModel = new();
    /// <summary>
    ///
    /// </summary>
    public FindSameManga ()
    {
        InitializeComponent();
        App.Current.GlobalViewModel.EventAfterDeleteMangaSource += viewModel.DeleteStorageFileInRootObservable;
        //viewModel.AddToResult += x => App.Current.BackgroundCoverSetter.mangas.Insert(0 , x);
    }


    private async void Button_Click (object sender , RoutedEventArgs e)
    {

        viewModel.Source = App.Current.GlobalViewModel.MangaList.SkipWhile(x => string.IsNullOrWhiteSpace(x.MangaName)).ToList();
        //viewModel.StartSearch(combobox.SelectedIndex);// 无法异步，放弃
        switch (combobox.SelectedIndex)
        {
            case 0:
                await viewModel.Method0();
                break;
        }

    }
}
