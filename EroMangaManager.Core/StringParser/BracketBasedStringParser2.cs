using System.Text.RegularExpressions;

namespace EroMangaManager.Core.StringParser;
/// <summary>
/// 设计出来的通过算法实现括号内外字符串，都是有缺陷的，都丢在这里，不对外使用
/// </summary>
internal static class BracketBasedStringParser2
{

    /// <summary>
    /// 获取本子名和tag，同时进行，有缺陷
    /// </summary>
    /// <param name="_FileDisplayName">传入漫画文件名（不带后缀）</param>
    /// <returns>第一个是MangaName，后面的是tag</returns>
    [Obsolete("有缺陷，有时候会出问题，且内部逻辑杂乱")]
    public static (string, List<string>) Get_BothContetn (string _FileDisplayName)
    {
        bool findmanganame = false;
        string manganame = default;
        List<string> tagslist = SplitByBrackets(_FileDisplayName);
        //var startwithleftbrackets = LeftBrackets.Contains(_FileDisplayName[0]);
        //var endwithrightbrackets = RightBrackets.Contains(_FileDisplayName[ _FileDisplayName.Length - 1]);
        foreach (var tag in tagslist)                    // 查找漫画名Tag，只能找出正常模式下的本子名
        {
            var tagstart = _FileDisplayName.IndexOf(tag);
            var tagend = tagstart + tag.Length - 1;

            var left1 = _FileDisplayName.LastIndexOfAny([.. RightBrackets] , tagstart);
            var left2 = _FileDisplayName.LastIndexOfAny([.. LeftBrackets] , tagstart);
            var right1 = _FileDisplayName.IndexOfAny([.. LeftBrackets] , tagend);
            var right2 = _FileDisplayName.IndexOfAny([.. RightBrackets] , tagend);
            if
            (
               (left1 != -1 && left2 != -1 && left1 > left2) ||
               (left1 == -1 && left2 == -1 && right1 < right2) ||
               (right1 == -1 && right2 == -1 && right1 < right2)
            )
            {
                tagslist.Remove(tag);              // 如果存在，则调整位置，把MangaName移到tag集合头部
                manganame = tag;
                findmanganame = true;
                break;
            }
        }

        if (!findmanganame)
        {
            if (_FileDisplayName.Count(x => LeftRightBrackets.Contains(x)) == 0)
            {
                // 无括号，则这个文件名就是本子名，且无tag
                manganame = _FileDisplayName;
                tagslist.Clear();
            }
            if (CorrectBracketPairConut(_FileDisplayName) != -1)
            {
                //  所有tag都被括号包起来了，本子名应该未包含在括号里面，这样无法识别
                Debug.WriteLine($"无法解析出MangaName：\n{_FileDisplayName}");
            }
            else
            {
                //其他情况下的bug，应该没有了
                Debug.WriteLine($"无法解析出MangaName：\n{_FileDisplayName}");
            }
        }

        return (manganame, tagslist);
    }

    /// <summary>
    /// 递归获取括号tag，未完成
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Obsolete("功能未完成" , true)]
    public static IEnumerable<string> Get_InsideContent_Recursion (string input)
    {
        return null;
    }
    // TODO 嵌套相同括号的话无法读取
    /// <summary>
    /// 以递归的方式获取未包含在括号内的字符串
    /// </summary>
    /// <remarks>
    /// 1.嵌套相同括号的话无法读取
    /// 2.只能获取第一个未包含在括号中的内容（写得时候没考虑过OutsideContent可能分成几个片段分散开来放）
    /// 基于此，使用推荐的那个
    /// </remarks>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string Get_OutsideContent_Recursion (string name)
    {
        if (CorrectBracketPairConut(name) == -1)
        {
            return name;
        }
        var _name = name.Trim();

        var leftbracketindex = _name.IndexOfAny([.. LeftBrackets]);

        switch (leftbracketindex)
        {
            case 0:
                var leftbracketchar = _name[leftbracketindex];

                var index = LeftBrackets.IndexOf(leftbracketchar);

                var rightbracket = RightBrackets[index];
                var rightindex = _name.IndexOf(rightbracket);

                var subname = _name.Substring(rightindex + 1);
                return Get_OutsideContent_Recursion(subname);

            case > 0:
                return _name.Substring(0 , leftbracketindex).Trim();
        }

        return name.Trim();
    }

    /// <summary>
    /// 使用正则表达式获取非括号内内容，有问题
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [Obsolete("功能未完成" , true)]
    public static string Get_OutsideContent_Regex (string name)
    {
        var pattern = @"([\]】）\)\}]|)[^\[【（\(\{\]】）\)\}]+([\[【（\(\{]|)";
        var pattern2 = @"([\]】）\)\}]|)\S+([\[【（\(\{]|)";
        var regex = new Regex(pattern);
        var regex2 = new Regex(pattern2);
        var a = regex.Matches(name);
        List<string> strings = [];
        foreach (Match t in a)
        {
            var str = t.Value;
            strings.Add(str);
        }
        strings.Sort((x , y) => -x.Length + y.Length);
        return strings.First();
    }

    /// <summary>
    /// 通过堆栈获取括号外字符串，未完成
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [Obsolete("功能未完成" , true)]
    public static string Get_OutsideContent_Stack (string name)
    {
        //Stack<char> lefts = new();
        //List<string> pieces = [];
        //StringBuilder stringBuilder = new();
        //int start;
        //for (int index = 0 ; index < name.Length ; index++)
        //{
        //    var c = name[index];
        //    if (LeftBrackets.Contains(c))
        //    {
        //        lefts.Push(c);
        //        start = index;
        //    }

        //    if (RightBrackets.Contains(c))
        //    {
        //        if (c == lefts.Peek())
        //        {
        //        }
        //    }
        //}
        return null;
    }
}
