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

namespace EroMangaManager.WinUI3.UserControls.MangaBasicInfo
{
    public sealed partial class MangaName : UserControl
    {
        public Manga Manga { get => DataContext as Manga; set => DataContext = value; }

        public MangaName()
        {
            InitializeComponent();
            DataContextChanged += (a, b) => this.Bindings.Update();
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            DataPackage dataPackage = new();
            dataPackage.SetText(manganameTextblock.Text);
            dataPackage.RequestedOperation = DataPackageOperation.Copy;

            Clipboard.SetContent(dataPackage);
        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            MainPage.Current.MainFrame.Navigate(typeof(SearchMangaPage), manganameTextblock.Text);
        }
    }
}