namespace ConsoleApp1;

internal class Test1
{
    public static void Run (string[] args)
    {
        //var translator = new SimpleTranslator("20210219000701366" , "VkerV4o1qG1TK6mUlbr_" , ServerHost.Https);

        foreach (var testfile in File_ersheng)
        {
            var ispair = CorrectBracketPairConut(testfile);
            if (ispair != -1)
            {
                Console.WriteLine(testfile);
                var suggestedname = RemoveRepeatTag(testfile);
                Console.WriteLine(suggestedname);

                Console.WriteLine();
            }
        }
    }
}