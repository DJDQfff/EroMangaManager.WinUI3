using System.Collections.Generic;

using EroMangaManager.Core.MangaParser;
using EroMangaManager.Core.Models;

using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1;
/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    string path = @"(C73)[T2 ART WORKS (Tony)] ¤­¤Æ¤ë¤è! ÖñÄÚ¤¯¤ó¤Ã (¥Ð¥ó¥Ö©`¥Ö¥ì©`¥É) [ÎÞÐÞÕý]";
    public MainWindow ()
    {
        this.InitializeComponent();

        var pieces = new MangaBook(path);
        control3.Sources = NameParser.SplitByBlank(path);
    }


    private IEnumerable<string> a_SplitFunc (string arg)
    {
        return arg.Split(' ');

    }

}
