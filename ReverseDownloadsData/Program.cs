// See https://aka.ms/new-console-template for more information
using ReserseDownloadsData;

Console.WriteLine("Hello, World!");

var list = new List<string>();
BookContext context = new BookContext();
var books = context.Books.ToList();
books.ForEach(x => Console.WriteLine(x.Title));
return;
var path = @"D:\test\ReserseDownloadsData\bin\Debug\net8.0\translator.txt";
var lines = File.ReadAllLines(path);
var lines2 = lines.Select(l => l.Trim()).Distinct().ToList();
File.WriteAllLines(@"D:\test\ReserseDownloadsData\bin\Debug\net8.0\translator2.txt" , lines2);
return;
var b = context.Books
    .Select(x => x.ChineseTeam)
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .Distinct()
    .ToList();
foreach (var item in b)
{
    Console.WriteLine(item);
}
System.IO.File.WriteAllLines("translator.txt" , b);

static IEnumerable<string> GetAuthor (List<string> list , BookContext context)
{
    var a = context.Books.Select(x => x.Author).ToList();
    foreach (var item in a)
    {
        var ss = item.Split('(' , ')' , ',' , '[' , ']' , '、' , '（' , '）' , '/' , '\\' , '．');
        list.AddRange(ss);
    }
    var c = list.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).Distinct();
    System.IO.File.WriteAllLines("author.txt" , c);
    return c;
}
