﻿using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;

using static EroMangaDatabase.BasicController;

namespace EroMangaManager.Core.ViewModels;

public partial class ManageTagsViewModel : ObservableObject
{
    /// <summary>
    ///
    /// </summary>
    public string[] Categorys => keyValuePairs.Keys.ToArray();

    public event Action CategorysChanged;

    Dictionary<string, ObservableCollection<string>> keyValuePairs;

    /// <summary>
    /// 未分类的tag
    /// </summary>
    public ObservableCollection<string> UnCategoryTags { set; get; } = new();

    [ObservableProperty]
    string displayedCategory;

    [ObservableProperty]
    ObservableCollection<string> displayedTags;

    /// <summary>
    ///
    /// </summary>
    public ManageTagsViewModel()
    {
        var a = DatabaseController.TagCategory_QueryAll();

        keyValuePairs = a.ToDictionary(x => x.Key, y => new ObservableCollection<string>(y.Value));
        //Categorys = new(a.Keys);
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

    /// <summary>
    ///
    /// </summary>
    /// <param name="tags"></param>
    public void AddUnCategoryTags(IEnumerable<string> tags)
    {
        var a = tags.Except(Tags).Distinct();

        UnCategoryTags.AddRange(a);
    }

    partial void OnDisplayedCategoryChanged(string value)
    {
        if (value is not null)
        {
            DisplayedTags = this[value];
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public ObservableCollection<string> this[string category] => keyValuePairs[category];

    /// <summary>
    /// 添加新分类
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [RelayCommand]
    public void AddCategory(string category)
    {
        if (!string.IsNullOrWhiteSpace(category))
        {
            keyValuePairs.Add(category, new ObservableCollection<string>());
            OnPropertyChanged(nameof(Categorys));
            CategorysChanged?.Invoke();
        }
    }

    /// <summary>
    /// 移除某一分类
    /// </summary>
    /// <param name="category"></param>
    [RelayCommand]
    public void DeleteCategory(string category)
    {
        UnCategoryTags.AddRange(keyValuePairs[category]);
        keyValuePairs.Remove(category);
        OnPropertyChanged(nameof(Categorys));
        CategorysChanged?.Invoke();
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
            keyValuePairs[oldcategory].Remove(tag);
            keyValuePairs[newcategory].Add(tag);
        }
    }
}
