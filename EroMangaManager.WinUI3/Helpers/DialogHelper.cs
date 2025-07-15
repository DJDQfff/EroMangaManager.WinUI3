namespace EroMangaManager.WinUI3.Helpers;

/// <summary>
/// 需要通过对话框用户进行交互，以及一些读取程序设置的操作
/// </summary>
internal class DialogHelper
{
    /// <summary>
    /// 修改文件名
    /// </summary>
    /// <param name="eroManga"></param>
    /// <returns></returns>
    public static async Task RenameSourceFileInDialog(MangaBook eroManga)
    {
        // TODO 暂时放弃，不会写页面UI，写出来也丑。等EditTag功能好了，在改回EditTag页面
        var renameDialog = new RenameDialog(eroManga)
        {
            XamlRoot = App.Current.MainWindow.Content.XamlRoot
        };

        _ = await renameDialog.ShowAsync();
    }

    /// <summary>
    /// 删除源文件时，会触发删除确认弹框，删除模式，这两个参数都是从程序设置中读取的，因此封装到助手类里面
    /// </summary>
    /// <param name="eroManga"></param>
    /// <returns></returns>
    public static async Task<bool> ConfirmDeleteSourceFileDialog(MangaBook eroManga)
    {
        var temp1 = App.Current.AppConfig.AppConfig.General.WhetherShowDialogBeforeDelete;

        var temp2 = App.Current.AppConfig.AppConfig.General.StorageFileDeleteOption;

        var deletemode = temp2 ? StorageDeleteOption.PermanentDelete : StorageDeleteOption.Default;

        bool deleteResult = false;
        if (!temp1)
        {
            ConfirmDeleteMangaFile confirm =
                new(eroManga) { XamlRoot = App.Current.MainWindow.Content.XamlRoot };
            var result = await confirm.ShowAsync();
            switch (result)
            {
                case ContentDialogResult.Primary:
                    _ = App.Current.GlobalViewModel.RemoveManga(eroManga);
                    await StorageOperation.Delete(eroManga, deletemode);
                    deleteResult = true;
                    break;

                case ContentDialogResult.Secondary:
                    break;
            }
        }
        else
        {
            await StorageOperation.Delete(eroManga, deletemode);

            deleteResult = true;
        }

        return deleteResult;
    }
}
