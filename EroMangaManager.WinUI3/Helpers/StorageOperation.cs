namespace EroMangaManager.WinUI3.Helpers;
internal class StorageOperation
{
    internal static async Task ExportAsPDFAsync (MangaBook mangaBook)
    {
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
    internal static async Task Delete (MangaBook eroManga , StorageDeleteOption deletemode)
    {
        try
        {
#if WINDOWS
            var file = await Windows.Storage.StorageFile.GetFileFromPathAsync(eroManga.FilePath);
            await file.DeleteAsync(deletemode);
#else
                        System.IO.File.Delete(eroManga.FilePath);

#endif
        }
        catch
        {
        }
    }

    internal static void RenameMange (MangaBook book , string text)
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
                File.Move(oldname , newname);
            }
            catch { }
            book.FilePath = Path.Combine(book.FolderPath , text + ".zip");
        }
    }
}
