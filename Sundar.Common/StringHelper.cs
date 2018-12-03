using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System
{
    public static class StringHelper
    {
        #region 字符串扩展
        /// <summary>忽略大小写的字符串相等比较，判断是否以任意一个待比较字符串相等</summary>
        /// <param name="value">字符串</param>
        /// <param name="strs">待比较字符串数组</param>
        /// <returns></returns>
        public static Boolean EqualIgnoreCase(this String value, params String[] strs)
        {
            foreach (var item in strs)
            {
                if (String.Equals(value, item, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }

        /// <summary>忽略大小写的字符串开始比较，判断是否以任意一个待比较字符串开始</summary>
        /// <param name="value">字符串</param>
        /// <param name="strs">待比较字符串数组</param>
        /// <returns></returns>
        public static Boolean StartsWithIgnoreCase(this String value, params String[] strs)
        {
            if (String.IsNullOrEmpty(value)) return false;

            foreach (var item in strs)
            {
                if (value.StartsWith(item, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }

        /// <summary>忽略大小写的字符串结束比较，判断是否以任意一个待比较字符串结束</summary>
        /// <param name="value">字符串</param>
        /// <param name="strs">待比较字符串数组</param>
        /// <returns></returns>
        public static Boolean EndsWithIgnoreCase(this String value, params String[] strs)
        {
            if (String.IsNullOrEmpty(value)) return false;

            foreach (var item in strs)
            {
                if (value.EndsWith(item, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }

        /// <summary>指示指定的字符串是 null 还是 String.Empty 字符串</summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static Boolean IsNullOrEmpty(this String value) { return value == null || value.Length <= 0; }

        /// <summary>是否空或者空白字符串</summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static Boolean IsNullOrWhiteSpace(this String value)
        {
            if (value != null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (!Char.IsWhiteSpace(value[i])) return false;
                }
            }
            return true;
        }

        /// <summary>拆分字符串，过滤空格，无效时返回空数组</summary>
        /// <param name="value">字符串</param>
        /// <param name="separators">分组分隔符，默认逗号分号</param>
        /// <returns></returns>
        public static String[] Split(this String value, params String[] separators)
        {
            if (String.IsNullOrEmpty(value)) return new String[0];
            if (separators == null || separators.Length < 1 || separators.Length == 1 && separators[0].IsNullOrEmpty()) separators = new String[] { ",", ";" };

            return value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>拆分字符串成为整型数组，默认逗号分号分隔，无效时返回空数组</summary>
        /// <remarks>过滤空格、过滤无效、不过滤重复</remarks>
        /// <param name="value">字符串</param>
        /// <param name="separators">分组分隔符，默认逗号分号</param>
        /// <returns></returns>
        public static Int32[] SplitAsInt(this String value, params String[] separators)
        {
            if (String.IsNullOrEmpty(value)) return new Int32[0];
            if (separators == null || separators.Length < 1) separators = new String[] { ",", ";" };

            var ss = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<Int32>();
            foreach (var item in ss)
            {
                var id = 0;
                if (!Int32.TryParse(item.Trim(), out id)) continue;

                // 本意只是拆分字符串然后转为数字，不应该过滤重复项
                //if (!list.Contains(id))
                list.Add(id);
            }

            return list.ToArray();
        }

        /// <summary>拆分字符串成为名值字典。逗号分号分组，等号分隔</summary>
        /// <param name="value">字符串</param>
        /// <param name="nameValueSeparator">名值分隔符，默认等于号</param>
        /// <param name="separators">分组分隔符，默认逗号分号</param>
        /// <returns></returns>
        public static IDictionary<String, String> SplitAsDictionary(this String value, String nameValueSeparator = "=", params String[] separators)
        {
            var dic = new Dictionary<String, String>();
            if (value.IsNullOrWhiteSpace()) return dic;

            if (String.IsNullOrEmpty(nameValueSeparator)) nameValueSeparator = "=";
            if (separators == null || separators.Length < 1) separators = new String[] { ",", ";" };

            var ss = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (ss == null || ss.Length < 1) return null;

            foreach (var item in ss)
            {
                var p = item.IndexOf(nameValueSeparator);
                // 在前后都不行
                if (p <= 0 || p >= item.Length - 1) continue;

                var key = item.Substring(0, p).Trim();
                dic[key] = item.Substring(p + nameValueSeparator.Length).Trim();
            }

            return dic;
        }

        /// <summary>把一个列表组合成为一个字符串，默认逗号分隔</summary>
        /// <param name="value"></param>
        /// <param name="separator">组合分隔符，默认逗号</param>
        /// <returns></returns>
        public static String Join(this IEnumerable value, String separator = ",")
        {
            var sb = new StringBuilder();
            if (value != null)
            {
                foreach (var item in value)
                {
                    sb.Separate(separator).Append(item + "");
                }
            }
            return sb.ToString();
        }

        /// <summary>把一个列表组合成为一个字符串，默认逗号分隔</summary>
        /// <param name="value"></param>
        /// <param name="separator">组合分隔符，默认逗号</param>
        /// <param name="func">把对象转为字符串的委托</param>
        /// <returns></returns>
        public static String Join<T>(this IEnumerable<T> value, String separator = ",", Func<T, String> func = null)
        {
            var sb = new StringBuilder();
            if (value != null)
            {
                if (func == null) func = obj => obj + "";
                foreach (var item in value)
                {
                    sb.Separate(separator).Append(func(item));
                }
            }
            return sb.ToString();
        }

        /// <summary>追加分隔符字符串，忽略开头，常用于拼接</summary>
        /// <param name="sb">字符串构造者</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static StringBuilder Separate(this StringBuilder sb, String separator)
        {
            if (sb == null || String.IsNullOrEmpty(separator)) return sb;

            if (sb.Length > 0) sb.Append(separator);

            return sb;
        }

        /// <summary>字符串转数组</summary>
        /// <param name="value">字符串</param>
        /// <param name="encoding">编码，默认utf-8无BOM</param>
        /// <returns></returns>
        public static Byte[] GetBytes(this String value, Encoding encoding = null)
        {
            if (value == null) return null;
            if (value == String.Empty) return new Byte[0];

            if (encoding == null) encoding = Encoding.UTF8;
            return encoding.GetBytes(value);
        }

        /// <summary>格式化字符串。特别支持无格式化字符串的时间参数</summary>
        /// <param name="value">格式字符串</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static String F(this String value, params Object[] args)
        {
            if (String.IsNullOrEmpty(value)) return value;

            // 特殊处理时间格式化。这些年，无数项目实施因为时间格式问题让人发狂
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is DateTime)
                {
                    // 没有写格式化字符串的时间参数，一律转为标准时间字符串
                    if (value.Contains("{" + i + "}")) args[i] = ((DateTime)args[i]).ToFullString();
                }
            }

            return String.Format(value, args);
        }
        #endregion

        #region 截取扩展
        /// <summary>确保字符串以指定的另一字符串开始，不区分大小写</summary>
        /// <param name="str">字符串</param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static String EnsureStart(this String str, String start)
        {
            if (String.IsNullOrEmpty(start)) return str;
            if (String.IsNullOrEmpty(str)) return start;

            if (str.StartsWith(start, StringComparison.OrdinalIgnoreCase)) return str;

            return start + str;
        }

        /// <summary>确保字符串以指定的另一字符串结束，不区分大小写</summary>
        /// <param name="str">字符串</param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static String EnsureEnd(this String str, String end)
        {
            if (String.IsNullOrEmpty(end)) return str;
            if (String.IsNullOrEmpty(str)) return end;

            if (str.EndsWith(end, StringComparison.OrdinalIgnoreCase)) return str;

            return str + end;
        }

        /// <summary>从当前字符串开头移除另一字符串，不区分大小写，循环多次匹配前缀</summary>
        /// <param name="str">当前字符串</param>
        /// <param name="starts">另一字符串</param>
        /// <returns></returns>
        public static String TrimStart(this String str, params String[] starts)
        {
            if (String.IsNullOrEmpty(str)) return str;
            if (starts == null || starts.Length < 1 || String.IsNullOrEmpty(starts[0])) return str;

            for (int i = 0; i < starts.Length; i++)
            {
                if (str.StartsWith(starts[i], StringComparison.OrdinalIgnoreCase))
                {
                    str = str.Substring(starts[i].Length);
                    if (String.IsNullOrEmpty(str)) break;

                    // 从头开始
                    i = -1;
                }
            }
            return str;
        }

        /// <summary>从当前字符串结尾移除另一字符串，不区分大小写，循环多次匹配后缀</summary>
        /// <param name="str">当前字符串</param>
        /// <param name="ends">另一字符串</param>
        /// <returns></returns>
        public static String TrimEnd(this String str, params String[] ends)
        {
            if (String.IsNullOrEmpty(str)) return str;
            if (ends == null || ends.Length < 1 || String.IsNullOrEmpty(ends[0])) return str;

            for (int i = 0; i < ends.Length; i++)
            {
                if (str.EndsWith(ends[i], StringComparison.OrdinalIgnoreCase))
                {
                    str = str.Substring(0, str.Length - ends[i].Length);
                    if (String.IsNullOrEmpty(str)) break;

                    // 从头开始
                    i = -1;
                }
            }
            return str;
        }

        /// <summary>从字符串中检索子字符串，在指定头部字符串之后，指定尾部字符串之前</summary>
        /// <remarks>常用于截取xml某一个元素等操作</remarks>
        /// <param name="str">目标字符串</param>
        /// <param name="after">头部字符串，在它之后</param>
        /// <param name="before">尾部字符串，在它之前</param>
        /// <param name="startIndex">搜索的开始位置</param>
        /// <param name="positions">位置数组，两个元素分别记录头尾位置</param>
        /// <returns></returns>
        public static String Substring(this String str, String after, String before = null, Int32 startIndex = 0, Int32[] positions = null)
        {
            if (String.IsNullOrEmpty(str)) return str;
            if (String.IsNullOrEmpty(after) && String.IsNullOrEmpty(before)) return str;

            /*
             * 1，只有start，从该字符串之后部分
             * 2，只有end，从开头到该字符串之前
             * 3，同时start和end，取中间部分
             */

            var p = -1;
            if (!String.IsNullOrEmpty(after))
            {
                p = str.IndexOf(after, startIndex);
                if (p < 0) return null;
                p += after.Length;

                // 记录位置
                if (positions != null && positions.Length > 0) positions[0] = p;
            }

            if (String.IsNullOrEmpty(before)) return str.Substring(p);

            var f = str.IndexOf(before, p >= 0 ? p : startIndex);
            if (f < 0) return null;

            // 记录位置
            if (positions != null && positions.Length > 1) positions[1] = f;

            if (p >= 0)
                return str.Substring(p, f - p);
            else
                return str.Substring(0, f);
        }

        /// <summary>根据最大长度截取字符串，并允许以指定空白填充末尾</summary>
        /// <param name="str">字符串</param>
        /// <param name="maxLength">截取后字符串的最大允许长度，包含后面填充</param>
        /// <param name="pad">需要填充在后面的字符串，比如几个圆点</param>
        /// <returns></returns>
        public static String Cut(this String str, Int32 maxLength, String pad = null)
        {
            if (String.IsNullOrEmpty(str) || maxLength <= 0 || str.Length < maxLength) return str;

            // 计算截取长度
            var len = maxLength;
            if (!String.IsNullOrEmpty(pad)) len -= pad.Length;
            if (len <= 0) return pad;

            return str.Substring(0, len) + pad;
        }

        ///// <summary>根据最大长度截取字符串（二进制计算长度），并允许以指定空白填充末尾</summary>
        ///// <remarks>默认采用Default编码进行处理，其它编码请参考本函数代码另外实现</remarks>
        ///// <param name="str">字符串</param>
        ///// <param name="maxLength">截取后字符串的最大允许长度，包含后面填充</param>
        ///// <param name="pad">需要填充在后面的字符串，比如几个圆点</param>
        ///// <param name="strict">严格模式时，遇到截断位置位于一个字符中间时，忽略该字符，否则包括该字符。默认true</param>
        ///// <returns></returns>
        //public static String CutBinary(this String str, Int32 maxLength, String pad = null, Boolean strict = true)
        //{
        //    if (String.IsNullOrEmpty(str) || maxLength <= 0 || str.Length < maxLength) return str;

        //    var encoding = Encoding.Default;

        //    var buf = encoding.GetBytes(str);
        //    if (buf.Length < maxLength) return str;

        //    // 计算截取字节长度
        //    var len = maxLength;
        //    if (!String.IsNullOrEmpty(pad)) len -= encoding.GetByteCount(pad);
        //    if (len <= 0) return pad;

        //    // 计算截取字符长度。避免把一个字符劈开
        //    var clen = 0;
        //    while (true)
        //    {
        //        try
        //        {
        //            clen = encoding.GetCharCount(buf, 0, len);
        //            break;
        //        }
        //        catch (DecoderFallbackException)
        //        {
        //            // 发生了回退，减少len再试
        //            len--;
        //        }
        //    }
        //    // 可能过长，修正
        //    if (strict) while (encoding.GetByteCount(str.ToCharArray(), 0, clen) > len) clen--;

        //    return str.Substring(0, clen) + pad;
        //}

        /// <summary>从当前字符串开头移除另一字符串以及之前的部分</summary>
        /// <param name="str">当前字符串</param>
        /// <param name="starts">另一字符串</param>
        /// <returns></returns>
        public static String CutStart(this String str, params String[] starts)
        {
            if (String.IsNullOrEmpty(str)) return str;
            if (starts == null || starts.Length < 1 || String.IsNullOrEmpty(starts[0])) return str;

            for (int i = 0; i < starts.Length; i++)
            {
                var p = str.IndexOf(starts[i]);
                if (p >= 0)
                {
                    str = str.Substring(p + starts[i].Length);
                    if (String.IsNullOrEmpty(str)) break;
                }
            }
            return str;
        }

        /// <summary>从当前字符串结尾移除另一字符串以及之后的部分</summary>
        /// <param name="str">当前字符串</param>
        /// <param name="ends">另一字符串</param>
        /// <returns></returns>
        public static String CutEnd(this String str, params String[] ends)
        {
            if (String.IsNullOrEmpty(str)) return str;
            if (ends == null || ends.Length < 1 || String.IsNullOrEmpty(ends[0])) return str;

            for (int i = 0; i < ends.Length; i++)
            {
                var p = str.LastIndexOf(ends[i]);
                if (p >= 0)
                {
                    str = str.Substring(0, p);
                    if (String.IsNullOrEmpty(str)) break;
                }
            }
            return str;
        }
        #endregion
    }
}
