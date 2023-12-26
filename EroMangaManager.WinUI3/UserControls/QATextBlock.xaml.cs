//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

using EroMangaManager.WinUI3.LocalizationWords;

namespace EroMangaManager.WinUI3.UserControls
{
    public sealed partial class QATextBlock : UserControl
    {
        private QuestionAndAnswers question;
        private QuestionAndAnswers answer;

        public QuestionAndAnswers Question
        {
            set
            {
                question = value;
                QuestionTextBlock.Text = ResourceLoader.GetForViewIndependentUse("QuestionAndAnswers").GetString(Enum.GetName(typeof(QuestionAndAnswers) , question));
            }
        }

        public QuestionAndAnswers Answer
        {
            set
            {
                answer = value;
                AnswerTextBlock.Text = ResourceLoader.GetForViewIndependentUse("QuestionAndAnswers").GetString(Enum.GetName(typeof(QuestionAndAnswers) , answer));
            }
        }

        public QATextBlock ()
        {
            InitializeComponent();
        }

        public QATextBlock (string question , string answer)
        {
            QuestionTextBlock.Text = question;
            AnswerTextBlock.Text = answer;
        }
    }
}