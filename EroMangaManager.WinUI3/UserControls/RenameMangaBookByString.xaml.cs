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

            //ָ��Ĭ�������֣����Ƴ��ظ���ǩ�����Դ�
            textbox.Text = MangaNameParser.RemoveRepeatTag(value.FileDisplayName);

            Bindings.Update();
        }
        get => book;
    }

    private bool isnewnameok;

    /// <summary>
    /// ������������Ƿ����
    /// ����������1.�����ֲ����Ƿ��ַ���2.������������Ʋ���ͬ��
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
    /// ���벻�Ϸ�
    /// </summary>
    public event Action WrongInput;

    /// <summary>
    /// ����Ϸ�
    /// </summary>
    public event Action CorrectInput;

    /// <summary>
    /// ѡ���������
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
    /// ��ָ�������֣�Ĭ��ȥ���ظ���ǩ
    /// </summary>
    /// <param name="mangaBook"></param>
    public RenameMangaBookByString (MangaBook mangaBook)
    {
        InitializeComponent();
        MangaBook = mangaBook;
    }

    /// <summary>
    /// ָ��������
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
            // ����ļ����Ƿ�Ϊ��
            hinttextblock.Text = ResourceLoader.GetForViewIndependentUse("Strings").GetString("DontUseEmptyString");
            IsNewnameOK = false;
            WrongInput?.Invoke();
        }
        else if (bool2)
        {
            // ����ļ��Ƿ��зǷ��ַ�
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