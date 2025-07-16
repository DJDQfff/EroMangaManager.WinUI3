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

namespace EroMangaManager.WinUI3.UserControls;
public sealed partial class TagListOrder2 : UserControl
{
    /// <summary>
    /// 不能拖拽子项，遂放弃
    /// </summary>
    public TagListOrder2 ()
    {
        InitializeComponent();
    }

    public string NewName
    {
        get
        {
            var items = gridview.Items;
            List<string> strings = [];
            foreach (var item in items)
            {
                var container = gridview.ContainerFromItem(item);
                //var checkbox = container.
                //var text = container.Content as string;
                //strings.Add(text);
            }
            return string.Concat(strings);

        }
    }
    public IEnumerable<string> Sources
    {
        set
        {
            gridview.Items.Clear();
            foreach (var item in value)
            {
                gridview.Items.Add(item);
            }

        }
    }

}
