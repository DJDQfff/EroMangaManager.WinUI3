﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLibrary.CollectionFindRepeat;

using GroupedItemsLibrary;

using static CommonLibrary.BracketBasedStringParser;

namespace EroMangaManager.Core.ViewModels;
public class SameMangaSearchViewModel
{

    public ItemsGroupsViewModel<string , Manga , RepeatMangasGroup> mangaBookViewModel { get; } = new();
    public void StartSearch (IList<Manga> mangaList , int index , Func<RepeatMangasGroup , bool> FiltSomes = null)
    {

        FiltSomes = FiltSomes ?? (x => !string.IsNullOrWhiteSpace(x.Key));

        Func<Manga , string> func = null;
        char[] chars = [' ' , '-' , '+' , '~'];

        switch (index)
        {
            case 0:
                func = n => n.MangaName;

                mangaBookViewModel.StartGroup(mangaList , func , FiltSomes);

                break;// 直接比较本子名，适用于较短本子名及本子名（括号外的内容）没有分成及部分
            case 1:
                {
                    var dic = StringArrayCollection
                        .Run(mangaList , x => Get_OutsideContent(x.FileDisplayName)
                        .SelectMany(x => x.Split(chars)))
                        .Where(x => x.Value > 1)
                        .Where(x => !int.TryParse(x.Key , out _))
                        .Where(x => !char.TryParse(x.Key , out _))
                        .ToDictionary();
                    func = x =>
                     Get_OutsideContent(x.FileDisplayName)
                     .SelectMany(x => x.Split(chars))
                        .FirstOrDefault(y => dic.ContainsKey(y));
                    mangaBookViewModel.StartGroup(mangaList , func , FiltSomes);

                }
                break;
            case 2:
                {
                    Func<Manga , Manga , string> func1 = (x , y) =>
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



                    mangaBookViewModel.StartCompareSequence(mangaList , func1 , x => !string.IsNullOrWhiteSpace(x));

                }
                break;
            case 3:
                {

                }
                break;
        }
    }
}
