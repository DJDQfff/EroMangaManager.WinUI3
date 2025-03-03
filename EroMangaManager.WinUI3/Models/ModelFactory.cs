using System.Threading;

namespace EroMangaManager.WinUI3.Models;

/// <summary>
/// 基于该平台的实例创建方法
/// </summary>
internal static class ModelFactory
{
    /// <summary>
    /// 初始化MangaBook，同时初始化FileSize
    /// </summary>
    /// <param name="storageFile"></param>
    /// <returns></returns>
    public static MangaBook CreateMangaBook (string filepath)
    {
        var mangaBook = new MangaBook(filepath)
        {
            FileSize = new FileInfo(filepath).Length ,

            //CoverPath = CoverHelper.DefaultCoverPath
            //这个的赋值放到上面方法里面去
        };

        return mangaBook;
    }

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
    /// 添加MangaBook，并在后台初始化封面
    /// </summary>
    /// <param name="mangasFolder"></param>
    /// <param name="StorageFolder"></param>
    /// <returns></returns>
    public static async Task Initial (
        this MangasGroup mangasFolder
    )
    {
        string[] OkExtension = [".zip" , ".7z"];
        mangasFolder.IsInitialing = true;
        mangasFolder.UpdateState = UpdateState.Ing;
        if (Directory.Exists(mangasFolder.FolderPath))
        {
            //var a = DatabaseController.database.FilteredImages.ToArray();

            var files = Directory.GetFiles(mangasFolder.FolderPath);
            var filteredfiles = files
                .Where(x => OkExtension.Contains(Path.GetExtension(x).ToLower()))
                .Select(x => CreateMangaBook(x));
            foreach (var x in filteredfiles)
            {
                x.CoverPath =
                    await CoverHelper.TryCreatCoverFileAsync(x.FilePath , null)
                    ?? CoverHelper.DefaultCoverPath;
                mangasFolder.MangaBooks.Add(x);
            }
        }
        mangasFolder.UpdateState = UpdateState.Over;
        mangasFolder.IsInitialing = false;
    }

    [Obsolete]
    public static async void InitialEachFolders (this ObservableCollectionVM ViewModel)
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
