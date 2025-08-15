using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using EroMangaManager.Core.Models;

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
    public static void InitialGroup2 (this MangasGroup mangasFolder)
    {

        if (Directory.Exists(mangasFolder.FolderPath))
        {
            mangasFolder.UpdateState = UpdateState.Busy;

            //var a = DatabaseController.database.FilteredImages.ToArray();
            //所有子文件作为mangabook
            var filteredfiles = Directory.EnumerateFiles(mangasFolder.FolderPath)
                .Where(x => SupportedType.MangaType.Contains(Path.GetExtension(x).ToLower()))
                .Select(xfile =>
            {
                return new Manga(xfile)
                {
                    CoverPath = CoverHelper.DefaultCoverPath ,

                };
            }
            );

            foreach (var manga in filteredfiles)
            {

                mangasFolder.Mangas.Add(manga);
                App.Current.BackgroundCoverSetter.mangas.Add(manga);

            }
            //所有子文件夹作为mangabook
            var folders = Directory.EnumerateDirectories(mangasFolder.FolderPath)
                .Select(x =>
                {
                    return new Manga(x)
                    {
                        CoverPath = CoverHelper.DefaultCoverPath

                    };
                });

            foreach (var manga in folders)
            {

                mangasFolder.Mangas.Add(manga);
                App.Current.BackgroundCoverSetter.mangas.Add(manga);

            }

            mangasFolder.UpdateState = UpdateState.Over;

        }
    }

    /// <summary>
    /// 添加Manga，并在后台初始化封面
    /// </summary>
    /// <param name="mangasFolder"></param>
    /// <param name="StorageFolder"></param>
    /// <returns></returns>
    public static async Task InitialGroup (this MangasGroup mangasFolder)
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
                var manga = new Manga(xfile)
                {
                    Type = Path.GetExtension(xfile).ToLower()
                };

                await Task.Run(() =>
            {
                var fileinfo = new FileInfo(manga.FilePath);
                manga.FileSize = fileinfo.Length;
                fileinfo = null;
                manga.CoverPath = CoverHelper.DefaultCoverPath;
                //manga.CoverPath =
                //    await CoverHelper.TryCreatCoverFileAsync(manga.FilePath , null)
                //    ?? CoverHelper.DefaultCoverPath;

            });
                mangasFolder.Mangas.Add(manga);
                App.Current.BackgroundCoverSetter.mangas.Add(manga);

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
                    manga.CoverPath = CoverHelper.DefaultCoverPath;

                    //manga.CoverPath =
                    //    CoverHelper.LoadCoverFromInternalFolder(manga.FilePath)
                    //    ?? CoverHelper.DefaultCoverPath;

                });

                mangasFolder.Mangas.Add(manga);
                App.Current.BackgroundCoverSetter.mangas.Add(manga);


            }
            mangasFolder.UpdateState = UpdateState.Over;

        }
    }
    public static async Task InitialFileSize (Manga manga)
    {

        switch (manga.Type)
        {
            case "":
                {
                    manga.FileSize = await Task.Run(() =>
                    {
                        return Directory
      .EnumerateFiles(manga.FilePath , "*.*" , new EnumerationOptions() { RecurseSubdirectories = true })
      .Sum(x => new FileInfo(x).Length);
                    });
                }
                break;
            default:
                {
                    var fileinfo = new FileInfo(manga.FilePath);
                    manga.FileSize = fileinfo.Length;

                    // filestream 也可以获取length;
                    //var rstr = new FileStream(manga.FilePath , FileMode.Open);

                }
                break;
        }


    }
    public static void InitialBasicInfo (Manga manga)
    {
        switch (manga.Type)
        {
            case "":
                {

                }
                break;
            default:
                { }
                break;
        }
        ;
    }
    public static async Task InitialCover (Manga manga)
    {
        switch (manga.Type)
        {
            case "":
                {
                    manga.CoverPath =
                       CoverHelper.LoadCoverFromInternalFolder(manga.FilePath)
                        ?? CoverHelper.DefaultCoverPath;

                }
                break;
            default:
                {
                    manga.CoverPath =
                        await CoverHelper.TryCreatCoverFileAsync(manga.FilePath , null)
                        ?? CoverHelper.DefaultCoverPath;

                }
                break;
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

            await folder.InitialGroup();
        }
    }
}
