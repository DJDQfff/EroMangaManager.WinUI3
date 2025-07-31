
var mangas = Directory.EnumerateDirectories(Folder_2)
    .Select(x => new Manga(x));

var error = Directory.EnumerateDirectories(Folder_2)
    .SelectMany(x => Get_OutsideContent(Path.GetFileName(x)))
    .Where(x => BracketBasedStringParser.ContainAnyBrackets(x));
foreach (var manga in error)
{
    Console.WriteLine(manga);
}
static void NewMethod (List<Manga> mangas)
{
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