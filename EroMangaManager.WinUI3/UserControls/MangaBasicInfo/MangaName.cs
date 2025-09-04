using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.WindowsRuntime;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EroMangaManager.WinUI3.UserControls.MangaBasicInfo;

public sealed partial class MangaName : Control
{
    public static readonly DependencyProperty MangaProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(MangaName), new PropertyMetadata(null));
    public string Text { get => GetValue(MangaProperty) as string; set => SetValue(MangaProperty, value); }

    public MangaName()
    {
        DefaultStyleKey = typeof(MangaName);
    }
}