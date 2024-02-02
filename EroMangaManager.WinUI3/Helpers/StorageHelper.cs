namespace EroMangaManager.WinUI3.Helpers;

/// <summary>
/// 操作文件时，还要与用户进行交互，以及一些别的操作
/// </summary>
internal class StorageHelper
{
    /// <summary>
    /// 修改文件名
    /// </summary>
    /// <param name="eroManga"></param>
    /// <returns></returns>
    public static async Task RenameSourceFile (MangaBook eroManga)
    {
        // TODO 暂时放弃，不会写页面UI，写出来也丑。等EditTag功能好了，在改回EditTag页面
        var renameDialog = new RenameDialog(eroManga)
        {
            XamlRoot = App.Current.MainWindow.Content.XamlRoot
        };
        var result = await renameDialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            // text是否合法由对话框保证
            var text = renameDialog.NewDisplayName;

            try
            {
                // TODO 重命名可能存在bug，如重复名称
                string oldname = eroManga.FilePath;
                string newname = Path.Combine(Path.GetDirectoryName(oldname) , text + ".zip");
                File.Move(oldname , newname);
            }
            catch { }
            eroManga.MangaName = text;
            eroManga.FilePath = Path.Combine(eroManga.FolderPath , text + ".zip");

            eroManga.NotifyPropertyChanged(string.Empty);
        }
    }

    /// <summary>
    /// 删除源文件时，会触发删除确认弹框，删除模式，这两个参数都是从程序设置中读取的，因此封装到助手类里面
    /// </summary>
    /// <param name="eroManga"></param>
    /// <returns></returns>
    public static async Task<bool> DeleteSourceFile (MangaBook eroManga)
    {
        var temp1 = App.Current.AppConfig.AppConfig.General.WhetherShowDialogBeforeDelete;

        var temp2 = App.Current.AppConfig.AppConfig.General.StorageFileDeleteOption;

        var option = temp2 ? StorageDeleteOption.PermanentDelete : StorageDeleteOption.Default;

        bool deleteResult = false;
        if (!temp1)
        {
            ConfirmDeleteMangaFile confirm = new(eroManga)
            {
                XamlRoot = App.Current.MainWindow.Content.XamlRoot
            };
            var result = await confirm.ShowAsync();
            switch (result)
            {
                case ContentDialogResult.Primary:
                    App.Current.GlobalViewModel.RemoveManga(eroManga);
                    try
                    {
                        System.IO.File.Delete(eroManga.FilePath);
                    }
                    catch
                    {
                    }
                    deleteResult = true;
                    break;

                case ContentDialogResult.Secondary:
                    break;
            }
        }
        else
        {
            App.Current.GlobalViewModel.RemoveManga(eroManga);
            try
            {
                System.IO.File.Delete(eroManga.FilePath);
            }
            catch
            {
            }
            deleteResult = true;
        }

        return deleteResult;
    }
}