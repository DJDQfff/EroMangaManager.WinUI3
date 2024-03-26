using System.Collections.ObjectModel;

namespace EroMangaManager.Core.ViewModels;
internal partial class RenameByTag : ObservableObject

{
    [ObservableProperty]
    MangaBook mangaBook;

    [ObservableProperty]
    string mangaName;

    ObservableCollection<string> RemovedTags = new();
    ObservableCollection<string> SavedTags = new();

    void SetAsMangaName (string mangaName)
    {

    }

}
