using System.Resources;

namespace EroMangaManager.Core.IOOperation;

/// <summary>
/// manga的io操作
/// </summary>
public class MangaFileOperation
{
    // 不需要单独写一个rename方法
    //public static void RenameMange(Manga book, string text)
    //{
    //    if (string.IsNullOrWhiteSpace(text))
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        string oldname = book.FilePath;
    //        string newname = Path.Combine(book.FolderPath, text + book.Type);

    //        switch (book.Type)
    //        {
    //            case "":

    //                {
    //                    if (oldname != newname)
    //                    {
    //                        if (!File.Exists(newname))
    //                        {
    //                            Directory.Move(oldname, newname);
    //                            book.FilePath = newname;
    //                        }
    //                    }
    //                }
    //                break;

    //            default:

    //                {
    //                    File.Move(oldname, newname);
    //                    book.FilePath = newname;
    //                }
    //                break;
    //        }
    //    }
    //}

    /// <summary>
    /// 移动、重命名漫画。
    /// </summary>
    /// <param name="book"></param>
    /// <param name="targetfolder">目标文件夹</param>
    /// <param name="newname">新名字</param>
    public static string MoveManga(Manga book, string targetfolder, string newname)
    {
        targetfolder ??= book.FolderPath; //这个路径并没有验证是否存在

        newname ??= book.FileDisplayName;
        var newpath = Path.Combine(targetfolder, newname + book.Type);

        // TODO 可能重名
        switch (book.Type)
        {
            case "":

                {
                    Directory.Move(book.FilePath, newpath);
                    return newpath;
                    //book.FilePath = newpath;
                    // 由于manganame使用MVVM绑定到了UI，所以跨线程，不能直接操作
                }

            default:

                {
                    File.Move(book.FilePath, newpath);
                    return newpath;
                    //book.FilePath = newpath;
                }
        }
    }
}