using System.Threading;

using SharpCompress.Common;

namespace EroMangaManager.WinUI3.Models;

/// <summary>
/// 基于该平台的实例创建方法
/// </summary>
internal static class MangaFactory
{
    /// <summary>ViewModel初始化</summary>
    public static void GetAllFolders(
        this ObservableCollectionVM ViewModel,
        IEnumerable<string> storageFolders
    )
    {
        ViewModel.MangaFolders.Clear();

        foreach (var folder in storageFolders)
        {
            //不存在则跳过
            if (Directory.Exists(folder))
            {
                var mangasFolder = new MangasGroup(folder);
                ViewModel.MangaFolders.Add(mangasFolder);
            }
        }
    }

    /// <summary>
    /// 添加MangaBook，并在后台初始化封面
    /// </summary>
    /// <param name="mangasFolder"></param>
    /// <param name="StorageFolder"></param>
    /// <returns></returns>
    public static async Task Initial(this MangasGroup mangasFolder)
    {
        mangasFolder.IsInitialing = true;
        mangasFolder.UpdateState = UpdateState.Ing;

        if (Directory.Exists(mangasFolder.FolderPath))
        {
            //var a = DatabaseController.database.FilteredImages.ToArray();

            //所有子文件作为mangabook
            var files = Directory.GetFiles(mangasFolder.FolderPath);
            var filteredfiles = files.Where(
                x => SupportedType.MangaType.Contains(Path.GetExtension(x).ToLower())
            );
            foreach (var xfile in filteredfiles)
            {
                var x = new MangaBook(xfile);

                x.MangaType = Path.GetExtension(xfile).ToLower();
                var fileinfo = new FileInfo(x.FilePath);
                x.FileSize = fileinfo.Length;
                fileinfo = null;
                x.CoverPath =
                    await CoverHelper.TryCreatCoverFileAsync(x.FilePath, null)
                    ?? CoverHelper.DefaultCoverPath;
                mangasFolder.MangaBooks.Add(x);
            }
            //所有子文件夹作为mangabook
            var folders = Directory
                .GetDirectories(mangasFolder.FolderPath)
                .Select(x => new MangaBook(x));
            foreach (var manga in folders)
            {
                manga.MangaType = string.Empty;
                manga.FileSize = Directory
                    .GetFiles(manga.FilePath)
                    .Sum(x => new FileInfo(x).Length);

                manga.CoverPath =
                    CoverHelper.LoadCoverFromInternalFolder(manga.FilePath)
                    ?? CoverHelper.DefaultCoverPath;

                mangasFolder.MangaBooks.Add(manga);
            }
        }
        mangasFolder.UpdateState = UpdateState.Over;
        mangasFolder.IsInitialing = false;
    }

    [Obsolete]
    public static async void InitialEachFolders(this ObservableCollectionVM ViewModel)
    {
        // TODO 如果在初始化的时候，移除了这个文件夹，会出错，比如一些大型文件夹
        foreach (var folder in ViewModel.MangaFolders.ToArray())
        {
            var token = new CancellationTokenSource();
            //App.Current.Tokens.TryAdd(folder , token);

            await folder.Initial();
        }
    }
}
