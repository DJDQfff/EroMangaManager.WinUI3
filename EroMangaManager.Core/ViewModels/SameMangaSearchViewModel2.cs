
using CommonLibrary.CollectionFindRepeat;
using CommonLibrary.GroupdItemsLibrary;



using static CommonLibrary.BracketBasedStringParser;

namespace EroMangaManager.Core.ViewModels;
public class SameMangaSearchViewModel2 : RepeatItemsGroup<string , Manga , RepeatItems<string , Manga>>
{
    public List<Manga> Source { set; get; }

    public void Method1 ()
    {
        Func<Manga , string> func = null;
        char[] chars = [' ' , '-' , '+' , '~' , '#'];

        var dic = StringArrayCollection
    .Run(Source , x => Get_OutsideContent(x.FileDisplayName)
    .SelectMany(x => x.Split(chars)))

    .Where(x => x.Value > 1)
    .Where(x => !int.TryParse(x.Key , out _))
    .Where(x => !char.TryParse(x.Key , out _))
    .ToDictionary();
        func = x =>
         Get_OutsideContent(x.FileDisplayName)
         .SelectMany(x => x.Split(chars))
            .FirstOrDefault(y => dic.ContainsKey(y));

        RepeatPairs.Clear();

        var array = Source.Select(x => x.MangaName);

        var a = Source
            .GroupBy(x => x.MangaName)
            .SkipWhile(x => x.Key is null);
        foreach (var cc in a)
        {

            if (cc.Count() > 1)
            {
                var items = new RepeatItems<string , Manga>();
                items.Initial(cc);
                if (!string.IsNullOrWhiteSpace(items.Key))
                {
                    RepeatPairs.Add(items);
                }
            }
        }

    }
    public void Method0 ()
    {
        RepeatPairs.Clear();

        var array = Source.Select(x => x.MangaName);

        var a = Source
            .GroupBy(x => x.MangaName)
            .SkipWhile(x => x.Key is null);
        foreach (var cc in a)
        {

            if (cc.Count() > 1)
            {
                var items = new RepeatItems<string , Manga>();
                items.Initial(cc);
                if (!string.IsNullOrWhiteSpace(items.Key))
                {
                    RepeatPairs.Add(items);
                }
            }
        }

    }

}
