var testfolder = @"D:\test";

var testfiles = Directory.GetFiles(testfolder , "*.zip");

foreach (var testfile in testfiles)
{
    var filename = Path.GetFileNameWithoutExtension(testfile);

    var name1 = SplitAndParser(filename).Item1;
    var name2 = GetMangaName(filename);
    if (name1 != name2)
    {
        Console.WriteLine(filename);

        Console.WriteLine(name1);
        Console.WriteLine(name2);
        if (Console.ReadKey().Key == ConsoleKey.Spacebar)
        {
            // 导出
        }
        Console.Clear();
    }
}