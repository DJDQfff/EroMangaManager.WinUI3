// See https://aka.ms/new-console-template for more information
using ReserseDownloadsData;

using ReverseDownloadsData;

var context = new BookContext();
var favorites = context.Favorites.ToList();
Console.WriteLine(favorites.Count);
foreach (var favorite in favorites)
{
    var book = context.Books.SingleOrDefault(x => x.Id == favorite.Id);
    if (book != null)
    {
        Console.WriteLine("*********");

        Console.WriteLine(book.Title);
        Console.WriteLine(book.Description);
        Console.WriteLine(book.Author);
    }
}
