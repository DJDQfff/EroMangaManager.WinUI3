﻿using System.Threading;

namespace EroMangaManager.Core.ViewModels
{
    /// <summary>
    /// 所有需要持续观察的集合都放在这，ViewModel
    /// </summary>
    public class ObservableCollectionVM
    {
        /// <summary>
        /// 出现无法解析的Manga时引发
        /// </summary>
        public event Action<string> ErrorZipEvent;

        /// <summary>
        /// 完成某项任务时引发
        /// </summary>
        public event Action<string> WorkDoneEvent;
        /// <summary>
        /// 任务失败事件
        /// </summary>

        public event Action<string> WorkFailedEvent;

        public Action<MangaBook> FastSetAction;
        public Action<MangaBook> SlowSetAction;
        /// <summary>
        /// 本子文件夹集合
        /// </summary>
        public ObservableCollection<MangasGroup> MangaFolders { get; } = [];
        private Dictionary<MangasGroup , CancellationTokenSource> tokenDic = new();

        /// <summary>
        /// 无法找到的文件夹
        /// </summary>
        public ObservableCollection<string> MissingFolders { set; get; } = [];

        /// <summary>存放zip文件的文件夹</summary>
        public List<string> StorageFolders => MangaFolders.Select(n => n.FolderPath).ToList();

        /// <summary>各漫画zip</summary>
        ///
        public List<MangaBook> MangaList
        {
            get
            {
                List<MangaBook> list = [];
                foreach (MangasGroup folder in MangaFolders)
                {
                    list.AddRange(folder.MangaBooks);
                }
                return list;
            }
        }
        /// <summary>
        /// 所有标签
        /// </summary>
        public IEnumerable<string> AllTags
        {
            get
            {
                List<string> alltags = [];
                foreach (var manga in MangaList)
                {
                    var tags = manga.MangaTagsIncludedInFileName;
                    alltags.AddRange(tags);
                }
                return alltags.Distinct();

                ;
            }
        }

        /// <summary>
        /// MangasFolder是否正在更新，有任意一个是则返回true
        /// </summary>
        public bool IsContentInitializing => MangaFolders.Any((x) => x.IsInitialing == true);

        /// <summary>
        /// 确保已添加文件夹，并添加到集合。如果已存在这个folder，则返回true;否则返回false并创建新的
        /// </summary>
        /// <returns></returns>
        public bool EnsureAddFolder (string path , out MangasGroup mangasFolder)
        {
            if (StorageFolders.Contains(path))
            {
                mangasFolder = MangaFolders.Single(x => x.FolderPath == path);
                return true;
            }
            else
            {
                mangasFolder = new MangasGroup(path);
                MangaFolders.Add(mangasFolder);

                return false;
            }
        }
        public void StartGroup (MangasGroup group)
        {
            if (tokenDic.ContainsKey(group))
            {
                CancellationTokenSource cancellationTokenSource = new();

                tokenDic.Add(group , cancellationTokenSource);
            }
        }

        /// <summary>
        /// 移除文件夹，并从集合中移除文件夹及下属漫画 （只移除，不删除）
        /// 1.从系统API中移除
        /// 2.从FolderList里移除
        /// 3.从MangaList里移除文件夹下属漫画
        /// </summary>
        public void RemoveFolder (MangasGroup group)
        {
            if (tokenDic.TryGetValue(group , out var value))
            {
                tokenDic.Remove(group);

                value.Cancel();
            }

            MangaFolders.Remove(group);
        }

        /// <summary>
        /// 移除一个本子
        /// </summary>
        /// <param name="mangaBook"></param>
        public void RemoveManga (MangaBook mangaBook)
        {
            string folderpath = mangaBook.FolderPath;
            MangasGroup folder = MangaFolders.Single(x => x.FolderPath == folderpath);
            folder.RemoveManga(mangaBook);
        }

        /// <summary>
        /// 事情完成时发生
        /// </summary>
        /// <param name="message"></param>
        public void WorkDone (string message) => WorkDoneEvent?.Invoke(message);
        /// <summary>
        /// 任务失败
        /// </summary>
        /// <param name="message"></param>
        public void WorkFailed (string message) => WorkFailedEvent?.Invoke(message);

        /// <summary>
        /// 发现错误漫画时引发
        /// </summary>
        /// <param name="manganame"></param>
        public void ErrorMangaEvent (string manganame)
        {
            ErrorZipEvent?.Invoke(manganame);
        }
    }
}