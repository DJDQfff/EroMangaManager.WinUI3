﻿using System.Text;
using System.Text.RegularExpressions;

namespace EroMangaManager.Core.MangaParser
{
    /// <summary>
    /// 对基于tag的本子名的解析类
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

        private static readonly int Length = LeftBrackets.Length;

        /// <summary>
        /// 所有括号
        /// </summary>
        public const string LeftRightBrackets = LeftBrackets + RightBrackets;

        /// <summary>
        /// 递归获取括号tag，未完成
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Obsolete("功能未完成" , true)]
        public static IEnumerable<string> GetTags_Recursion (string input)
        {
            return null;
        }
        /// <summary>
        /// 获取本子名和tag，同时进行，有缺陷
        /// </summary>
        /// <param name="_FileDisplayName">传入漫画文件名（不带后缀）</param>
        /// <returns>第一个是MangaName，后面的是tag</returns>
        public static (string, List<string>) GetNameAndTags (string _FileDisplayName)
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
        /// 字符串中是否还有任意括号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ContainBracket (string str)
        {
            foreach (var b in LeftRightBrackets)
            {
                if (str.Contains(b))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 按左右括号分离，保留括号
        /// </summary>
        ///
        /// <param name="_FileDisplayName"></param>
        /// <returns></returns>
        public static List<string> SplitByBrackets_Reserve (string _FileDisplayName)
        {
            var stringBuilder = new StringBuilder();
            var pieces = new List<string>();
            var n = new StringBuilder(_FileDisplayName);
            for (int i = n.Length - 1 ; i > 0 ; i--)
            {
                if (LeftRightBrackets.Contains(n[i]))
                {
                    n.Insert(i + 1 , '/');

                    n.Insert(i , '/');
                }
            }
            // 这一步会把tag中的空格给消除，是一个小缺陷。以前使用空格有这个问题，现在换了个字符不知道有没有
            var _pieces = n.ToString().Split(['/']).Where(x => !string.IsNullOrWhiteSpace(x));

            foreach (var piece in _pieces)
            {
                stringBuilder.Append(piece);
                var result = stringBuilder.ToString();
                if (CorrectBracketPairConut(result) != -1)
                {
                    pieces.Add(result);
                    stringBuilder.Clear();
                }
                else
                {
                    continue;
                }
            }
            return pieces;
        }

        /// <summary>
        /// 按括号进行分解，移除括号
        /// </summary>
        /// <param name="_FileDisplayName"></param>
        /// <returns></returns>
        public static List<string> SplitByBrackets (string _FileDisplayName)
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
        /// 使用正则表达式获取本子名，有问题
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Obsolete("功能未完成" , true)]
        public static string GetName_Regex (string name)
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
        /// 是否包含在一对括号中（不要求首位括号是相对应的类型）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsIncludedInBracketPair (this string name)
        {
            if (name.Length < 2)
            {
                return false;
            }
            var start = name[0];
            var end = name[name.Length - 1];
            if (LeftRightBrackets.Contains(start) && LeftRightBrackets.Contains(end))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 移除首位括号
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string TrimBracket (this string name)
        {
            if (IsIncludedInBracketPair(name))
            {
                var n = name.Substring(1 , name.Length - 2);
                return TrimBracket(n);
            }
            return name.Trim();
        }
        /// <summary>
        /// 通过堆栈获取本子名，未完成
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Obsolete("功能未完成" , true)]
        public static string GetName_Stack (string name)
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
        // TODO 嵌套相同括号的话无法读取
        /// <summary>
        /// 以递归的方式获取本子名
        /// 1.嵌套相同括号的话无法读取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetName_Recursion (string name)
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
                    return GetName_Recursion(subname);

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
            var pieces = SplitByBrackets_Reserve(oldname);
            for (var index = 0 ; index < pieces.Count ; index++)
            {
                var piece = pieces[index];
                var behind = pieces.GetRange(index , pieces.Count);
                if (piece.IsIncludedInBracketPair())
                {
                    var piecewithoutbracket = piece.TrimBracket();
                    //foreach(var piece in _pieces.)
                }
            }



            return null;
        }


        /// <summary>
        /// 移除一个字符串集合的所有括号
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static List<string> RemoveBracketForEachString (this IEnumerable<string> a)
        {
            var list = new List<string>();
            foreach (var aa in a)
            {
                var aaa = SplitByBrackets(aa);
                aaa.ForEach(x => list.Add(x));
            }

            return list;
        }

        /// <summary>
        /// 左右括号成对，入成对，则返回多少对，否则返回-1
        /// </summary>
        /// <param name="tagstring"></param>
        /// <returns></returns>
        public static int CorrectBracketPairConut (string tagstring)
        {
            int brackettype = 0;
            for (int i = 0 ; i < Length ; i++)
            {
                int count1 = tagstring.Count(n => n == LeftBrackets[i]);
                int count2 = tagstring.Count(n => n == RightBrackets[i]);
                if (count1 != count2)
                {
                    return -1;
                }
                if (count1 != 0)
                {
                    brackettype++;
                }
            }
            return brackettype;
        }
    }
}