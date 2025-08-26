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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.Views.ContentDialogPages;

public sealed partial class TagCategorySelect : ContentDialog
{
    public TagCategorySelect()
    {
        InitializeComponent();
    }

    public string CategoryName => combobox.SelectedItem as string;

    private void combobox_Loaded(object sender, RoutedEventArgs e)
    {
        var category = DatabaseController.TagCategory_Query();
        if (category != null)
        { combobox.ItemsSource = category; }
    }
}