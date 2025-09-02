// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
using System.ComponentModel;

using Windows.Devices.Geolocation;

namespace EroMangaManager.WinUI3.UserControls;

public sealed partial class RenameMangaByDragTag : UserControl, INotifyPropertyChanged
{
    private Manga mangaBook;

    public Manga Manga
    {
        get => mangaBook;
        set
        {
            mangaBook = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Manga)));
            order.Sources = BracketBasedStringParser.SplitByBrackets_KeepBracket(
                Manga.FileDisplayName
            ); //value.Tags;
        }
    }

    public event Action<Manga> NameChanged;

    public event PropertyChangedEventHandler PropertyChanged;

    public RenameMangaByDragTag()
    {
        InitializeComponent();
    }

    private async void SingleMangaRename_New(object sender, RoutedEventArgs e)
    {
        var text = newnameBox.Text;// ����NewDIsplayName�������첽���ʣ����ܻ����ui�̱߳�����ǰȡ����

        try
        {
            string newpath = await Task.Run(() => MangaFileOperation.MoveManga(Manga, null, text));
            Manga.FilePath = newpath;
        }
        catch (UnauthorizedAccessException) { App.Current.GlobalViewModel.AccessDenied(); }// δ��Ȩ�쳣
        catch (System.IO.IOException) { App.Current.GlobalViewModel.AccessDenied(); }// ����δ֪�쳣

        NameChanged?.Invoke(Manga);
    }

    [Obsolete("ע���������bug������loaded������ʱ��Mangabook�������Ի�ûˢ��", true)]
    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        // ע���������bug������loaded������ʱ��mangabook�������Ի�ûˢ��
        //var a = BracketBasedStringParser.SplitByBrackets_Reserve(Manga.FileDisplayName);
        //textblock.Text = Manga.FileDisplayName;
        //order.Sources = a;
        //orde.ItemsSource = a;
    }
}