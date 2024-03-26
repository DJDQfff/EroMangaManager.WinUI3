

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System;
using System.Collections.Generic;

namespace App1;
[INotifyPropertyChanged]
public sealed partial class UserControl2 : UserControl
{
    public event Func<string , IEnumerable<string>> SplitFunc;

    [ObservableProperty]
    string newName;
    [ObservableProperty]
    string source;
    partial void OnSourceChanged (string value)
    {

        var s = SplitFunc.Invoke(value);
        if (s is null)
            return;
        ListView1.Items.Clear();
        ListView2.Items.Clear();
        foreach (var ss in s)
        {
            ListView1.Items.Add(ss);
        }
    }
    public UserControl2 ()
    {
        this.InitializeComponent();
    }


    private void AddButton_Click (object sender , Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            var tag = button.DataContext as string;
            var container = ListView2.ContainerFromItem(tag);
            var index = ListView2.IndexFromContainer(container);

            ListView2.Items.RemoveAt(index);
            ListView1.Items.Add(tag);
        }
        SetNewName();
    }

    private void RemoveButton_Click (object sender , Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            var tag = button.DataContext as string;
            var container = ListView1.ContainerFromItem(tag);
            var index = ListView1.IndexFromContainer(container);

            ListView1.Items.RemoveAt(index);
            ListView2.Items.Add(tag);
        }
        SetNewName();
    }

    void SetNewName ()
    {
        var list = new List<string>();
        foreach (var tag in ListView1.Items)
        {
            list.Add(tag as string);
        }
        NewName = string.Join(' ' , list);
    }
}
