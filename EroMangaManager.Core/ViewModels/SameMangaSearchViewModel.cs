using System;
using System.Threading.Tasks;

using CommonLibrary.CollectionFindRepeat;
using CommonLibrary.RepetitiveGroup;

using static CommonLibrary.BracketBasedStringParser;

namespace EroMangaManager.Core.ViewModels;
/// <summary>
/// 查找重复manga的viewmodel
/// </summary>
public class SameMangaSearchViewModel : RepeatItemsGroupWithMethod<string , Manga , RepeatMangasGroup>
{
    /// <summary>
    /// 表示查重方法执行中
    /// </summary>
    char[] chars = [' ' , '-' , '+' , '~' , '#'];

    public bool isWorking = false;
    // TODO 需要优化
    public async Task Method3 (IEnumerable<string> tags)
    {
        foreach (var tag in tags)
        {
            var mangas = Source.Where(x => x.Tags.Contains(tag)).ToList();
            string func (Manga x , Manga y)
            {

                if (x.Tags.Contains(tag)
                && y.Tags.Contains(tag))
                {
                    var piecesx = Get_OutsideContent(x.MangaName);
                    var piecesy = Get_OutsideContent(y.MangaName);
                    if (piecesx.Intersect(piecesy).Any())
                        return "[" + tag + "]" + piecesx.Intersect(piecesy).First();

                }
                return null;
            }
            var _viewmodel = new SameMangaSearchViewModel();
            await _viewmodel.StartCompareSequence(mangas , func , x => !string.IsNullOrWhiteSpace(x));

            foreach (var pair in _viewmodel.RepeatPairs)
            {
                RepeatPairs.Add(pair);
            }

            //await StartCompareSequence(mangas , func , x => !string.IsNullOrWhiteSpace(x));


        }
    }
    // TODO 需要优化
    public async Task Method2 ()
    {
        static string func1 (Manga manga1 , Manga manga2)
        {
            var tags1 = manga1.Tags;
            var tags2 = manga2.Tags;

            var namepieces1 = Get_OutsideContent(manga1.FileDisplayName);
            var namepieces2 = Get_OutsideContent(manga2.FileDisplayName);

            if (tags1.Intersect(tags2).Any() && namepieces1.Intersect(namepieces2).Any())
            {
                return tags1.Intersect(tags2).First() + "|" + namepieces1.Intersect(namepieces2).First();
            }
            return null;
        }

        await StartCompareSequence(Source , func1 , x => !string.IsNullOrWhiteSpace(x));

    }
    public async Task Method1 ()
    {
        Func<Manga , string> func = null;

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
        await ByEachKey(Source , func , x => !string.IsNullOrWhiteSpace(x.Key));

    }

    public async Task Method0 ()
    {
        static string func1 (Manga x , Manga y) => x.MangaName == y.MangaName ? x.MangaName : null;
        await StartCompareSequence(Source , func1 , x => !string.IsNullOrWhiteSpace(x));

    }

}
