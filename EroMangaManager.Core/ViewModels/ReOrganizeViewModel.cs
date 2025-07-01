using System;
using System.Collections.Generic;
using System.Text;

namespace EroMangaManager.Core.ViewModels;

public class ReOrganizeViewModel : ObservableObject
{
    public ObservableCollection<Models.MangaBook> contentFolders = new();

    public ReOrganizeViewModel(IEnumerable<MangaBook> mangas) { }
}
