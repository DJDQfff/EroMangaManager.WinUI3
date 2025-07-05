using System;
using System.Collections.Generic;
using System.Text;

namespace EroMangaManager.Core.ViewModels;

/// <summary>
/// 重新管理页面
/// </summary>
/// <remarks>
///
/// </remarks>
/// <param name="mangas"></param>
public class ReOrganizeViewModel (IEnumerable<MangaBook> mangas) : ObservableObject
{
    /// <summary>
    ///
    /// </summary>
    public ObservableCollection<Models.MangaBook> contentFolders = [];
}
