using EroMangaManager.Core.MangaParser;

var foldertest = @"D:\test";
var folderersheng = @"D:\Downloads\本子二审";
var filesersheng = Directory.GetFiles(folderersheng , "*.zip")
    .Select(x => Path.GetFileNameWithoutExtension(x));
string[] fileserror = [
    @"[sin-maniax (轟真)] (アズールレーン) [中国翻訳] [DL版]",
@"[鹤崎贵大&苍津ウミヒト&川上なち助&しののめしの&ダコワズ&雪月佳&结城心一&毒でんぱ]异世界跟奴隶做色色事情漫画短篇集异世界 で奴隷とエロいことしちゃうアンソロジーコミック",
    @"[ZOAL (LEN[A-7])](コミティア125)ヌーディストビーチ にて",//也是嵌套相同括号的问题
    @"[[date]][天鹅之恋]deep stalker-ソノ皮デ美少女ニナル",// 嵌套相同括号会出错
    @"（C97)僕の記憶が教え子に偽装されてしまいました",//前面两个括号一个中文一个英文
@" (C93) 美羽ちゃんとベランダXX",// 已解决
    @"[140kmh (赤城あさひと)](C95) [不良ちゃんとコタツでヌクヌクする大晦日。 [中国翻訳].zip",
// 不良前面多了个括号，不对称
];
//var translator = new SimpleTranslator("20210219000701366" , "VkerV4o1qG1TK6mUlbr_" , ServerHost.Https);

foreach (var testfile in filesersheng)
{
    var ispair = CorrectBracketPairConut(testfile);
    if (ispair != -1)
    {
        Console.WriteLine(testfile);
        var suggestedname = NameParser.RemoveRepeatTag(testfile);
        Console.WriteLine(suggestedname);

        Console.WriteLine();
    }


}