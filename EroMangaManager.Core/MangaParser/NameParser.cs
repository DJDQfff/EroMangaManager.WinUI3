using System.Text;
using System.Text.RegularExpressions;

namespace EroMangaManager.Core.MangaParser
{
    /// <summary>
    /// 对基于tag的本子名的相关操作的帮助整合类
    /// </summary>
    public static class NameParser
    {
        /// <summary>
        /// 左括号
        /// </summary>
        public const string LeftBrackets = "[【（({";

        /// <summary>
        /// 右括号
        /// </summary>
        public const string RightBrackets = "]】）)}";

        private static int Length = LeftBrackets.Length;

        /// <summary>
        /// 所有括号
        /// </summary>
        public const string LeftRightBrackets = LeftBrackets + RightBrackets;
        /// <summary>
        /// 先获取本子名和tag，在分开
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static (string, List<string>) GetMangaNameAndTags2 (string input)

        {
            var manganame = GetMangaName_Recursion(input);
            var tags = SplitByBrackets(input);
            tags.Remove(manganame);
            return (manganame, tags);
        }
        /// <summary>
        /// 获取本子名和tag，同时进行
        /// </summary>
        /// <param name="_FileDisplayName">传入漫画文件名（不带后缀）</param>
        /// <returns>第一个是MangaName，后面的是tag</returns>
        public static (string, List<string>) GetMangaNameAndTags (string _FileDisplayName)
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
                if (CanbePair(_FileDisplayName))
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
        /// 按括号分解
        /// </summary>
        /// <param name="_FileDisplayName"></param>
        /// <returns></returns>
        private static List<string> SplitByBrackets (string _FileDisplayName)
        {
            var tagslist =
                _FileDisplayName.Trim()
                .Split(LeftRightBrackets.ToCharArray())      // 按括号分解为tag
                .Where(x => !string.IsNullOrWhiteSpace(x))// 移除所有为空白的tag
                .Select(x => x.Trim())                  // 所有移除首尾空白
                .ToList();
            return tagslist;
        }
        /// <summary>
        /// 使用正则表达式获取本子名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetMangaName_Regex (string name)
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
        /// 通过堆栈获取本子名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetMangaName_Stack (string name)
        {
            Stack<char> lefts = new();
            List<string> tags = [];
            StringBuilder stringBuilder = new();
            int start;
            for (int index = 0 ; index < name.Length ; index++)
            {
                var c = name[index];
                if (LeftBrackets.Contains(c))
                {
                    lefts.Push(c);
                    start = index;
                }

                if (RightBrackets.Contains(c))
                {
                    if (c == lefts.Peek())
                    {


                    }
                }
            }
            return null;
        }
        // TODO 嵌套相同括号的话无法读取
        /// <summary>
        /// 以递归的方式获取本子名
        /// 1.嵌套相同括号的话无法读取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetMangaName_Recursion (string name)
        {
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
                    return GetMangaName_Recursion(subname);

                case > 0:
                    return _name.Substring(0 , leftbracketindex).Trim();
            }

            return name.Trim();
        }

        /// <summary>
        ///移除其中包含的重复tag
        /// </summary>
        /// <param name="oldname">旧的名字</param>
        public static string RemoveRepeatTag (string oldname)
        {
            // TODO 输入一个包含重复tag的名称，算出一个去掉重复tag的名称
            return oldname;
        }

        /// <summary>
        /// 是否左右括号成对
        /// </summary>
        /// <param name="tagstring"></param>
        /// <returns></returns>
        public static bool CanbePair (string tagstring)
        {
            for (int i = 0 ; i < Length ; i++)
            {
                int count1 = tagstring.Count(n => n == LeftBrackets[i]);
                int count2 = tagstring.Count(n => n == RightBrackets[i]);
                if (count1 != count2)
                {
                    return false;
                }
            }
            return true;
        }
    }
}