namespace EroMangaManager.WinUI3.Helpers;

internal class StorageOperation
{
    internal static async Task ExportAsPDFAsync (MangaBook mangaBook)
    {
        var fileSavePicker = new FileSavePicker();
        fileSavePicker.FileTypeChoices.Add("PDF" , [".pdf"]);
        fileSavePicker.SuggestedFileName = mangaBook.FileDisplayName;

        var handle = WindowNative.GetWindowHandle(App.Current.MainWindow);
        InitializeWithWindow.Initialize(fileSavePicker , handle);

        var storageFile = await fileSavePicker.PickSaveFileAsync();
        try
        {
            await Task.Run(() => Exporter.ExportAsPDF(mangaBook , storageFile.Path));
            string done = ResourceLoader.GetForViewIndependentUse().GetString("ExportDone");
            App.Current.GlobalViewModel.WorkDone(done);
        }
        catch
        {
            string failed = ResourceLoader.GetForViewIndependentUse().GetString("ExportFailed");
            App.Current.GlobalViewModel.WorkFailed(failed);
        }
    }

    internal static async Task Delete (MangaBook eroManga , StorageDeleteOption deletemode)
    {
        try
        {
            switch (eroManga.Type)
            {
                case "":

                    {
#if WINDOWS
                        var folder = await Windows.Storage.StorageFolder.GetFolderFromPathAsync(
                            eroManga.FilePath
                        );
                        System.IO.Directory.Delete(eroManga.FilePath , true);

                        await folder.DeleteAsync(deletemode);
#else
                        System.IO.Directory.Delete(eroManga.FilePath, true);
#endif
                    }
                    break;
                default:

                    {
#if WINDOWS
                        var file = await Windows.Storage.StorageFile.GetFileFromPathAsync(
                            eroManga.FilePath
                        );
                        await file.DeleteAsync(deletemode);
#else
                        System.IO.File.Delete(eroManga.FilePath);
#endif
                    }
                    break;
            }
        }
        catch { }
    }

    internal static void RenameMange (MangaBook book , string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }
        else
        {
            switch (book.Type)
            {
                case "":

                    {
                        // TODO 重命名可能存在bug，如重复名称
                        string oldname = book.FilePath;
                        string newname = Path.Combine(
                            Path.GetDirectoryName(oldname) ,
                            text + book.Type
                        );
                        if (oldname != newname)
                        {
                            Directory.Move(oldname , newname);
                            book.FilePath = newname;
                        }
                    }
                    break;

                default:

                    {
                        // TODO 重命名可能存在bug，如重复名称
                        string oldname = book.FilePath;
                        string newname = Path.Combine(
                            Path.GetDirectoryName(oldname) ,
                            text + book.Type
                        );
                        File.Move(oldname , newname);
                        book.FilePath = newname;
                    }
                    break;
            }
        }
    }
}
