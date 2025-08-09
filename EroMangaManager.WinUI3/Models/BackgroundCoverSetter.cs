using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EroMangaManager.WinUI3.Models;
internal class BackgroundCoverSetter
{
    public Stack<Manga> mangas = [];
    public bool IsWorking = false;
    public async Task Update (IEnumerable<Manga> _mangas)
    {
        foreach(var  manga in _mangas) {mangas.Push(manga); }
        IsWorking = true;
    }

    async Task SequenceWork ()
    {
        if (!IsWorking)
        {
            var a = mangas.FirstOrDefault(x => x.CoverPath == CoverHelper.DefaultCoverPath);
            if (a != null)
            {
                a.CoverPath =
        await CoverHelper.TryCreatCoverFileAsync(a.FilePath , null)
        ?? CoverHelper.DefaultCoverPath;
                //mangas.Remove(a);

                await SequenceWork();

            }

        }
    }
}
