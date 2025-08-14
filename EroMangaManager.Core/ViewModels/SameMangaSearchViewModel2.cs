
using System.Threading.Tasks;

using CommonLibrary.CollectionFindRepeat;
using CommonLibrary.RepetitiveGroup;

using iText.Commons.Bouncycastle.Asn1.X509;

using static CommonLibrary.BracketBasedStringParser;

namespace EroMangaManager.Core.ViewModels;
public class SameMangaSearchViewModel2 : GroupsViewModel<string , Manga , RepeatMangasGroup>
{
    public List<Manga> Source { set; get; }
    public bool isWorking = false;

    public async Task Method1 ()
    {
        isWorking = true;
        RepeatPairs.Clear();
        while (true)
        {
            var group = new RepeatMangasGroup()
            {
                Key = Source[^1].MangaName
            };
            await Task.Run(() =>
            {
                for (int index = Source.Count - 1 ; index >= 0 ; index--)
                {
                    if (Source[index].MangaName == group.Key)
                    {
                        group.AddElement(Source[index]);
                        Source.RemoveAt(index);
                    }
                }

            });
            if (group.Collections.Count > 1)
            {
                RepeatPairs.Add(group);
            }
        }
        isWorking = false;


    }

    /// <summary>
    /// 异步比较manganame，但是一次只比较一个，非常慢
    /// </summary>
    /// <returns></returns>
    public async Task Method0 ()
    {
        isWorking = true;
        RepeatPairs.Clear();
        while (true)
        {
            var group = new RepeatMangasGroup()
            {
                Key = Source[^1].MangaName
            };
            Source.RemoveAt(Source.Count - 1);
            await Task.Run(() =>
             {
                 for (int index = Source.Count - 1 ; index >= 0 ; index--)
                 {
                     if (Source[index].MangaName == group.Key)
                     {
                         group.AddElement(Source[index]);
                         Source.RemoveAt(index);
                     }
                 }

             });
            if (group.Collections.Count > 1)
            {
                RepeatPairs.Add(group);
            }
            else
            {
                group.Dispose();
            }
        }
        isWorking = false;


    }

}
