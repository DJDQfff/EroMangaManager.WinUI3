﻿using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

using Org.BouncyCastle.Tls;

namespace EroMangaManager.WinUI3.Commands;

internal class MangaCommands
{
    public StandardUICommand ExportPDF = new(StandardUICommandKind.Save);
    public StandardUICommand OpenFolderInOutside = new(StandardUICommandKind.Open);
    public StandardUICommand OpenManga = new(StandardUICommandKind.Open);
    public StandardUICommand StorageCommandDelete = new(StandardUICommandKind.Delete);
    public StandardUICommand StorageCommandRename = new();
    public static MangaCommands Instance { get; set; }

    public static void Initial()
    {
        Instance ??= new();
        Instance.OpenFolderInOutside.ExecuteRequested += (sender, args) =>
        {
            string folderpath = args.Parameter switch
            {
                MangasGroup folder => folder.FolderPath,
                Manga book => book.FilePath,
                string str => str,
                _ => null
            };
            ;
            if (folderpath is not null)
            {
                try
                {
                    ExplorerFile.ExplorerSelectFile(folderpath);
                }
                catch { }
            }
            //System.Diagnostics.Process.Start("explorer" , $"/select , {folderpath}");
        };

        Instance.StorageCommandDelete.ExecuteRequested += async (sender, args) =>
        {
            switch (args.Parameter)
            {
                case Manga book:
                    var deleteresult = await DialogHelper.ConfirmDeleteSourceFileDialog(book);
                    if (deleteresult) { }
                    break;
            }
        };

        Instance.StorageCommandRename.ExecuteRequested += async (sender, args) =>
        {
            switch (args.Parameter)
            {
                case Manga book:
                    await DialogHelper.RenameSourceFileInDialog(book);
                    break;
            }
        };
        Instance.StorageCommandRename.IconSource = new SymbolIconSource()
        {
            Symbol = Symbol.Rename
        };

        Instance.OpenManga.ExecuteRequested += (sender, args) =>
        {
            if (args.Parameter is Manga book)
            {
                var wayindex = App.Current.AppConfig.AppConfig.MangaOpenWay3.WayIndex;

                try
                {
                    switch (wayindex)
                    {
                        case 0:
                            RunDefault:
                            WindowHelper.ShowReadWindow(book);
                            break;

                        case 1:

                            Process.Start("explorer", book.FilePath);
                            break;

                        case > 1:
                            var SelectedExePath = App.Current.AppConfig.ExePaths.ToList()[wayindex];
                            if (!File.Exists(SelectedExePath))
                            {
                                goto RunDefault;
                            }
                            else
                            {
                                Process.Start(SelectedExePath, $"\"{book.FilePath}\"");
                            }
                            break;
                    }
                }
                catch (Exception)
                {
                    var appNotification = new AppNotificationBuilder()
                        .AddText(
                            $"{book.MangaName}\r{ResourceLoader.GetForViewIndependentUse().GetString("OpenFailed")}"
                        )
                        .BuildNotification();
                    AppNotificationManager.Default.Show(appNotification);
                }
            }
        };

        Instance.ExportPDF.ExecuteRequested += async (sender, args) =>
        {
            switch (args.Parameter)
            {
                case Manga book:
                    await StorageOperation.ExportAsPDFAsync(book);
                    break;
            }
        };
        Instance.ExportPDF.IconSource = new SymbolIconSource() { Symbol = Symbol.Save, };
    }
}
