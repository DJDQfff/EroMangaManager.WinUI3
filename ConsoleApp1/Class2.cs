namespace ConsoleApp1;

internal class Class2
{
    public static void Run()
    {
        foreach (var file in File_ersheng)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(file);

            Console.ForegroundColor = ConsoleColor.Blue;
            var a = Get_InsideContent(file);
            a.ForEach(x => Console.WriteLine(x));

            Console.ForegroundColor = ConsoleColor.Yellow;
            var out1 = Get_OutsideContent(file);
            out1.ForEach(x => Console.WriteLine(x));
            Console.WriteLine();
        }
    }
}
