﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

using EroMangaManager.Core.Models;

using SharpCompress;

namespace EroMangaManager.Core.ViewModels
{
    /// <summary>
    /// 本子文件夹，包含一大堆本子
    /// </summary>
    public class MangasFolder : INotifyPropertyChanged
    {
        /// <summary>
        /// 文件夹路径，一开始是作为文件夹设计的，后来不作为文件夹，仅作为本子统一集合
        /// </summary>
        public string FolderPath { get; }

        private bool _IsInitialiing = false;

        /// <summary>
        /// 对外展示的信息，考虑将这个属性改名为Description
        /// </summary>
        public string ShowString { set; get; }

        /// <summary>
        /// 是否在更新数据
        /// </summary>
        public bool IsInitialing
        {
            set
            {
                _IsInitialiing = value;
                PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(""));
            }
            get => _IsInitialiing;
        }

        /// <summary>
        /// 本子集合
        /// </summary>
        public ObservableCollection<MangaBook> MangaBooks { get; } = new ObservableCollection<MangaBook>();
        /// <summary>
        /// 所有标签
        /// </summary>
        public IEnumerable<string> AllTags
        {
            get
            {
                List<string> tags = new List<string>();
                MangaBooks.ForEach(X => tags.AddRange(X.MangaTagsIncludedInFileName));
                return tags;
            }
        }
        /// <summary>
        /// 不当文件夹用，所以不指定文件夹路径
        /// </summary>
        public MangasFolder ()
        { }

        /// <summary>
        /// 当文件夹用，需要指定文件夹路径
        /// </summary>
        /// <param name="storageFolderPath"></param>
        public MangasFolder (string storageFolderPath)
        {
            FolderPath = storageFolderPath;
            ShowString = storageFolderPath;
        }

        /// <summary>
        /// 更新通知
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 对内部漫画进行排序
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="func"></param>
        public void SortMangaBooks<TKey> (Func<MangaBook , TKey> func)
        {
            var list = MangaBooks.OrderBy(func);        //OrderBy方法不会修改源数据，返回的值是与源挂钩的，源清零，返回值也清零

            var list2 = new List<MangaBook>(list);
            MangaBooks.Clear();

            foreach (var book in list2)
            {
                MangaBooks.Add(book);
            }
        }

        /// <summary>
        /// 移除一个本子
        /// </summary>
        /// <param name="mangaBook"></param>
        public void RemoveManga (MangaBook mangaBook)
        {
            MangaBooks.Remove(mangaBook);
        }
    }
}