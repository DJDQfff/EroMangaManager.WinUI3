using System.Threading;

using SharpCompress.Common;

namespace EroMangaManager.WinUI3.Models;

/// <summary>
/// 基于该平台的实例创建方法
/// </summary>
internal static class MangaFactory
{
    /// <summary>ViewModel初始化</summary>
    public static void GetAllFolders (
        this ObservableCollectionVM ViewModel ,
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
    /// 添加Manga，并在后台初始化封面
    /// </summary>
    /// <param name="mangasFolder"></param>
    /// <param name="StorageFolder"></param>
    /// <returns></returns>
    public static async Task Initial (this MangasGroup mangasFolder)
    {

        if (Directory.Exists(mangasFolder.FolderPath))
        {
            mangasFolder.UpdateState = UpdateState.Busy;

            //var a = DatabaseController.database.FilteredImages.ToArray();
            List<Task> tasks = [];
            //所有子文件作为mangabook
            var files = Directory.EnumerateFiles(mangasFolder.FolderPath);
            var filteredfiles = files.Where(
                x => SupportedType.MangaType.Contains(Path.GetExtension(x).ToLower())
            );
            foreach (var xfile in filteredfiles)
            {
                var x = new Manga(xfile)
                {
                    Type = Path.GetExtension(xfile).ToLower()
                };

                await Task.Run(async () =>
            {
                var fileinfo = new FileInfo(x.FilePath);
                x.FileSize = fileinfo.Length;
                fileinfo = null;
                //x.CoverPath = CoverHelper.DefaultCoverPath;
                x.CoverPath =
                    await CoverHelper.TryCreatCoverFileAsync(x.FilePath , null)
                    ?? CoverHelper.DefaultCoverPath;

            });
                mangasFolder.Mangas.Add(x);

            }
            //所有子文件夹作为mangabook
            var folders = Directory.EnumerateDirectories(mangasFolder.FolderPath);
            foreach (var mangafo in folders)
            {
                var manga = new Manga(mangafo)
                {
                    Type = string.Empty
                };
                await Task.Run(() =>
                {
                    manga.FileSize = Directory
                        .GetFiles(manga.FilePath , "*.*" , new EnumerationOptions() { RecurseSubdirectories = true })
                        .Sum(x => new FileInfo(x).Length);
                    //manga.CoverPath = CoverHelper.DefaultCoverPath;

                    manga.CoverPath =
                        CoverHelper.LoadCoverFromInternalFolder(manga.FilePath)
                        ?? CoverHelper.DefaultCoverPath;

                });

                mangasFolder.Mangas.Add(manga);

            }
            mangasFolder.UpdateState = UpdateState.Over;

        }
    }

    [Obsolete]
    public static async void InitialEachFoldersInOrder (this ObservableCollectionVM ViewModel)
    {
        // TODO 如果在初始化的时候，移除了这个文件夹，会出错，比如一些大型文件夹
        foreach (var folder in ViewModel.MangaFolders.ToArray())
        {
            //var token = new CancellationTokenSource();
            //App.Current.Tokens.TryAdd(folder , token);

            await folder.Initial();
        }
    }
}
