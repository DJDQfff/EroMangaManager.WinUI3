using System;
using System.Collections.Generic;
using System.Text;

namespace EroMangaManager.Core.Setting;
public class SupportedType
{
    // 空字符串是文件夹
    public static string[] MangaType => [string.Empty , ".zip" , ".7z"];

    public static string[] ImageType => [".png" , ".bmp" , ".jpg" , ".jpeg" , ".webp"];

}
