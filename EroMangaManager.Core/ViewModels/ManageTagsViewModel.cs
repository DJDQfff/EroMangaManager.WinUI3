using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;

using static EroMangaDatabase.BasicController;

namespace EroMangaManager.Core.ViewModels;

public partial class ManageTagsViewModel : ObservableObject
{
    public ObservableCollection<string> Categorys { get; set; }

    [ObservableProperty]
    Dictionary<string, ObservableCollection<string>> keyValuePairs;

    /// <summary>
    /// 未分类的tag
    /// </summary>
    public ObservableCollection<string> UnCategoryTags { set; get; } = new();

    [ObservableProperty]
    string displayedCategory;

    [ObservableProperty]
    ObservableCollection<string> displayedTags;

    public ManageTagsViewModel()
    {
        var a = DatabaseController.TagCategory_QueryAll();

        keyValuePairs = a.ToDictionary(x => x.Key, x => new ObservableCollection<string>(x.Value));
        Categorys = new(a.Keys);
    }

    /// <summary>
    /// 已分类的tag
    /// </summary>
    public IEnumerable<string> Tags
    {
        get
        {
            var list = new List<string>();

            foreach (var a in keyValuePairs)
            {
                list.AddRange(a.Value);
            }
            return list.Distinct();
        }
    }

    public void AddUnCategoryTags(IEnumerable<string> tags)
    {
        var a = tags.Except(Tags).Distinct();

        UnCategoryTags.AddRange(a);
    }

    partial void OnDisplayedCategoryChanged(string value)
    {
        DisplayedTags = this[value];
    }

    public ObservableCollection<string> this[string category] => KeyValuePairs[category];

    /// <summary>
    /// 添加新分类
    /// </summary>
    /// <param name="category"></param>
    /// <param name="strings"></param>
    /// <returns></returns>
    [RelayCommand]
    public void AddCategory(string category)
    {
        KeyValuePairs.Add(category, new ObservableCollection<string>());
    }

    /// <summary>
    /// 移除某一分类
    /// </summary>
    /// <param name="category"></param>
    [RelayCommand]
    public void DeleteCategory(string category)
    {
        UnCategoryTags.AddRange(KeyValuePairs[category]);
        KeyValuePairs.Remove(category);
    }

    /// <summary>
    /// 改变某一tag的分类
    /// </summary>
    /// <param name="oldcategory"></param>
    /// <param name="newcategory"></param>
    /// <param name="tags"></param>
    public void TagChangeCategory(string oldcategory, string newcategory, IList<string> tags)
    {
        foreach (var tag in tags)
        {
            KeyValuePairs[oldcategory].Remove(tag);
            KeyValuePairs[newcategory].Add(tag);
        }
    }
}
