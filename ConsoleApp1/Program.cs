using ConsoleApp1;

var a = "abcdefg".ToArray();
var b = "abcdefghik".ToArray();
var diff = StringSearch.SequenceDifferenceDelta(a, b);
Console.WriteLine(diff);
return;

Console.WriteLine();

char[] sperators = [' ', '&', '+', '～', '~', '#', '!', '?', '？', '！', '|', '丶', '、', '•', '﹐'];
var mangas = Directory.GetDirectories("D:\\重复本子").Select(x => new Manga(x));
StringCollection<Manga, string> stringCollection = new()
{
    Action = x => x.MangaName.Split(sperators),
    Sources = mangas,// SimilarMangas.Strings;
    MinItemLength = 1
};
stringCollection.Run2();