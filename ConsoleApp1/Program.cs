// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine(Brackets.BracketsArray.Length);
var testfolder = @"D:\test";

var testfiles = Directory.GetFiles(testfolder , "*.zip");

foreach (var testfile in testfiles)
{
    var filename = Path.GetFileName(testfile);
    var manganame = SplitAndParser(filename);

    Console.WriteLine(manganame.Item1);
}