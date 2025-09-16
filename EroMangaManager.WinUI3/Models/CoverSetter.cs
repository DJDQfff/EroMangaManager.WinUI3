using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EroMangaManager.WinUI3.Models
{
    internal class CoverSetter
    {
        public Stack<Queue<Manga>> stacks = [];

        private bool chandedTop;

        public bool Isworking;

        public async Task AddWork(IEnumerable<Manga> mangas)
        {
            Queue<Manga> queue = new(mangas);
            stacks.Push(queue);
            chandedTop = true;

            if (!Isworking)
            {
                Isworking = true;
                while (stacks.Count > 0)
                {
                    var popqueue = stacks.Peek();
                    while (popqueue.Count > 0)
                    {
                        if (chandedTop)
                        {
                            chandedTop = false; // stacks发生改变，处理新queue
                            break;
                        }
                        var manga = popqueue.Dequeue();
                        if (manga.FileSize == 0) // 以filesize是否为0，来判断漫画信息是否已初始化
                        {
                            await MangaFactory.LoadMangaInfo(manga);
                        }
                    }
                    if (popqueue.Count == 0)
                    {
                        _ = stacks.Pop();
                    }
                }
                if (stacks.Count == 0) { Isworking = false; }
            }
        }
    }
}