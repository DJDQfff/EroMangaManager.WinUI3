//https://go.microsoft.com/fwlink/?LinkId=234236 �Ͻ����ˡ��û��ؼ�����ģ��

namespace EroMangaManager.WinUI3.UserControls
{
    public sealed partial class QATextBlock : UserControl
    {
        private string answer;
        private string question;

        public QATextBlock ()
        {
            InitializeComponent();
        }

        public QATextBlock (string question , string answer)
        {
            QuestionTextBlock.Text = question;
            AnswerTextBlock.Text = answer;
        }

        public string Answer
        {
            set
            {
                answer = value;
                AnswerTextBlock.Text = ResourceLoader.GetForViewIndependentUse().GetString(answer);
            }
        }

        public string Question
        {
            set
            {
                question = value;
                QuestionTextBlock.Text = ResourceLoader.GetForViewIndependentUse().GetString(question);
            }
        }
    }
}