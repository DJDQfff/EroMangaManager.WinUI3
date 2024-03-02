
using MyLibrary.Standard20;
//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace EroMangaManager.WinUI3.UserControls
{
    /// <summary>
    ///
    /// </summary>
    public sealed partial class TagsDisplayControl : UserControl
    {
        private readonly ObservableCollection<string> tags = new();

        /// <summary>
        ///
        /// </summary>
        public string TagArray
        {
            set
            {
                var temp = NameParser.SplitAndParser(value);
                tags.Clear();
                tags.AddRange(temp.Item2);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public TagsDisplayControl ()
        {
            InitializeComponent();
        }
    }
}