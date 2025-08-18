
var mangas = Directory.EnumerateDirectories(@"D:\Downloads\bika_downloads\commies")
    .Select(x => new Manga(x));

