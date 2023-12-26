
// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“内容对话框”项模板

namespace EroMangaManager.WinUI3.Views.ContentDialogPages
{
    /// <summary>
    ///
    /// </summary>
    public sealed partial class OverviewInformation : ContentDialog
    {
        private MangaBook MangaBook { get; }

        /// <summary>
        ///
        /// </summary>
        public OverviewInformation (MangaBook manga)
        {
            InitializeComponent();
            MangaBook = manga;
        }

    }
}