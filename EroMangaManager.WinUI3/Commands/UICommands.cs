namespace EroMangaManager.WinUI3.Commands;
internal class UICommands
{
    public static UICommands Instance { get; set; }
    public StandardUICommand OverviewInformation = new();

    public static void Initial ()
    {
        Instance ??= new();

        Instance.OverviewInformation.ExecuteRequested += async (sender , args) =>
        {
            switch (args.Parameter)
            {
                case MangaBook manga:
                    var dialog = new OverviewInformation(manga)
                    {
                        XamlRoot = App.Current.MainWindow.Content.XamlRoot
                    };
                    _ = await dialog.ShowAsync();
                    break;
            }

        };
        Instance.OverviewInformation.IconSource = new SymbolIconSource()
        {
            Symbol = Symbol.View
        };

    }


}
