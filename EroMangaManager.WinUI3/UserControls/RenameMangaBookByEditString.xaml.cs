using System.Collections.Specialized;
using System.ComponentModel;

using Microsoft.EntityFrameworkCore.Query.Internal;

namespace EroMangaManager.WinUI3.UserControls;

public sealed partial class RenameMangaBookByEditString : UserControl, INotifyPropertyChanged
{
    private MangaBook mangaBook;
    public MangaBook MangaBook
    {
        get => mangaBook;
        set
        {
            mangaBook = value;
            textbox.Text = value.FileDisplayName;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MangaBook)));
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
        get { return isnewnameok && (NewDisplayName != MangaBook.FileDisplayName); }
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
    public RenameMangaBookByEditString()
    {
        InitializeComponent();
    }

    /// <summary>
    /// ��ָ�������֣�Ĭ��ȥ���ظ���ǩ
    /// </summary>
    /// <param name="mangaBook"></param>
    public RenameMangaBookByEditString(MangaBook mangaBook)
    {
        InitializeComponent();
        MangaBook = mangaBook;
        //CorrectInput += () => RenameButton.IsEnabled = true;
        //WrongInput += () => RenameButton.IsEnabled = false;
    }

    /// <summary>
    /// ָ��������
    /// </summary>
    /// <param name="mangaBook"></param>
    /// <param name="suggestedname"></param>
    public RenameMangaBookByEditString(MangaBook mangaBook, string suggestedname)
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

    private void RenameButton_Click(object sender, RoutedEventArgs e)
    {
        EroMangaManager.Core.IOOperation.MangaBookFileOperation.MoveManga(
            MangaBook,
            null,
            NewDisplayName
        );
    }
}
