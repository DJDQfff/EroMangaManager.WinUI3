using EroMangaManager.Core.MangaNameParse;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls;

public sealed partial class RenameMangaBookByString : UserControl
{
    private MangaBook book;

    /// <summary>
    ///
    /// </summary>
    public MangaBook MangaBook
    {
        set
        {
            book = value;
            textbox.Text = value.FileDisplayName;

            //指定默认新名字，按移除重复标签方法对待
            textbox.Text = MangaNameParser.RemoveRepeatTag(value.FileDisplayName);

            Bindings.Update();
        }
        get => book;
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
        var bool1 = string.IsNullOrWhiteSpace(NewDisplayName);
        var invalidChars = Path.GetInvalidFileNameChars();
        var bool2 = NewDisplayName.Any(c => invalidChars.Contains(c));
        if (bool1)
        {
            // 检查文件名是非为空
            hinttextblock.Text = ResourceLoader.GetForViewIndependentUse("Strings").GetString("DontUseEmptyString");
            IsNewnameOK = false;
            WrongInput?.Invoke();
        }
        else if (bool2)
        {
            // 检查文件是否含有非法字符
            hinttextblock.Text = ResourceLoader.GetForViewIndependentUse("Strings").GetString("ContainInvalaidChar");
            IsNewnameOK = false;

            WrongInput?.Invoke();
        }
        else
        {
            hinttextblock.Text = string.Empty;
            IsNewnameOK = true;

            CorrectInput?.Invoke();
        }
    }
}