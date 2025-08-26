namespace EroMangaManager.WinUI3.Models;

internal class MangaInitialStack
{
    public bool IsWorking;

    private Stack<Queue<Manga>> stacks = new();

    private int a { set; get; }

    public void Add(IEnumerable<Manga> mangas)
    {
        if (!mangas.Any())
            return;

        var queue = new Queue<Manga>(mangas);
        stacks.Push(queue);
    }

    public async Task StartAsync()
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
                if (stacks.Count != 0)
                {
                    var queue = stacks.Peek();
                    if (queue.Count != 0)
                    {
                        var manga = queue.Dequeue();
                        // TODO 不知道为什么，不能获取到源manga，获得的好像是个副本
                        if (manga != null && manga.FileSize == 0 && MangaFactory.Exists(manga))
                        {
                            await MangaFactory.InitialCover(manga);

                            await MangaFactory.InitialFileSize(manga);
                        }
                    }
                    else
                    {
                        _ = stacks.Pop();
                    }
                }
                else
                {
                    IsWorking = false;
                }
            }
        }
    }
}