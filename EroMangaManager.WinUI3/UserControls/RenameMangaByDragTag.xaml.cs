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
        var text = newnameBox.Text;// 由于NewDIsplayName在下面异步访问，可能会出现ui线程报错，提前取出来

        try
        {
            string newpath = await Task.Run(() => MangaFileOperation.MoveManga(Manga, null, text));
            Manga.FilePath = newpath;
        }
        catch (UnauthorizedAccessException) { App.Current.GlobalViewModel.AccessDenied(); }// 未授权异常
        catch (System.IO.IOException) { App.Current.GlobalViewModel.AccessDenied(); }// 其他未知异常

        NameChanged?.Invoke(Manga);
    }

    [Obsolete("注意这里出过bug，调用loaded函数的时候，Mangabook可能属性还没刷新", true)]
    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        // 注意这里出过bug，调用loaded函数的时候，mangabook可能属性还没刷新
        //var a = BracketBasedStringParser.SplitByBrackets_Reserve(Manga.FileDisplayName);
        //textblock.Text = Manga.FileDisplayName;
        //order.Sources = a;
        //orde.ItemsSource = a;
    }
}