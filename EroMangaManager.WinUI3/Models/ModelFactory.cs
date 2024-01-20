using MyLibrary.Standard20;

namespace EroMangaManager.WinUI3.Models
{
    /// <summary>
    /// 基于该平台的实例创建方法
    /// </summary>
    internal static class ModelFactory
    {
        /// <summary>ViewModel初始化</summary>
        public static void GetAllFolders (this ObservableCollectionVM ViewModel , IEnumerable<string> storageFolders)
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

        public static async Task InitialEachFolders (this ObservableCollectionVM ViewModel)
        {
            foreach (var folder in ViewModel.MangaFolders)
            {
                await folder.Initial();
            }
        }

        /// <summary>
        /// 添加MangaBook，并在后台初始化封面
        /// </summary>
        /// <param name="mangasFolder"></param>
        /// <param name="StorageFolder"></param>
        /// <returns></returns>
        public static async Task Initial (this MangasGroup mangasFolder)
        {
            string[] OkExtension = new string[] { ".zip" , ".7z" };
            mangasFolder.IsInitialing = true;
            if (Directory.Exists(mangasFolder.FolderPath))
            {
                var files = Directory.GetFiles(mangasFolder.FolderPath);
                var filteredfiles = files
                    .Where(x => OkExtension.Contains(Path.GetExtension(x).ToLower()))
                    .ToList();

                var a = DatabaseController.database.FilteredImages.ToArray();
                var tasks = new List<Task>();

                var lcts = new LimitedConcurrencyLevelTaskScheduler(2);
                var taskFactory = new TaskFactory(lcts);
                foreach (var storageFile in filteredfiles)
                {
                    var file = storageFile;

                    var manga = CreateMangaBook(file);
                    manga.CoverPath = (await CoverHelper.TryCreatCoverFileAsync(file , a))
                                     ?? CoverHelper.DefaultCoverPath;
                    mangasFolder.MangaBooks.Add(manga);
                }

                await Task.WhenAll(tasks);
            }
            mangasFolder.IsInitialing = false;
        }

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
    }
}