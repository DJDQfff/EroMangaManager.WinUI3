using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EroMangaManager.Core.Models;

namespace EroMangaManager.WinUI3.Models;
internal class BackgroundCoverSetter
{
    public readonly List<Manga> mangas = [];
    bool IsWorking = false;
    int workcount = 0;
    public async Task LoopWork2 ()
    {

        if (workcount < 1)
        {
            if (mangas.Count == 0)
            {
                return;
            }
            workcount++;

            var manga = mangas.FirstOrDefault(x => x.FileSize == 0);
            // 理论上这个manga不可能是null
            if (manga != null /*&& manga.FileSize == 0*/ /*manga.CoverPath == CoverHelper.DefaultCoverPath*/)
            {
                await MangaFactory.InitialCover(manga);

                MangaFactory.InitialFileSize(manga);

            }
            mangas.Remove(manga);//改回list了，又需要了 .不需要执行，stack的pop方法已经取出最上面的了

            workcount--;
            await LoopWork2();

        }

    }
    //async Task LoopWork ()
    //{
    //    if (!IsWorking)
    //    {
    //        IsWorking = true;
    //        var manga = mangas.Pop();
    //        if (manga != null && manga.CoverPath == CoverHelper.DefaultCoverPath)
    //        {
    //            await MangaFactory.InitialCover(manga);
    //            MangaFactory.InitialFileSize(manga);
    //            //mangas.Remove(manga);// 不需要执行，stack的pop方法已经取出最上面的了

    //            await LoopWork();

    //        }
    //        IsWorking = false;
    //    }
    //}
}
