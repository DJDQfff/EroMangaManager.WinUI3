using CommunityToolkit.Mvvm.Input;

namespace EroMangaManager.Core.ViewModels;

public partial class ManageTagsViewModel2
{
    public ManageTagsViewModel2 ()
    {
        var a = DatabaseController.database.TagCategorys.ToArray();
        CategoryTags = new(a);
    }

    public event Action CategorysChanged;

    public ObservableCollection<TagCategory> CategoryTags { get; }
    public ObservableCollection<string> ImCategoryedTags { get; } = [];
    public TagCategory selectedTagCategory { set; get; }

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
    /// 添加新分类
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [RelayCommand]
    public async void AddCategory (string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            return;
        if (CategoryTags.FirstOrDefault(x => x.CategoryName == category) is null)
        {
            var tagCategory = DatabaseController.TagCategory_AddCategorySingle(category);

            CategoryTags.Add(tagCategory);

            CategorysChanged?.Invoke();
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
    /// 移除某一分类
    /// </summary>
    /// <param name="category"></param>
    [RelayCommand]
    public void DeleteCategory (string category)
    {
        TagCategory tagCategory = CategoryTags.FirstOrDefault(x => x.CategoryName == category);
        if (tagCategory != null)
        {
            var tags = tagCategory.Tags;
            CategoryTags.Remove(tagCategory);
            DatabaseController.TagCategory_RemoveCategory(tagCategory);
            ImCategoryedTags.AddRange(tags);
            CategorysChanged?.Invoke();
        }
    }

    [RelayCommand]
    public async void SaveToDatabase ()
    {
        foreach (var tagcategory in CategoryTags)
        {
            tagcategory.Keywords = string.Join("\r" , tagcategory.Tags);
        }
        await DatabaseController.database.SaveChangesAsync();
    }

    /// <summary>
    /// 改变某一tag的分类
    /// </summary>
    /// <param name="oldcategory"></param>
    /// <param name="newcategory"></param>
    /// <param name="tags"></param>
    public void TagChangeCategory (TagCategory oldcategory , TagCategory newcategory , IList<string> tags)
    {
        if (oldcategory is null)
        {
            foreach (var tag in tags)
            {
                ImCategoryedTags.Remove(tag);
                newcategory.Tags.Add(tag);
            }
        }
        else
        {
            foreach (var tag in tags)
            {
                oldcategory.Tags.Remove(tag);
                newcategory.Tags.Add(tag);
            }
        }
    }
}