namespace EroMangaManager.Core.Models;

/// <summary> 本子 </summary>
public partial class Manga : ObservableObject
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
    [NotifyPropertyChangedFor(nameof(FolderPath))]
    [NotifyPropertyChangedFor(nameof(FileFullName))]
    private string filePath;

    /// <summary> 漫画翻译后的名称 </summary>
    [ObservableProperty]
    private string translatedMangaName;

    /// <summary> 实例化EroManga </summary>
    public Manga (string filepath)
    {
        FilePath = filepath;
    }

    /// <summary>
    ///本子类型，可以为.zip .7z 文件夹，这也是对应的文件后缀名，文件夹的话为“”空字符串
    /// </summary>
    public string Type { get; set; }

    /// <summary> 文件Display名（不带扩展名） </summary>
    public string FileDisplayName
    {
        get
        {
            if (Directory.Exists(FilePath))
                return Path.GetFileName(FilePath);
            if (File.Exists(FilePath))
                return Path.GetFileNameWithoutExtension(FilePath);
            throw new Exception();
        }
    }

    /// <summary> 获取文件的扩展名 </summary>
    public string FileExtension => Path.GetExtension(FilePath).ToLower();

    /// <summary> 漫画文件名（全名，带扩展名，不包含文件夹名） </summary>
    public string FileFullName => Path.GetFileName(FilePath);

    /// <summary>
    /// 获取漫画文件大小。单位：字节
    /// </summary>
    [ObservableProperty]
    long fileSize;

    /// <summary> 漫画文件所在文件夹路径 </summary>
    public string FolderPath => Path.GetDirectoryName(FilePath);

    /// <summary> 本子名字。第一个括号外的内容（括号外内容可能有多个,也可能所有内容都在括号内） </summary>
    public string MangaName
    {
        get => string.Join(' ' , BracketBasedStringParser.Get_OutsideContent(FileDisplayName));
        //{
        //    var a = BracketBasedStringParser.Get_OutsideContent(FileDisplayName);
        //    return a.Count switch
        //    {
        //        0 => FileDisplayName,
        //        _ => 
        //    };
        //}
    }
    //BracketBasedStringParser.Get_OutsideContent_Recursion(FileDisplayName);

    /// <summary> 文件名中包含在括号的本子Tag </summary>
    public string[] MangaTagsIncludedInFileName =>
        BracketBasedStringParser.Get_InsideContent(FileDisplayName).ToArray();

    //TODO 翻译漫画名的功能
}
