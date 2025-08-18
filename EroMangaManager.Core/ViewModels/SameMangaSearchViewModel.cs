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

    public bool isWorking = false;
    /// <summary>
    /// 包含几种不同思路查重
    /// </summary>
    /// <param name="index"></param>
    /// <param name="FiltSomes"></param>
    /// <returns></returns>
    public async Task StartSearch (int index , Func<RepeatMangasGroup , bool> FiltSomes = null)
    {

        FiltSomes = FiltSomes ?? (x => !string.IsNullOrWhiteSpace(x.Key));

        Func<Manga , string> func = null;
        Func<Manga , Manga , string> func1 = null;
        char[] chars = [' ' , '-' , '+' , '~' , '#'];
        isWorking = true;
        switch (index)
        {

            case 3:
                {
                    func1 = (manga1 , manga2) =>
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
                    };
                    await StartCompareSequence(Source , func1 , x => !string.IsNullOrWhiteSpace(x));
                }
                break;
            case 2:
                {
                    func1 = (x , y) =>
                   {
                       var way = (Manga manga) =>
                       manga.MangaName.Split(chars);
                       var arrayx = way(x);
                       var arrayy = way(y);


                       if (arrayx.Intersect(arrayy).Any())
                       {
                           return arrayx.Intersect(arrayy).First();
                       }
                       return null;
                   };


                    await StartCompareSequence(Source , func1 , x => !string.IsNullOrWhiteSpace(x));

                }
                break;
            case 1:
                {
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
                    await ByEachKey(Source , func , FiltSomes);

                }
                break;

            case 0:
                func = n => n.MangaName;
                func1 = (x , y) => x.MangaName == y.MangaName ? x.MangaName : null;
                await StartCompareSequence(Source , func1 , x => !string.IsNullOrWhiteSpace(x));
                //await ByEachKey(Source , func , FiltSomes);
                //await Task.Run(() => ByEachKey(Source , func , FiltSomes));

                break;// 直接比较本子名，适用于较短本子名及本子名（括号外的内容）没有分成及部分
        }
        isWorking = false;
    }
}
