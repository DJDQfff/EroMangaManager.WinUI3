var testfolder = @"D:\test";
var testfolder2 = @"D:\Downloads\本子二审\本子";
var testfiles = Directory.GetFiles(testfolder2 , "*.zip");

string[] testfiles2 = [
    @"[ZOAL (LEN[A-7])](コミティア125)ヌーディストビーチ にて",//
    @"[[date]][天鹅之恋]deep stalker-ソノ皮デ美少女ニナル",// 嵌套相同括号会出错
    @"（C97)僕の記憶が教え子に偽装されてしまいました",//前面两个括号一个中文一个英文
@" (C93) 美羽ちゃんとベランダXX",// 已解决
    @"D:\\Downloads\\本子二审\\本子\\[140kmh (赤城あさひと)](C95) [不良ちゃんとコタツでヌクヌクする大晦日。 [中国翻訳].zip",
// 不良前面多了个括号，不对称
];

foreach (var testfile in testfiles)
{
    var filename = Path.GetFileNameWithoutExtension(testfile);

    var ispair = CanbePair(filename);
    if (!ispair)
    {
        Console.WriteLine(filename);
        continue;
    }
    var name1 = SplitAndParser(filename).Item1;
    var name2 = GetMangaName(filename);
    if (name1 != name2)
    {
        Console.WriteLine(filename);

        Console.WriteLine(name1);
        Console.WriteLine(name2);

        _ = GetMangaName(filename);
    }
}