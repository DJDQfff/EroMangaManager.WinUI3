
using EroMangaManager.Core.ViewModels;

using GroupedItemsLibrary;

Console.WriteLine();

var mangas = Directory.GetFiles(Folder_3).Select(x => new Manga(x)).ToList();
;
SameMangaSearchViewModel viewmodel = new();
viewmodel.StartSearch(mangas , 2);
foreach (var group in viewmodel.mangaBookViewModel.RepeatPairs)
{
    Console.WriteLine(group.Key);
    foreach (var manga in group.Collections)
    {
        Console.WriteLine(manga.FileDisplayName);
    }
    Console.WriteLine();
}
