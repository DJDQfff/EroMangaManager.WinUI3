

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Windows.ApplicationModel.DataTransfer;

namespace TagListOrder;

[INotifyPropertyChanged]
public sealed partial class UserControl1 : UserControl
{
    string guid = Guid.NewGuid().ToString();
    [ObservableProperty]
    string sourceString;

    ObservableCollection<string> SavedTags = new();
    ObservableCollection<string> RemovedTags = new();
    int SelectedTagIndex = -1;
    public event Func<string , IEnumerable<string>> SplitFunc;
    partial void OnSourceStringChanged (string value)
    {

        var tags = SplitFunc?.Invoke(value);
        SavedTags.Clear();
        RemovedTags.Clear();
        foreach (var item in tags)
        {
            SavedTags.Add(item);
        }

    }
    public UserControl1 ()
    {
        this.InitializeComponent();
    }


    async Task<bool> PackageCanUse (DataPackageView view)
    {
        if (view.Contains(StandardDataFormats.Text))
        {
            var text = await view.GetTextAsync();
            var items = text.Split('|');
            if (items.Length != 2)
            {
                return false;
            }
            if (items[0] != guid)
            {
                return false;
            }

        }
        return true;
    }

    private async Task<string> getText (DataPackageView view)
    {
        var text = await view.GetTextAsync();
        var items = text.Split('|');
        return items[1];
    }
    private async void ItemsRepeater_DragOver (object sender , Microsoft.UI.Xaml.DragEventArgs e)
    {
        e.AcceptedOperation = DataPackageOperation.Move;

        if (!await PackageCanUse(e.DataView))
        {
            e.AcceptedOperation = DataPackageOperation.None;
        }
    }

    private async void ItemsRepeater_Drop (object sender , Microsoft.UI.Xaml.DragEventArgs e)
    {
        var a = await getText(e.DataView);

    }

    private void Border_DragStarting (Microsoft.UI.Xaml.UIElement sender , Microsoft.UI.Xaml.DragStartingEventArgs args)
    {

        if (sender is Border border)
        {
            var text = border.FindName("textblock") as TextBlock;
            args.Data.SetText($"{guid}|{text.Text}");
            args.Data.RequestedOperation = DataPackageOperation.Move;
        }

    }

}
