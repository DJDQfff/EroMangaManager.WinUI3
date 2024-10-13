using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;

namespace EroMangaManager.Core.ViewModels;

internal partial class ManageTagsViewModel2
{
    public ObservableCollection<TagCategory> CategoryTags { get; }
    public ObservableCollection<string> ImCategoryedTags { get; } = [];
    public ManageTagsViewModel2 ()
    {
        var a = EroMangaDatabase.BasicController.DatabaseController.database.TagCategorys.ToArray();
        CategoryTags = new(a);
    }
    /// <summary>
    /// 已分类的tag
    /// </summary>
    public IEnumerable<string> Tags
    {
        get
        {
            var list = new List<string>();

            foreach (var a in CategoryTags)
            {
                list.AddRange(a.Tags);
            }
            return list.Distinct();
        }
    }

    /// <summary>
    /// 传入tags，先过滤已分类的tag，剩下的全部挪到未分类里面
    /// </summary>
    /// <param name="tags"></param>
    public void AddUnCategoryTags (IEnumerable<string> tags)
    {
        var a = tags.Except(Tags).Distinct();

        ImCategoryedTags.AddRange(a);
    }
    /// <summary>
    /// 添加新分类
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [RelayCommand]
    public void AddCategory (string category)
    {
        TagCategory tagCategory = new TagCategory();
        tagCategory.CategoryName = category;
        CategoryTags.Add(tagCategory);
    }
    /// <summary>
    /// 移除某一分类
    /// </summary>
    /// <param name="category"></param>
    [RelayCommand]
    public void DeleteCategory (string category)
    {
        TagCategory tagCategory = CategoryTags.Where(x => x.CategoryName == category).FirstOrDefault();
        if (tagCategory != null)
        {
            CategoryTags.Remove(tagCategory);
        }

    }

}
