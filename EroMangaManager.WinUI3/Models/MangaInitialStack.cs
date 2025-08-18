


namespace EroMangaManager.WinUI3.Models;
internal class MangaInitialStack
{
    public bool IsWorking;

    Stack<Queue<Manga>> stacks;

    public void Add (IEnumerable<Manga> mangas)
    {
        var queue = new Queue<Manga>(mangas);
        stacks.Push(queue);
    }
    public async Task StartAsync ()
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
                if (stacks.Count == 0)
                {
                    break;
                }

                var queue = stacks.Pop();
                while (queue.Count != 0)
                {
                    var manga = queue.Dequeue();
                    if (manga != null && manga.FileSize == 0 && MangaFactory.Exists(manga))
                    {
                        await MangaFactory.InitialCover(manga);

                        await MangaFactory.InitialFileSize(manga);

                    }


                }
                IsWorking = false;


            }

        }
    }
}
