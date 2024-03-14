var testfolder = @"D:\test";
var testfolder2 = @"D:\Downloads\本子二审\本子";
var testfiles = Directory.GetFiles(testfolder2 , "*.zip").Select(x => Path.GetFileNameWithoutExtension(x));
string[] testfiles2 = [
    @"[ZOAL (LEN[A-7])](コミティア125)ヌーディストビーチ にて",//也是嵌套相同括号的问题
    @"[[date]][天鹅之恋]deep stalker-ソノ皮デ美少女ニナル",// 嵌套相同括号会出错
    @"（C97)僕の記憶が教え子に偽装されてしまいました",//前面两个括号一个中文一个英文
@" (C93) 美羽ちゃんとベランダXX",// 已解决
    @"D:\\Downloads\\本子二审\\本子\\[140kmh (赤城あさひと)](C95) [不良ちゃんとコタツでヌクヌクする大晦日。 [中国翻訳].zip",
// 不良前面多了个括号，不对称
];

foreach (var testfile in testfiles)
{

    var ispair = CanbePair(testfile);
    if (!ispair)
    {
        //Console.WriteLine(testfile);
        continue;
    }
    var parse1 = GetMangaNameAndTags(testfile);
    var parse2 = GetMangaNameAndTags2(testfile);
    //var name3 = GetMangaName_Regex(testfile);
    if (parse1.Item2 != parse2.Item2)
    {
        parse1.Item2.ForEach(x => Console.Write(x + "\t"));
        Console.WriteLine();
        parse2.Item2.ForEach(x => Console.Write(x + "\t"));
        Console.WriteLine();
        continue;

    }


    var name1 = parse1.Item1;
    var name2 = parse2.Item1;

    if (name1 != name2 /*|| name2 != name3 || name1 != name3*/)
    {
        Console.WriteLine(testfile);

        Console.WriteLine("\t" + name1);
        Console.WriteLine("\t" + name2);
        //Console.WriteLine("\t" + name3);

        Console.WriteLine();
        _ = GetMangaName_Regex(testfile);
    }
}