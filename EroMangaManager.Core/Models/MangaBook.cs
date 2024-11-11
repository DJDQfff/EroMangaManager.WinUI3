using CommonLibrary.StringParser;

namespace EroMangaManager.Core.Models;

/// <summary> 本子 </summary>
public partial class MangaBook : ObservableObject
{
    /// <summary>
    /// 封面文件路径
    /// </summary>
    /// <remarks>这个一定要有，不能为null，不然在Image控件加载图像时会异常导致程序闪退</remarks>
    [ObservableProperty]
    private string coverPath;

    /// <summary> 漫画文件路径 </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MangaName))]
    [NotifyPropertyChangedFor(nameof(MangaTagsIncludedInFileName))]
    [NotifyPropertyChangedFor(nameof(FileDisplayName))]
    private string filePath;

    /// <summary> 漫画翻译后的名称 </summary>
    [ObservableProperty]
    private string translatedMangaName;

    /// <summary> 实例化EroManga </summary>
    public MangaBook(string filepath)
    {
        //FileSize = (new FileInfo(filepath)).Length;
        FilePath = filepath;
    }

    /// <summary> 文件Display名（不带扩展名） </summary>
    public string FileDisplayName => Path.GetFileNameWithoutExtension(FilePath);

    /// <summary> 获取文件的扩展名 </summary>
    public string FileExtension => Path.GetExtension(FilePath).ToLower();

    /// <summary> 漫画文件名（全名，带扩展名，不包含文件夹名） </summary>
    public string FileFullName => Path.GetFileName(FilePath);

    /// <summary>
    /// 获取漫画文件大小。单位：字节
    /// </summary>
    public long FileSize { get; set; }

    /// <summary> 漫画文件所在文件夹路径 </summary>
    public string FolderPath => Path.GetDirectoryName(FilePath);

    /// <summary> 本子名字。第一个括号外的内容（括号外内容可能有多个） </summary>
    public string MangaName =>
        BracketBasedStringParser.Get_OutsideContent_Recursion(FileDisplayName);

    /// <summary> 文件名中包含在括号的本子Tag </summary>
    public string[] MangaTagsIncludedInFileName =>
        BracketBasedStringParser.Get_InsideContent(FileDisplayName).ToArray();

    //TODO 翻译漫画名的功能
}
