namespace EroMangaManager.Core.ViewModels
{
    /// <summary>
    /// 本子标签管理VM
    /// </summary>
    /// <remarks>
    /// 搜索ViewModel
    /// </remarks>
    public class MangaSearchViewModel
    {
        /// <summary>
        /// 隐藏起来的项
        /// </summary>
        private readonly List<string> hidedTags = [];

        /// <summary>
        /// 数据源
        /// </summary>
        private readonly IEnumerable<string> originTags;

        /// <summary>
        /// 选中项
        /// </summary>
        public List<string> SelectedTags = [];

        /// <summary>
        /// 搜索ViewModel
        /// </summary>
        /// <param name="strings"></param>
        public MangaSearchViewModel (IEnumerable<string> strings) => AllTags = new(strings);

        /// <summary>
        /// 对外公开的所有项
        /// </summary>
        public List<string> AllTags { set; get; } = [];

        /// <summary>
        ///
        /// </summary>
        /// <param name="tag"></param>
        public void CancelHideTag (string tag)
        {
            if (hidedTags.Remove(tag))
            {
                AllTags.Add(tag);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tag"></param>
        public void HideTag (string tag)
        {
            if (AllTags.Remove(tag))
            {
                hidedTags.Add(tag);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Initial ()
        {
            AllTags.Clear();
            AllTags.AddRange(originTags);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<string> Search (string query)
        {
            var temptags = new List<string>();
            foreach (var x in AllTags)
            {
                if (x.Contains(query))
                {
                    temptags.Add(x);
                }
            };

            return temptags;
        }
    }
}