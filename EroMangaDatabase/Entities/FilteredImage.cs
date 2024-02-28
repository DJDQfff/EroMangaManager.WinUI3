﻿namespace EroMangaDatabase.Entities
{
    /// <summary>
    /// 要过滤的图片。
    /// 其中：Hash 是唯一的（算法保证唯一），length 不唯一
    /// </summary>
    public class FilteredImage : IDatabaseID
    {
        /// <summary> 主键 </summary>
        public int ID { set; get; }

        /// <summary> ZipEntry 的解压后流大小，判断唯一性第一步 </summary>
        public long ZipEntryLength { set; get; }

        /// <summary> 图片流的sha256哈希值，判断唯一性第二步 </summary>
        public string Hash { set; get; }
    }
}