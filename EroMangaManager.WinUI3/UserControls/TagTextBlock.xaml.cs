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

using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls;
public sealed partial class TagTextBlock : UserControl
{
    public string Text
    {
        set
        {
            textblock.Text = value;
        }
    }
    public TagTextBlock ()
    {
        InitializeComponent();
    }

    private void MenuFlyoutItem_Click (object sender , RoutedEventArgs e)
    {
        DataPackage dataPackage = new();
        dataPackage.SetText(textblock.Text);
        dataPackage.RequestedOperation = DataPackageOperation.Copy;

        Clipboard.SetContent(dataPackage);
    }

    private void MenuFlyoutItem_Click_1 (object sender , RoutedEventArgs e)
    {
        MainPage.Current.MainFrame.Navigate(typeof(SearchMangaPage) , new string[] { textblock.Text });

    }
}
