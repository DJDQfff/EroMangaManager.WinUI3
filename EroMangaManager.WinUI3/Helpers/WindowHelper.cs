namespace EroMangaManager.WinUI3.Helpers
{
    internal static class WindowHelper
    {
        internal static void ShowReadWindow (MangaBook mangaBook)
        {
            var newWindow = new MainWindow();
            newWindow.MainWindowFrame.Navigate(typeof(ReadPage) , mangaBook);
            newWindow.Title = mangaBook.FileDisplayName;

            newWindow.Activate();
        }
    }
}