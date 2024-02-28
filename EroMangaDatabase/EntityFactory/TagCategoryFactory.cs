using System.Collections.Generic;

using EroMangaDatabase.Entities;

namespace EroMangaDatabase.EntityFactory
{
    /// <summary>
    /// TagCategory工厂方法
    /// </summary>
    public static class TagCategoryFactory
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="categoryname"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public static TagCategory Creat (string categoryname , IEnumerable<string> keywords)
        {
            string keywordsstring = string.Join("\r" , keywords);
            TagCategory tagKeywords = new TagCategory() { CategoryName = categoryname , Keywords = keywordsstring };
            return tagKeywords;
        }
    }
}