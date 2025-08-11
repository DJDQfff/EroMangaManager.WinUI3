
var mangas = Directory.EnumerateDirectories(Folder_test)
    .Select(x => new Manga(x));
var search = new MangaSearchViewModel();

search.RequiredText = "宫子";

Console.WriteLine(search.ResultMangas.Count);

static void NewMethod (List<Manga> mangas)
{
    SameMangaSearchViewModel viewmodel = new();
    viewmodel.Source = mangas;
    viewmodel.StartSearch(2);
    foreach (var group in viewmodel.RepeatPairs)
    {
        Console.WriteLine(group.Key);
        foreach (var manga in group.Collections)
        {
            Console.WriteLine(manga.FileDisplayName);
        }
        Console.WriteLine();
    }
}

static void NewMethod1 (List<Manga> mangas)
{
    var coolection = new StringCollection<Manga>();
    coolection.Action = x => x.MangaName;
    coolection.MinItemLength = 2;
    coolection.MinOccurTimes = 2;
    coolection.StringsList.AddRange(mangas);
    coolection.Run2();
}