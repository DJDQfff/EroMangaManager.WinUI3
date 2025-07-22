
namespace EroMangaManager.Core.ViewModels;

/// <summary>
/// 本子标签管理VM
/// </summary>
/// <remarks>
/// 搜索ViewModel
/// </remarks>
public partial class MangaSearchViewModel (ObservableCollectionVM vm)
{


    /// <summary>
    /// 选中项
    /// </summary>
    public List<string> RequiredTags = [];


    /// <summary>
    /// 对外公开的所有项
    /// </summary>
    public IEnumerable<string> AllTags => vm.AllTags;

    /// <summary>
    /// 可能需要的tag
    /// </summary>
    public ObservableCollection<string> AlailableTags { get; } = [];
    /// <summary>
    ///
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public void FiltTags (string query)
    {
        AlailableTags.Clear();

        foreach (var x in AllTags.Except(RequiredTags))
        {
            if (x.Contains(query))
            {
                AlailableTags.Add(x);
            }
        }
        ;


    }
    /// <summary>
    /// 按搜索条件筛选出来的本子
    /// </summary>
    public ObservableCollection<MangaBook> MangaList { get; } = [];
    /// <summary>
    ///  开始搜索
    /// </summary>
    /// <param name="manganame"></param>
    public void SearchResult (string manganame)
    {
        MangaList.Clear();

        var tags = new List<string>(RequiredTags) { manganame };

        var requiredMatchCount = tags.Count;

        var allmangas = vm.MangaList;

        //TODO 这里有一个大问题，tag和本子名没有分开。
        // 传入参数是本子名，同样可能本传入到tag里面
        var a = vm.MangaList
            .Where(x => tags.Any(y => x.FileDisplayName.Contains(y)));
        foreach (var x in a)
        { MangaList.Add(x); }

    }
}