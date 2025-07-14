// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.ComponentModel;

namespace DJDQfff;

public sealed partial class TagListOrder : UserControl, INotifyPropertyChanged
{
    IEnumerable<string> sources;

    public IEnumerable<string> Sources
    {
        set
        {
            sources = value;
            ListView1.Items.Clear();
            ListView2.Items.Clear();
            foreach (var ss in value)
            {
                ListView1.Items.Add(ss);
            }
        }
        get => sources;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    string newName;

    public string NewName
    {
        get => newName;
        set
        {
            newName = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewName)));
        }
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
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

    private void RemoveButton_Click(object sender, RoutedEventArgs e)
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

    public void SetNewName()
    {
        var list = new List<string>();
        foreach (var tag in ListView1.Items)
        {
            list.Add(tag as string);
        }
        NewName = string.Join(' ', list);
    }

    public TagListOrder()
    {
        InitializeComponent();
    }
}
