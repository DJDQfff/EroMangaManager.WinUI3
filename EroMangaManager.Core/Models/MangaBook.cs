namespace EroMangaManager.Core.Models;

/// <summary> 本子 </summary>
public partial class MangaBook : ObservableObject
{
    /// <summary> 实例化EroManga </summary>
    public MangaBook (string filepath)
    {
        //FileSize = (new FileInfo(filepath)).Length;
        FilePath = filepath;
    }

    /// <summary>
    /// 封面文件路径
    /// </summary>
    /// <remarks>这个一定要有，不能为null，不然在Image控件加载图像时会异常导致程序闪退</remarks>
    [ObservableProperty]
    private string coverPath;

    /// <summary>
    /// 获取漫画文件大小。单位：字节
    /// </summary>
    public long FileSize { get; set; }

    private string filepath;

    /// <summary> 漫画文件路径 </summary>

    public string FilePath
    {
        get => filepath;
        set
        {
            Debug.WriteLine(value);
            SetProperty(ref filepath , value);
            //var tags = NameParser.GetNameAndTags2(FileDisplayName);
            //MangaTagsIncludedInFileName = [.. tags.Item2];
            //MangaName = tags.Item1;

            var pieces = NameParser.SplitByBlank(FileDisplayName);
            var name = pieces.FirstOrDefault(piece => !piece.IsIncludedInBracketPair());
            if (name == null)
            {
                name = GetName_Recursion(FileDisplayName);
                name ??= NameParser.GetNameAndTags2(FileDisplayName).Item1;
            }
            MangaName = name;

            MangaTagsIncludedInFileName = pieces.Where(piece => piece.IsIncludedInBracketPair()).Select(x => TrimBracket(x)).ToArray();

            OnPropertyChanged("");


        }
    }

    /// <summary> 获取文件的扩展名 </summary>
    public string FileExtension => Path.GetExtension(FilePath).ToLower();

    /// <summary> 文件Display名（不带扩展名） </summary>
    public string FileDisplayName => Path.GetFileNameWithoutExtension(FilePath);

    /// <summary> 漫画文件所在文件夹路径 </summary>
    public string FolderPath => Path.GetDirectoryName(FilePath);

    /// <summary> 漫画文件名（全名，带扩展名，不包含文件夹名） </summary>
    public string FileFullName => Path.GetFileName(FilePath);

    /// <summary> 本子名字 </summary>
    [ObservableProperty]
    private string mangaName;

    /// <summary> 包含在文件名中的本子Tag </summary>
    [ObservableProperty]
    private string[] mangaTagsIncludedInFileName;

    /// <summary> 漫画翻译后的名称 </summary>
    [ObservableProperty]
    private string translatedMangaName;

    //TODO 翻译漫画名的功能
}