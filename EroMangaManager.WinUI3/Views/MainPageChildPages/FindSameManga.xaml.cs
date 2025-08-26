// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace EroMangaManager.WinUI3.Views.FunctionChildPages;

/// <summary>
/// 可用于自身或导航至 Frame 内部的空白页。
/// </summary>
public sealed partial class FindSameManga : Page
{
    private readonly SameMangaSearchViewModel viewModel = new();

    /// <summary>
    ///
    /// </summary>
    public FindSameManga()
    {
        InitializeComponent();
        App.Current.GlobalViewModel.EventAfterDeleteMangaSource += viewModel.DeleteStorageFileInRootObservable;
        viewModel.AddToResult += x => App.Current.BackgroundCoverSetter.mangas.Insert(0, x);
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        viewModel.Source = App.Current.GlobalViewModel.MangaList.SkipWhile(x => string.IsNullOrWhiteSpace(x.MangaName)).ToList();

        switch (combobox.SelectedIndex)
        {
            case 0:
                await viewModel.Method0();
                break;

            case 1:
                await viewModel.Method1();
                break;

            case 2:
                await viewModel.Method2();
                break;

            case 3:
                {
                    var selectcategory = new TagCategorySelect()
                    {
                        XamlRoot = App.Current.MainWindow.Content.XamlRoot
                    };
                    var result = await selectcategory.ShowAsync();
                    switch (result)
                    {
                        case ContentDialogResult.Primary:
                            {
                                if (!string.IsNullOrWhiteSpace(selectcategory.CategoryName))
                                {
                                    var strings = DatabaseController.TagCategory_QuerySingle(selectcategory.CategoryName);
                                    await viewModel.Method4(strings);
                                }
                            }
                            break;
                    }
                }
                break;
        }
    }
}