namespace ConsoleApp1;

internal class Configurations
{
    public static string[] File_error = [
        @"[sin-maniax (轟真)] (アズールレーン) [中国翻訳] [DL版]",
@"[鹤崎贵大&苍津ウミヒト&川上なち助&しののめしの&ダコワズ&雪月佳&结城心一&毒でんぱ]异世界跟奴隶做色色事情漫画短篇集异世界 で奴隷とエロいことしちゃうアンソロジーコミック",
    @"[ZOAL (LEN[A-7])](コミティア125)ヌーディストビーチ にて",//也是嵌套相同括号的问题
    @"[[date]][天鹅之恋]deep stalker-ソノ皮デ美少女ニナル",// 嵌套相同括号会出错
    @"（C97)僕の記憶が教え子に偽装されてしまいました",//前面两个括号一个中文一个英文
@" (C93) 美羽ちゃんとベランダXX",// 已解决
    @"[140kmh (赤城あさひと)](C95) [不良ちゃんとコタツでヌクヌクする大晦日。 [中国翻訳].zip",
// 不良前面多了个括号，不对称
];

    public static IEnumerable<string> File_ersheng = Directory.GetFiles(Folder_ersheng , "*.zip")
        .Select(x => Path.GetFileNameWithoutExtension(x));

    public static string Folder_ersheng = @"D:\本子\本子二审";
    public static string Folder_test = @"D:\test";
}