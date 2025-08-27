using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using EroMangaManager.Core.Models;

using Org.BouncyCastle.Crypto.Digests;

using SharpCompress.Common;

using Windows.Web.Syndication;

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
    /// 创建所有manga实例，但是不设置cover，filesize属性（丢到backgroundcoversetter里面后台执行）
    /// </summary>
    /// <param name="mangasFolder"></param>
    /// <param name="StorageFolder"></param>
    /// <returns></returns>
    public static async Task InitialGroup2(this MangasGroup mangasFolder)
    {
        if (Directory.Exists(mangasFolder.FolderPath))
        {
            mangasFolder.UpdateState = UpdateState.Busy;

            //var a = DatabaseController.database.FilteredImages.ToArray();
            //所有子文件作为mangabook
            var filteredfiles = await Task.Run(() =>
                 Directory.EnumerateFiles(mangasFolder.FolderPath)
                  .Where(x => SupportedType.MangaType.Contains(Path.GetExtension(x).ToLower()))
                  .Select(xfile =>
                       new Manga(xfile)
                       {
                           CoverPath = CoverHelper.DefaultCoverPath,
                       }
                  )
            );

            foreach (var manga in filteredfiles)
            {
                mangasFolder.Mangas.Add(manga);
                App.Current.BackgroundCoverSetter.mangas.Add(manga);
            }
            //所有子文件夹作为mangabook
            var folders = await Task.Run(() => Directory.EnumerateDirectories(mangasFolder.FolderPath)
                .Select(x =>
                     new Manga(x)
                     {
                         CoverPath = CoverHelper.DefaultCoverPath
                     }
                ));

            foreach (var manga in folders)
            {
                mangasFolder.Mangas.Add(manga);
                App.Current.BackgroundCoverSetter.mangas.Add(manga);
            }

            mangasFolder.UpdateState = UpdateState.Over;
        }
    }

    /// <summary>
    /// 依次创建manga实例、设置封面、filesize，
    /// </summary>
    /// <param name="mangasFolder"></param>
    /// <param name="StorageFolder"></param>
    /// <returns></returns>
    //public static async Task InitialGroup (this MangasGroup mangasFolder)
    //{
    //    if (Directory.Exists(mangasFolder.FolderPath))
    //    {
    //        mangasFolder.UpdateState = UpdateState.Busy;

    //        //var a = DatabaseController.database.FilteredImages.ToArray();
    //        List<Task> tasks = [];
    //        //所有子文件作为mangabook
    //        var files = Directory.EnumerateFiles(mangasFolder.FolderPath);
    //        var filteredfiles = files.Where(
    //            x => SupportedType.MangaType.Contains(Path.GetExtension(x).ToLower())
    //        );
    //        foreach (var xfile in filteredfiles)
    //        {
    //            var manga = new Manga(xfile)
    //            {
    //                Type = Path.GetExtension(xfile).ToLower()
    //            };

    //            await Task.Run(() =>
    //        {
    //            var fileinfo = new FileInfo(manga.FilePath);
    //            manga.FileSize = fileinfo.Length;
    //            fileinfo = null;
    //            manga.CoverPath = CoverHelper.DefaultCoverPath;
    //            //manga.CoverPath =
    //            //    await CoverHelper.TryCreatCoverFileAsync(manga.FilePath , null)
    //            //    ?? CoverHelper.DefaultCoverPath;

    //        });
    //            mangasFolder.Mangas.Add(manga);
    //            App.Current.BackgroundCoverSetter.mangas.Add(manga);

    //        }
    //        //所有子文件夹作为mangabook
    //        var folders = Directory.EnumerateDirectories(mangasFolder.FolderPath);
    //        foreach (var mangafo in folders)
    //        {
    //            var manga = new Manga(mangafo)
    //            {
    //                Type = string.Empty
    //            };
    //            await Task.Run(() =>
    //            {
    //                manga.FileSize = Directory
    //                    .GetFiles(manga.FilePath , "*.*" , new EnumerationOptions() { RecurseSubdirectories = true })
    //                    .Sum(x => new FileInfo(x).Length);
    //                manga.CoverPath = CoverHelper.DefaultCoverPath;

    //                //manga.CoverPath =
    //                //    CoverHelper.LoadCoverFromInternalFolder(manga.FilePath)
    //                //    ?? CoverHelper.DefaultCoverPath;

    //            });

    //            mangasFolder.Mangas.Add(manga);
    //            App.Current.BackgroundCoverSetter.mangas.Add(manga);

    //        }
    //        mangasFolder.UpdateState = UpdateState.Over;

    //    }
    //}
    public static int CountChapterAmount(Manga manga)
    {
        switch (manga.Type)
        {
            case "":
                {
                    return Directory.EnumerateDirectories(manga.FilePath, "*", new EnumerationOptions() { RecurseSubdirectories = true })
                         .Count(folder => SupportedType.ImageType.Any(imagetype => Directory.EnumerateFiles(folder).Any(file => Path.GetExtension(file).ToLower() == imagetype)));
                }

            default:
                {
                    var archive = ArchiveFactory.Open(manga.FilePath);
                    var folders = archive.Entries.Where(x => x.IsDirectory).ToList();
                    for (var index = folders.Count - 1; index >= 0; index--)
                    {
                        for (int index2 = index - 1; index2 >= 0; index2--)
                        {
                            var key1 = folders[index].Key;
                            var key2 = folders[index2].Key;
                            if (key1.Contains(key2))
                            {
                                folders.RemoveAt(index2);
                                break;
                            }
                            if (key2.Contains(key1))
                            {
                                folders.RemoveAt(index);
                                break;
                            }
                        }
                    }
                    var files = archive.Entries.Where(x => !x.IsDirectory);

                    var group = folders.Count(folder => files.Any(file => file.Key.Contains(folder.Key)));
                    if (group == 0)
                    {
                        group = 1; //TODO 如果所有图片都直接放到文件夹里面，那么章数为0，这里直接设为1
                    }
                    return group;
                }
        }
    }

    public static int CountImageAmount(Manga manga)
    {
        switch (manga.Type)
        {
            case "":
                {
                    return Directory
      .EnumerateFiles(manga.FilePath, "*.*", new EnumerationOptions() { RecurseSubdirectories = true })
      .Count(x => SupportedType.ImageType.Contains(Path.GetExtension(x).ToLower()));
                }
            default:
                {
                    return
                       ArchiveFactory.Open(manga.FilePath).Entries
                         .Count(x => SupportedType.ImageType.Contains(Path.GetExtension(x.Key).ToLower()))
                    ;
                }
        }
    }

    public static long GetFileSize(Manga manga)
    {
        switch (manga.Type)
        {
            case "":
                {
                    return Directory
                        .EnumerateFiles(manga.FilePath, "*.*", new EnumerationOptions() { RecurseSubdirectories = true })
                        .Sum(x => new FileInfo(x).Length);
                }

            default:
                {
                    return new FileInfo(manga.FilePath).Length;

                    // filestream 也可以获取length;
                    //var rstr = new FileStream(manga.FilePath , FileMode.Open);
                }
        }
    }

    public static void InitialBasicInfo(Manga manga)
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

    public static async Task<string> GetCoverFile(Manga manga)
    {
        switch (manga.Type)
        {
            case "":
                {
                    return
                       CoverHelper.LoadCoverFromInternalFolder(manga.FilePath)
                        ?? CoverHelper.DefaultCoverPath;
                }

            default:
                {
                    return
                        await CoverHelper.TryCreatCoverFileAsync(manga.FilePath, null)
                        ?? CoverHelper.DefaultCoverPath;
                }
        }
    }

    public static bool Exists(Manga manga)
    {
        return Directory.Exists(manga.FilePath) || File.Exists(manga.FilePath);
    }

    [Obsolete]
    public static void InitialEachFoldersInOrder(this ObservableCollectionVM ViewModel)
    {
        // TODO 如果在初始化的时候，移除了这个文件夹，会出错，比如一些大型文件夹
        //foreach (var folder in ViewModel.MangaFolders.ToArray())
        //{
        //    var token = new CancellationTokenSource();
        //    App.Current.Tokens.TryAdd(folder, token);

        //    await folder.InitialGroup();
        //}
    }
}