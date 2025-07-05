using System;
using System.Collections.Generic;
using System.Text;

namespace EroMangaManager.Core.Setting;
/// <summary>
/// 支持的格式
/// </summary>
public class SupportedType
{
    /// <summary>
    /// 空字符串是文件夹
    /// </summary>
    public static string[] MangaType => [string.Empty , ".zip" , ".7z"];
    /// <summary>
    /// 内部支持的图片格式
    /// </summary>
    public static string[] ImageType => [".png" , ".bmp" , ".jpg" , ".jpeg" , ".webp"];

}
