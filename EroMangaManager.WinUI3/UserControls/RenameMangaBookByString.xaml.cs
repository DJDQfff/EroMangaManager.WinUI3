namespace EroMangaManager.WinUI3.UserControls;

[INotifyPropertyChanged]
public sealed partial class RenameMangaBookByString : UserControl
{
    [ObservableProperty]
    private MangaBook mangaBook;
    partial void OnMangaBookChanged (MangaBook value)
    {
        textbox.Text = value.FileDisplayName;
    }

    private bool isnewnameok;

    /// <summary>
    /// 输入的新名字是否可用
    /// 两个条件：1.新名字不含非法字符。2.新名称与旧名称不相同。
    /// </summary>
    public bool IsNewnameOK
    {
        set
        {
            isnewnameok = value;
        }
        get
        {
            return isnewnameok && (NewDisplayName != MangaBook.FileDisplayName);
        }
    }

    /// <summary>
    /// 输入不合法
    /// </summary>
    public event Action WrongInput;

    /// <summary>
    /// 输入合法
    /// </summary>
    public event Action CorrectInput;

    /// <summary>
    /// 选择的新名字
    /// </summary>
    public string NewDisplayName => textbox.Text;

    /// <summary>
    ///
    /// </summary>
    public RenameMangaBookByString ()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 不指定新名字，默认去除重复标签
    /// </summary>
    /// <param name="mangaBook"></param>
    public RenameMangaBookByString (MangaBook mangaBook)
    {
        InitializeComponent();
        MangaBook = mangaBook;
        //CorrectInput += () => RenameButton.IsEnabled = true;
        //WrongInput += () => RenameButton.IsEnabled = false;

    }

    /// <summary>
    /// 指定新名字
    /// </summary>
    /// <param name="mangaBook"></param>
    /// <param name="suggestedname"></param>
    public RenameMangaBookByString (MangaBook mangaBook , string suggestedname) : base()
    {
        if (suggestedname == null)
        {
            throw new NullReferenceException();
        }
        else
        {
            textbox.Text = suggestedname;
        }
    }

    private void TextBox_TextChanged (object sender , TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NewDisplayName))
        {
            // 检查文件名是非为空

            hinttextblock.Text = ResourceLoader.GetForViewIndependentUse("Strings").GetString("DontUseEmptyString");
            IsNewnameOK = false;
            RenameButton.IsEnabled = false;
            WrongInput?.Invoke();

        }
        else if (NewDisplayName.Any(c => Path.GetInvalidFileNameChars().Contains(c)))
        {
            // 检查文件是否含有非法字符

            hinttextblock.Text = ResourceLoader.GetForViewIndependentUse("Strings").GetString("ContainInvalaidChar");
            IsNewnameOK = false;
            RenameButton.IsEnabled = false;
            WrongInput?.Invoke();
        }
        else
        {
            // 以上都没问题，那就认为正确
            hinttextblock.Text = string.Empty;
            IsNewnameOK = true;
            RenameButton.IsEnabled = true;
            CorrectInput?.Invoke();

        }


    }

    private void RenameButton_Click (object sender , RoutedEventArgs e)
    {
        var text = NewDisplayName;

        try
        {
            // TODO 重命名可能存在bug，如重复名称
            string oldname = MangaBook.FilePath;
            string newname = Path.Combine(Path.GetDirectoryName(oldname) , text + ".zip");
            File.Move(oldname , newname);
        }
        catch { }
        MangaBook.FilePath = Path.Combine(MangaBook.FolderPath , text + ".zip");

    }
}