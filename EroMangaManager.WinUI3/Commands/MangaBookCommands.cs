namespace EroMangaManager.WinUI3.Commands;

internal class MangaBookCommands
{
    public static MangaBookCommands Instance { get; set; }

    public StandardUICommand OpenFolderInOutside = new(StandardUICommandKind.Open);

    public StandardUICommand StorageCommandDelete = new(StandardUICommandKind.Delete);

    public StandardUICommand StorageCommandRename = new();

    public StandardUICommand OpenManga = new(StandardUICommandKind.Open);

    public static void Initial ()
    {
        Instance ??= new MangaBookCommands();

        Instance.OpenFolderInOutside.ExecuteRequested += (sender , args) =>
        {
            string folderpath = args.Parameter switch
            {
                MangasGroup folder => folder.FolderPath,
                MangaBook book => book.FilePath,
                string str => str,
                _ => null
            };
            ;
            if (folderpath is not null)
            {
                try
                {
                    DJDQfff.ShellAPI.ExplorerFile.ExplorerSelectFile(folderpath);
                }
                catch
                {
                }
            }
            //System.Diagnostics.Process.Start("explorer" , $"/select , {folderpath}");
        };

        Instance.StorageCommandDelete.ExecuteRequested += async (sender , args) =>
        {
            switch (args.Parameter)
            {
                case MangaBook book:
                    _ = await StorageHelper.DeleteSourceFile(book);
                    break;
            }
        };

        Instance.StorageCommandRename.ExecuteRequested += async (sender , args) =>
        {
            switch (args.Parameter)
            {
                case MangaBook book:
                    await StorageHelper.RenameSourceFile(book);
                    break;
            }
        };
        Instance.StorageCommandRename.IconSource = new SymbolIconSource()
        {
            Symbol = Symbol.Rename
        };

        Instance.OpenManga.ExecuteRequested += (sender , args) =>
            {
                if (args.Parameter is MangaBook book)
                {
                    var wayindex = App.Current.AppConfig.AppConfig.MangaOpenWaySetting.ReadMangaWayIndex;

                    try
                    {
                        switch (wayindex)
                        {
                            case 0:
                            RunDefault:
                                WindowHelper.ShowReadWindow(book);
                                break;

                            case 1:

                                Process.Start("explorer" , book.FilePath);
                                // TODO 无法触发，有bug
                                break;

                            case 2:
                                var SelectedExePath = App.Current.AppConfig.AppConfig.MangaOpenWaySetting.UserSelectedReadMangaExePath;
                                if (!File.Exists(SelectedExePath))
                                {
                                    goto RunDefault;
                                }
                                else
                                {
                                    Process.Start(SelectedExePath , $"\"{book.FilePath}\"");
                                }
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        new ToastContentBuilder()
        .AddText($"{book.MangaName}\r{ResourceLoader.GetForViewIndependentUse("Strings").GetString("OpenFailed")}")
        .Show();
                    }
                }
            };
    }
}