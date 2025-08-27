namespace EroMangaManager.WinUI3.Models;

internal class BackgroundCoverSetter
{
    public readonly List<Manga> mangas = [];
    private bool IsWorking = false;

    //int workcount = 0;
    public async Task LoopWork3()
    {
        if (IsWorking)
        {
            return;
        }
        else
        {
            IsWorking = true;
            while (IsWorking)
            {
                if (mangas.Count == 0)
                {
                    break;
                }
                var manga = mangas.FirstOrDefault(x => x.FileSize == 0);
                // 文件可能被删除
                if (manga != null && MangaFactory.Exists(manga) /*&& manga.FileSize == 0*/ /*manga.CoverPath == CoverHelper.DefaultCoverPath*/)
                {
                    manga.CoverPath = await Task.Run(() => MangaFactory.GetCoverFile(manga));


                    manga.FileSize = await Task.Run(() => MangaFactory.GetFileSize(manga));
                    manga.ImageAmount = await Task.Run(() => MangaFactory.CountImageAmount(manga));
                    manga.ChapterAmount = await Task.Run(() => MangaFactory.CountChapterAmount(manga));
                }
                _ = mangas.Remove(manga);//改回list了，又需要了 .不需要执行，stack的pop方法已经取出最上面的了
            }
            IsWorking = false;
        }
    }

    //public async Task LoopWork2 ()
    //{
    //    if (workcount < 1)
    //    {
    //        if (mangas.Count == 0)
    //        {
    //            return;
    //        }
    //        workcount++;

    //        var manga = mangas.FirstOrDefault(x => x.FileSize == 0);
    //        // 理论上这个manga不可能是null
    //        if (manga != null /*&& manga.FileSize == 0*/ /*manga.CoverPath == CoverHelper.DefaultCoverPath*/)
    //        {
    //            await MangaFactory.InitialCover(manga);

    //            MangaFactory.InitialFileSize(manga);

    //        }
    //        await Task.Run(() => _ = mangas.Remove(manga));//改回list了，又需要了 .不需要执行，stack的pop方法已经取出最上面的了

    //        workcount--;
    //        await LoopWork2();

    //    }

    //}
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