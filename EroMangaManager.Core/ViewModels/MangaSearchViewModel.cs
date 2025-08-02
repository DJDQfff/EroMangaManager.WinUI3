
namespace EroMangaManager.Core.ViewModels;

/// <summary>
/// 本子标签管理VM
/// </summary>
/// <remarks>
/// 搜索ViewModel
/// </remarks>
public partial class MangaSearchViewModel : ObservableObject
{

    public List<Manga> Sources
    {
        set
        {
            field = value;
            AllTags = value.SelectMany(x => x.MangaTagsIncludedInFileName)
                            .Distinct()
                            .ToList();
        }

        get;
    }
    /// <summary>
    /// 选中项
    /// </summary>
    public List<string> RequiredTags { get; } = [];

    [ObservableProperty]
    string requiredText = "";

    /// <summary>
    /// 对外公开的所有项
    /// </summary>
    public List<string> AllTags { private set; get; }

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
    public ObservableCollection<Manga> ResultMangas { get; } = [];
    public void Search ()
    {
        ResultMangas.Clear();


        var a = Sources
             .Where(x => x.MangaName.Contains(RequiredText.Trim()))
        .Where(x => RequiredTags.All(y => x.MangaTagsIncludedInFileName.Contains(y)));

        foreach (var x in a)
        {

            ResultMangas.Add(x);

        }

        //}




    }
    /// <summary>
    ///  开始搜索
    /// </summary>
    /// <param name="manganame"></param>
    public void SearchResult (string manganame)
    {
        ResultMangas.Clear();

        var tags = new List<string>(RequiredTags) { manganame };

        var requiredMatchCount = tags.Count;


        //TODO 这里有一个大问题，tag和本子名没有分开。
        // 传入参数是本子名，同样可能本传入到tag里面
        var a = Sources
            .Where(x => tags.Any(y => x.FileDisplayName.Contains(y)));
        foreach (var x in a)
        { ResultMangas.Add(x); }

    }
}