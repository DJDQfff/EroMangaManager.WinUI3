using System.Text;

namespace EroMangaManager.Core.StringParser
{
    /// <summary>
    /// 基于成对的括号进行解析,调用前，使用correctbracketpaircount方法确认括号是成对的。
    /// 根据字符串是否包含在括号中，子字符串分为InsideContent和OutsideContent。
    /// 核心：通过split本玉bracket_keepbracket来分割，再按需取字符串
    /// 
    /// </summary>
    /// 
    public static class BracketBasedStringParser
    {
        internal static readonly int Length = LeftBrackets.Length;

        /// <summary>
        /// 左括号
        /// </summary>
        public const string LeftBrackets = "[【（({";

        /// <summary>
        /// 所有括号
        /// </summary>
        public const string LeftRightBrackets = LeftBrackets + RightBrackets;

        /// <summary>
        /// 右括号
        /// </summary>
        public const string RightBrackets = "]】）)}";

        /// <summary>
        /// 字符串中是否含有任意括号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ContainAnyBrackets (string str)
        {
            foreach (var b in LeftRightBrackets)
            {
                if (str.Contains(b))
                    return true;
            }
            return false;
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

        /// <summary>
        /// 获取括号内字符串，推荐此方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<string> Get_InsideContent (string input)
        {
            return SplitByBrackets_KeepBracket(input)
                     .Where(piece => piece.IsIncludedInBracketPair())
                     .RemoveBracketForEachString();
        }
        /// <summary>
        /// 获取括号外字符串 推荐用这个
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<string> Get_OutsideContent (string input)
        {
            return SplitByBrackets_KeepBracket(input)
         .Where(piece => !piece.IsIncludedInBracketPair())
         .RemoveBracketForEachString();

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
        ///移除其中包含的重复tag
        /// </summary>
        /// <param name="oldname">旧的名字</param>
        public static string RemoveRepeatTag (string oldname)
        {
            // TODO 输入一个包含重复tag的名称，算出一个去掉重复tag的名称
            var pieces = SplitByBrackets_KeepBracket(oldname);
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
        /// 按左右括号分离，保留括号
        /// </summary>
        ///
        /// <param name="_FileDisplayName"></param>
        /// <returns></returns>
        public static List<string> SplitByBrackets_KeepBracket (string _FileDisplayName)
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
    }
}