using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using Windows.Foundation;
using Windows.Foundation.Collections;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EroMangaManager.WinUI3.Views;

/// <summary>
/// 用于查看folder类型的本子
/// </summary>
public sealed partial class ReadPage2 : Page
{
    private ObservableCollection<string> _list = [];
    private string folder;

    public ReadPage2 ()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo (NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        if (e.Parameter is MangaBook manga)
        {
            if (folder != manga.FilePath)
            {
                folder = manga.FilePath;
                var list = Directory.GetFiles(folder);
                _list.Clear();
                _list.AddRange(list);
            }
        }
    }

    private void listview_SelectionChanged (object sender , SelectionChangedEventArgs e)
    {
        return;
        //var a = e.AddedItems[0] as string;
        //listview.SelectedItem = a;
        //flipview.SelectedItem = a;
    }
}
