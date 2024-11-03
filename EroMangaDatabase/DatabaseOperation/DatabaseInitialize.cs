using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using EroMangaDatabase.Entities;

using SharpCompress.Archives;
using SharpCompress.Readers;

namespace EroMangaDatabase
{
    public partial class BasicController
    {
        /// <summary> 并初始化默认数据 </summary>
        public async Task InitializeDefaultData ()
        {
            // 初始化数据
            List<string>[] vs = new List<string>[]
            {
                new List<string> { PresetTagCategory.全彩.ToString(), "全彩" },
                new List<string>
                {
                    PresetTagCategory.无修.ToString(),
                    "无修",
                    "无修正",
                    "無修正",
                    "無修",
                    "无码",
                    "無码"
                },
                new List<string> { PresetTagCategory.DL版.ToString(), "DL版" },
                new List<string> { PresetTagCategory.刊登.ToString(), "COMIC" },
                new List<string> { PresetTagCategory.CM展.ToString(), "C99" },
                new List<string> { PresetTagCategory.中译.ToString(), "漢化", "中国語", "汉化", "中国翻訳" },
                new List<string> { PresetTagCategory.英译.ToString(), "英訳" },
                new List<string> { PresetTagCategory.作者.ToString(), "国崎蛍" },
                new List<string> { PresetTagCategory.单行本.ToString(), "长篇", "单行本" },
                new List<string> { PresetTagCategory.短篇.ToString(), "短篇" },
            };
            foreach (var list in vs)
            {
                try
                {
                    var temp = database.TagCategorys.Single(n => n.CategoryName == list[0]);
                }
                catch (InvalidOperationException)
                {
                    string tagname = list[0];
                    list.RemoveAt(0);
                    var one = EntityFactory.TagCategoryFactory.Creat(tagname , list);
                    database.TagCategorys.Add(one);
                }
            }
            await database.SaveChangesAsync();
        }

        public string LoadCategoryFromAssembly (string enbededResourceFileName)
        {
            var assembly = typeof(BasicController).Assembly;

            var zip = assembly.GetManifestResourceStream($"EroMangaDatabase.{enbededResourceFileName}.7z");
            zip.Position = 0;
            ReaderOptions readerOptions = new ReaderOptions()
            {
                Password = "F9429775-6EAB-48FC-9F8A-4E079F90AF3F"
            };
            var stream = new MemoryStream();
            var archive = ArchiveFactory.Open(zip , readerOptions);

            foreach (var item in archive.Entries)
            {
                item.WriteTo(stream);
                stream.Position = 0;
                break;
            }
            StreamReader sr = new StreamReader(stream);
            var a = sr.ReadToEnd();
            sr.Close();
            return a;
        }
    }
}