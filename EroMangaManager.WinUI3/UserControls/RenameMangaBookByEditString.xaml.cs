using System.Collections.Specialized;
using System.ComponentModel;

using Microsoft.EntityFrameworkCore.Query.Internal;

namespace EroMangaManager.WinUI3.UserControls;

public sealed partial class RenameMangaByEditString : UserControl, INotifyPropertyChanged
{
    private Manga mangaBook;

    public Manga Manga
    {
        get => mangaBook;
        set
        {
            mangaBook = value;
            textbox.Text = value.FileDisplayName;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Manga)));
        }
    }

    private bool isnewnameok;

    /// <summary>
    /// ������������Ƿ����
    /// ����������1.�����ֲ����Ƿ��ַ���2.������������Ʋ���ͬ��
    /// </summary>
    public bool IsNewnameOK
    {
        set { isnewnameok = value; }
        get { return isnewnameok && (NewDisplayName != Manga.FileDisplayName); }
    }

    /// <summary>
    /// ���벻�Ϸ�
    /// </summary>
    public event Action WrongInput;

    /// <summary>
    /// ����Ϸ�
    /// </summary>
    public event Action CorrectInput;

    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// ѡ���������
    /// </summary>
    public string NewDisplayName => textbox.Text;

    /// <summary>
    ///
    /// </summary>
    public RenameMangaByEditString()
    {
        InitializeComponent();
    }

    /// <summary>
    /// ��ָ�������֣�Ĭ��ȥ���ظ���ǩ
    /// </summary>
    /// <param name="mangaBook"></param>
    public RenameMangaByEditString(Manga mangaBook)
    {
        InitializeComponent();
        Manga = mangaBook;
        //CorrectInput += () => RenameButton.IsEnabled = true;
        //WrongInput += () => RenameButton.IsEnabled = false;
    }

    /// <summary>
    /// ָ��������
    /// </summary>
    /// <param name="mangaBook"></param>
    /// <param name="suggestedname"></param>
    public RenameMangaByEditString(Manga mangaBook, string suggestedname)
        : base()
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

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NewDisplayName))
        {
            // ����ļ����Ƿ�Ϊ��

            hinttextblock.Text = ResourceLoader
                .GetForViewIndependentUse()
                .GetString("DontUseEmptyString");
            IsNewnameOK = false;
            RenameButton.IsEnabled = false;
            WrongInput?.Invoke();
        }
        else if (NewDisplayName.Any(c => Path.GetInvalidFileNameChars().Contains(c)))
        {
            // ����ļ��Ƿ��зǷ��ַ�

            hinttextblock.Text = ResourceLoader
                .GetForViewIndependentUse()
                .GetString("ContainInvalaidChar");
            IsNewnameOK = false;
            RenameButton.IsEnabled = false;
            WrongInput?.Invoke();
        }
        else
        {
            // ���϶�û���⣬�Ǿ���Ϊ��ȷ
            hinttextblock.Text = string.Empty;
            IsNewnameOK = true;
            RenameButton.IsEnabled = true;
            CorrectInput?.Invoke();
        }
    }

    private async void RenameButton_Click(object sender, RoutedEventArgs e)
    {
        var newname = NewDisplayName;// ����NewDIsplayName�������첽���ʣ������ui�̱߳�����ǰȡ����
        try
        {
            string newpath = await Task.Run(() => MangaFileOperation.MoveManga(Manga, null, newname));
            Manga.FilePath = newpath;
        }
        catch (UnauthorizedAccessException) { App.Current.GlobalViewModel.AccessDenied(); }
    }
}