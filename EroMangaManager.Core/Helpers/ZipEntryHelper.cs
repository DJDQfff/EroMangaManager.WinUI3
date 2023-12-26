using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using EroMangaDatabase.Entities;

using MyLibrary.Standard20;

using SharpCompress.Archives;

namespace EroMangaManager.Core.Helpers
{
    /// <summary>
    ///
    /// </summary>
    public static class ZipEntryHelper
    {
        /// <summary>
        /// 排序zipentry
        /// </summary>
        /// <param name="zipArchive"></param>
        /// <param name="sortFunc"></param>
        /// <returns></returns>
        public static List<string> SortEntriesByName (this IArchive zipArchive , Action<IEnumerable<string>> sortFunc = null)
        {
            List<string> vs = new List<string>();
            foreach (var zipEntry in zipArchive.Entries)
            {
                string entryName = zipEntry.Key;
                vs.Add(entryName);
            }

            if (sortFunc != null)
            {
                sortFunc(vs);
            }
            else
            {
                vs.Sort();
            }

            return vs;
        }

        /// <summary>
        /// 筛选zipentry，如果filteredImages为null，则不进行比较
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="filteredImages">要进行比较的数据，如果为null，则不进行数据比较</param>
        /// <returns></returns>
        public static bool EntryFilter (this IArchiveEntry entry , FilteredImage[] filteredImages)
        {
            string[] okimageextension = new string[] { ".jpg" , ".png" , ".webp" , "bmp" };
            bool canuse = true;

            if (entry.IsDirectory)                      // 排除文件夹entry
                return false;

            string extension = Path.GetExtension(entry.Key).ToLower();

            if (!okimageextension.Contains(extension))
            {
                return false;
            }

            if (filteredImages != null)            // 是否检查图片过滤
            {
                #region 第一个条件：比较数据库，解压后大小

                if (filteredImages.Count(n => n.ZipEntryLength == entry.Size) == 0)
                    return true;

                #endregion 第一个条件：比较数据库，解压后大小

                #region // 第二个条件： 计算流hash，判断唯一性

                string hash = entry.ComputeHash();

                int count = filteredImages.Count(n => n.Hash == hash);

                if (count != 0)
                {
                    canuse = false;
                }

                #endregion // 第二个条件： 计算流hash，判断唯一性
            }

            return canuse;                                                  // 最后一定符合调教
        }
    }
}