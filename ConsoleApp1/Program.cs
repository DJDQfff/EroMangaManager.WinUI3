var folderbika = @"D:\Downloads\bika_downloads\commies";
var foldercheck = "d:/本子/检查";
var mangas = Directory.EnumerateDirectories(foldercheck)
    .Select(x => new Manga(x));

StringCollection<Manga> stringCollection = new();
stringCollection.Action = x => x.MangaName;
stringCollection.Sources = mangas;
stringCollection.Run2();
foreach (var itemi in stringCollection.RepeatList)
{
    Console.WriteLine(itemi.Content);
}