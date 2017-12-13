using BloodSearch.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace BloodSearch.Core.Extensions {

    public static class StringExtensions {

        private static Regex rxClearSymbolsAndSpaces = new Regex("[^a-zA-Zа-яА-Я0-9]", RegexOptions.CultureInvariant);

        private static Regex rxDoubleSpaces = new Regex("[ ]{2,}", RegexOptions.None);

        private static Regex rxNewLines = new Regex("[\n]", RegexOptions.None);
        private static Regex rxNewRecords = new Regex("[\r]", RegexOptions.None);

        private static readonly Regex _tags_ = new Regex(@"<[^>]+?>", RegexOptions.Multiline | RegexOptions.Compiled);

        //add characters that are should not be removed to this regex
        private static readonly Regex _notOkCharacter_ = new Regex(@"[^\w;&#@.:/\\?=|%!() -]", RegexOptions.Compiled);

        #region translitDict

        private static List<string> lat_up = new List<string> { "A", "B", "V", "G", "D", "E", "Yo", "Zh", "Z", "I", "Y", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "H", "C", "Ch", "Sh", "Sch", "", "Y", "", "E", "Yu", "Ya", "-", "-", "-" };
        private static List<string> lat_low = new List<string> { "a", "b", "v", "g", "d", "e", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "h", "c", "ch", "sh", "sch", "", "y", "", "e", "yu", "ya", "-", "-", "-" };
        private static List<string> rus_up = new List<string> { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я", " ", "-", "_" };
        private static List<string> rus_low = new List<string> { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я", " ", "-", "_" };

        #endregion

        public static DateTime? ToDateTime(this string value, string format = "") {
            if (string.IsNullOrEmpty(value) == true)
                return (DateTime?)null;

            try {
                DateTime dt = DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);

                return dt;
            } catch {
                return (DateTime?)null;
            }
        }

        public static int? ToInt(this string value) {
            var normalizedValue = value.NormalizeNumberFormat();

            float ret;
            if (float.TryParse(normalizedValue, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out ret) && ret < int.MaxValue)
                return Convert.ToInt32(ret);

            return null;
        }

        public static long? ToLong(this string value) {
            var normalizedValue = value.NormalizeNumberFormat();

            float ret;
            if (float.TryParse(normalizedValue, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out ret))
                return Convert.ToInt64(ret);

            return (long?)null;
        }

        public static double? ToDouble(this string value) {
            var normalizedValue = value.NormalizeNumberFormat();

            double ret;
            if (double.TryParse(normalizedValue, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out ret))
                return ret;

            return (double?)null;
        }

        public static float? ToFloat(this string value) {
            var normalizedValue = value.NormalizeNumberFormat();

            float ret;
            if (float.TryParse(normalizedValue, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out ret))
                return ret;

            return (float?)null;
        }

        private static string NormalizeNumberFormat(this string value) {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            var ret = value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator)
                         .Replace(",", CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);

            return ret;
        }

        public static decimal? ToDecimal(this string value) {
            var normalizedValue = value.NormalizeNumberFormat();

            decimal ret;
            if (decimal.TryParse(normalizedValue, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out ret))
                return ret;

            return (decimal?)null;
        }

        public static int? ToPositiveInt(this string value) {
            var normalizedValue = value.NormalizeNumberFormat();

            int ret;
            if (int.TryParse(normalizedValue, out ret)) {
                if (ret > 0)
                    return ret;
                else
                    return null;
            }

            return (int?)null;
        }

        public static float? ToPositiveFloat(this string value) {
            var normalizedValue = value.NormalizeNumberFormat();

            float ret;
            if (float.TryParse(normalizedValue, out ret)) {
                if (ret > 0)
                    return ret;
                else
                    return null;
            }

            return (float?)null;
        }

        public static long? ToPositiveLong(this string value) {
            var normalizedValue = value.NormalizeNumberFormat();

            long ret;
            if (long.TryParse(normalizedValue, out ret)) {
                if (ret > 0)
                    return ret;
                else
                    return null;
            }

            return (long?)null;
        }

        public static List<int> SplitInt(this string value, string[] separator = null) {
            var ret = new List<int>();

            if (string.IsNullOrEmpty(value))
                return ret;

            var splitted = value.Split(separator ?? new string[] { ";", ",", "-" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in splitted) {
                int iValue;
                if (Int32.TryParse(item, out iValue)) {
                    ret.Add(iValue);
                }
            }

            return ret;
        }

        public static T ToEnum<T>(this string value, T defaultValue) where T : struct {
            if (string.IsNullOrEmpty(value)) {
                return defaultValue;
            }

            T result;
            return Enum.TryParse<T>(value, true, out result) ? result : defaultValue;
        }

        public static string ClearSymbolsAndSpaces(this string value) {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return rxClearSymbolsAndSpaces.Replace(value, string.Empty);
        }

        public static string ToUpperFirst(this string input) {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return input.First().ToString().ToUpper() + string.Join("", input.Skip(1));
        }

        public static string TrimSafety(this string value) {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value.Trim();
        }

        public static string ToUpperSafety(this string value) {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value.ToUpper();
        }

        public static string ToLowerSafety(this string value) {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value.ToLower();
        }

        public static string NormalizeString(this string value) {
            if (string.IsNullOrEmpty(value) == true)
                return string.Empty;

            return value.Replace("\n", "").Trim();
        }

        public static string RemoveHTML(this string value) {
            return Regex.Replace(value, "<(.|\n)*?>", "");
        }

        public static PhoneModel ParsePhone(this string phone) {
            string country;
            string code;

            if (string.IsNullOrEmpty(phone))
                return null;

            if (phone.StartsWith("8", StringComparison.Ordinal) && phone.Length != 10)
                phone = phone.Substring(1).Insert(0, "+7");

            if (!Regex.IsMatch(phone, @"^\s*\+?[0-9\(\)\s\-]{6,}\s*$"))
                return null;

            var hasPlus = phone.IndexOf("+", StringComparison.Ordinal) > -1;

            phone = Regex.Replace(phone, @"(\t|\n)", " ", RegexOptions.Multiline);

            while (phone.IndexOf("  ", StringComparison.Ordinal) > -1)
                phone = phone.Replace("  ", " ");

            phone = phone.Trim();
            if (Regex.IsMatch(phone, @"\+{1}\d+\s{1}\d+\s{1}.+")) {
                var scobInd = phone.IndexOf(" ", StringComparison.Ordinal);
                if (scobInd > -1) {
                    var scobInd2 = phone.IndexOf(" ", scobInd + 1, StringComparison.Ordinal);
                    if (scobInd2 - scobInd >= 3) {
                        phone = phone.Remove(scobInd, 1);
                        phone = phone.Insert(scobInd, "(");
                        scobInd = phone.IndexOf(" ", StringComparison.Ordinal);
                        if (scobInd > -1) {
                            phone = phone.Remove(scobInd, 1);
                            phone = phone.Insert(scobInd, ")");
                        }
                    }
                }
            }

            phone = phone.Replace(" ", "").Replace("+", "").Replace("-", "");

            if (phone.StartsWith("7(9", StringComparison.Ordinal) && phone.Length == 13)
                phone = phone.Replace("(", "").Replace(")", "");

            var codeBeginInd = phone.IndexOf("(", StringComparison.Ordinal);
            var codeLength = -1;
            var codeInBlock = false;
            if (codeBeginInd > -1) {
                var codeEndInd = phone.IndexOf(")", codeBeginInd, StringComparison.Ordinal);
                if (codeEndInd > codeBeginInd) {
                    codeEndInd--;
                    codeInBlock = true;
                    codeLength = codeEndInd - codeBeginInd;
                }
            }

            phone = phone.Replace("(", "").Replace(")", "");
            //phone = Regex.Replace(phone, @"\D", "");
            if (phone.Length < 7)
                return null;

            var countryLength = phone.Length - 10;
            if (countryLength > 0) {
                if (codeInBlock) {
                    country = phone.Substring(0, codeBeginInd);
                    countryLength = country.Length;
                } else {
                    country = phone.Substring(0, countryLength);
                    if (phone.StartsWith("8", StringComparison.Ordinal))
                        country = "7";
                }
            } else {
                if (!hasPlus) {
                    countryLength = 0;
                    country = "7";
                } else {
                    if (!codeInBlock) {
                        countryLength = 2;
                        country = phone.Substring(0, countryLength);
                    } else {
                        countryLength = codeBeginInd;
                        country = phone.Substring(0, countryLength);
                    }
                }
            }

            if (codeInBlock) {
                code = phone.Substring(codeBeginInd, codeLength);
            } else {
                codeLength = phone.Length - countryLength - 7;
                if (codeLength < 1) {
                    code = "495";
                } else {
                    code = phone.Substring(countryLength, codeLength);
                }
            }

            var number = phone.Substring(countryLength + codeLength);

            if (string.IsNullOrEmpty(number)) {
                if (!string.IsNullOrEmpty(code)) {
                    number = code;
                    code = string.Empty;
                } else {
                    return null;
                }
            }

            if (code.Length < 1)
                return null;

            //formattedPhone = phoneFormater.Invoke (country, code, number);
            if (!string.IsNullOrEmpty(country))
                country = "+" + country;

            return new PhoneModel { CountryCode = country, Code = code, Number = number };
        }

        public static string RemoveDoubleSpaces(this string value) {
            return rxDoubleSpaces.Replace(value, " ");
        }

        public static string RemoveDoubleLines(this string value) {
            var ret = rxNewLines.Replace(value, " ");
            return rxNewRecords.Replace(value, " ");
        }

        public static string TranslitRealtyClass(this string str) {
            if (string.IsNullOrEmpty(str)) {
                return string.Empty;
            }

            var arr_lat = new[] { "A", "B", "B", "C" };

            var arr_rus = new[] { "А", "В", "Б", "С" };

            str = str.ToUpper();

            for (int i = 0; i < arr_rus.Length; i++) {
                str = str.Replace(arr_rus[i], arr_lat[i]);
            }

            return str;
        }

        public static string Translit(this string str) {
            if (string.IsNullOrEmpty(str)) {
                return string.Empty;
            }

            for (int i = 0; i < rus_up.Count; i++) {
                str = str.Replace(rus_up[i], lat_up[i]);
                str = str.Replace(rus_low[i], lat_low[i]);
            }

            return str;
        }

        public static string TranslitToLowerAndReplaceOther(this string str) {
            if (string.IsNullOrEmpty(str)) {
                return string.Empty;
            }

            var ret = new StringBuilder();

            for (int i = 0; i < str.Length; i++) {
                var word = str[i].ToString();

                var index = rus_low.IndexOf(word);

                if (index >= 0) {
                    ret.Append(lat_low[index]);
                } else {
                    var digit = word.ToInt();
                    if (digit.HasValue) {
                        ret.Append(word);
                    }
                }
            }

            return ret.ToString();
        }

        public static string RemoveHtmlTags(this string html) {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            html = HttpUtility.UrlDecode(html);
            html = HttpUtility.HtmlDecode(html);

            html = RemoveTag(html, "<!--", "-->");
            html = RemoveTag(html, "<script", "</script>");
            html = RemoveTag(html, "<style", "</style>");

            //replace matches of these regexes with space
            html = _tags_.Replace(html, " ");
            html = _notOkCharacter_.Replace(html, " ");
            html = SingleSpacedTrim(html);

            return html;
        }

        private static String RemoveTag(String html, String startTag, String endTag) {
            Boolean bAgain;
            do {
                bAgain = false;
                Int32 startTagPos = html.IndexOf(startTag, 0, StringComparison.CurrentCultureIgnoreCase);
                if (startTagPos < 0)
                    continue;
                Int32 endTagPos = html.IndexOf(endTag, startTagPos + 1, StringComparison.CurrentCultureIgnoreCase);
                if (endTagPos <= startTagPos)
                    continue;
                html = html.Remove(startTagPos, endTagPos - startTagPos + endTag.Length);
                bAgain = true;
            } while (bAgain);
            return html;
        }

        private static String SingleSpacedTrim(String inString) {
            StringBuilder sb = new StringBuilder();
            Boolean inBlanks = false;
            foreach (Char c in inString) {
                switch (c) {
                    case '\r':
                    case '\n':
                    case '\t':
                    case ' ':
                        if (!inBlanks) {
                            inBlanks = true;
                            sb.Append(' ');
                        }
                        continue;
                    default:
                        inBlanks = false;
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString().Trim();
        }

        public static List<string> SplitToListSafety(this string txt, string[] separator) {
            if (string.IsNullOrEmpty(txt))
                return new List<string>();

            var ret = txt.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            return ret.ToList();
        }

        public static List<string> SplitToListSafety(this string txt, string separator) {
            if (string.IsNullOrEmpty(txt))
                return new List<string>();

            var ret = txt.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            return ret.ToList();
        }

        /// <summary>
		/// Метод, осуществляющий удаления тегов из строки, а так же заменяющий
		/// каждый спецсимвол \n на один тэг &lt;br /&gt;
		/// </summary>
		/// <param name="str">Входная строка</param>
		/// <param name="insertBreaks">если true, заменяет \n на &lt;br /&gt;</param>
		/// <returns>Результат</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "str")]
        public static string RemoveHtmlTags(this string str, bool insertBreaks) {
            if (String.IsNullOrEmpty(str))
                return str;


            Regex regexRemoveHtmlTags1 = new Regex("<[^>]*>");
            Regex regexRemoveHtmlTags2 = new Regex("\\S{50}");
            Regex regexRemoveHtmlTags3 = new Regex("([.,])(\\S)");
            Regex regexRemoveHtmlTags4 = new Regex("(\\S{49})(\\S)");
            Regex regexRemoveHtmlTags5 = new Regex("\n");

            str = regexRemoveHtmlTags1.Replace(str, "");

            if (regexRemoveHtmlTags2.Match(str).Success)
                str = regexRemoveHtmlTags3.Replace(str, "$1 $2");

            str = regexRemoveHtmlTags4.Replace(str, "$1 $2");

            if (insertBreaks)
                str = regexRemoveHtmlTags5.Replace(str.Replace("\r", ""), "<br />");

            return str;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "str")]
        public static string RemoveHtmlTagsSimple(this string str, bool insertBreaks) {
            if (string.IsNullOrEmpty(str))
                return str;
            str = ReplaceHtmlTags(str, "");


            if (insertBreaks)
                str = new Regex("\n").Replace(str.Replace("\r", ""), "<br />");


            return str;
        }

        /// <summary>
        /// Удаляет тэги
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveHtmlTagsSimple(this string str) {
            return RemoveHtmlTagsSimple(str, false);
        }

        /// <summary>
        /// Удаляет теги и ссылки
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveHtmlTagsAndLinksSimple(this string str) {
            var s = RemoveHtmlTagsSimple(str, false);
            return Regex.Replace(s, @"(?<url>(((ht|f)tp(s?))\://)?((([a-zA-Z0-9_\-]{2,}\.)+[a-zA-Z]{2,})|((?:(?:25[0-5]|2[0-4]\d|[01]\d\d|\d?\d)(?(\.?\d)\.)){4}))(:[a-zA-Z0-9]+)?(/[a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~]*)?)", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);
        }

        /// <summary>
        /// Заменяет теги
        /// </summary>
        /// <param name="str"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string ReplaceHtmlTags(this string str, string replacement) {
            if (string.IsNullOrEmpty(str))
                return str;
            str = Regex.Replace(str, "<[^>]*>", replacement);
            return str;
        }

        /// <summary>
        /// Метод, осуществляющий trim, удаления тегов из строки, а так же заменяющий \n на &lt;br /&gt;
        /// </summary>
        /// <param name="str">Входная строка</param>
        /// <param name="insertBreaks">если true, заменяет \n на &lt;br /&gt;</param>
        /// <returns>Результат</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "str")]
        public static string RemoveHtmlTagsAndTrim(this string str, bool insertBreaks) {
            if (str == null)
                return null;

            str = str.Trim();
            return RemoveHtmlTags(str, insertBreaks);
        }

        /// <summary>
        /// PreventXSSInText
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string PreventXSSInText(this string text) {
            if (String.IsNullOrEmpty(text))
                return text;


            string[] badTags = "drop|delete|applet|meta|xml|blink|link|style|script|embed|iframe|frame|frameset|ilayer|layer|bgsound|title|base".Split('|');

            string res = text;

            foreach (string tag in badTags) {
                res = Regex.Replace(text, string.Format(@"<{0}.*?>(.*?)<\/{0}>", tag), "", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            }

            string noJavascript = @"j([\x00-\x20]|&#\w+?;)*a([\x00-\x20]|&#\w+?;)*v([\x00-\x20]|&#\w+?;)*a([\x00-\x20]|&#\w+?;)*s([\x00-\x20]|&#\w+?;)*c([\x00-\x20]|&#\w+?;)*r([\x00-\x20]|&#\w+?;)*i([\x00-\x20]|&#\w+?;)*p([\x00-\x20]|&#\w+?;)*t([\x00-\x20]|&#\w+?;)*";

            res = Regex.Replace(text, noJavascript, "NOSCRIPT", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

            return res;
        }

        /// <summary>
        /// BBCode to Html
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string BBCodeToHTML(this string input) {
            string str = input;

            str = HttpUtility.HtmlEncode(HttpUtility.HtmlDecode(str));

            Regex exp;
            // format the bold tags: [b][/b]
            // becomes: <strong></strong>


            exp = new Regex(@"\[\w\]\[/\w\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            str = exp.Replace(str, "");

            //exp = new Regex(@"\[b\]([^\]]+)\[/b\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            //str = exp.Replace(str, "<strong>$1</strong>");


            exp = new Regex(@"\[b\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            str = exp.Replace(str, "<strong>");

            exp = new Regex(@"\[/b\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            str = exp.Replace(str, "</strong>");


            exp = new Regex(@"\[i\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            str = exp.Replace(str, "<em>");

            exp = new Regex(@"\[/i\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            str = exp.Replace(str, "</em>");

            exp = new Regex(@"\[u\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            str = exp.Replace(str, "<u>");

            exp = new Regex(@"\[/u\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            str = exp.Replace(str, "</u>");

            exp = new Regex(@"\[s\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            str = exp.Replace(str, "<strike>");

            exp = new Regex(@"\[/s\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            str = exp.Replace(str, "</strike>");



            // format the url tags: [url=www.website.com]my site[/url]
            // becomes: <a href="www.website.com">my site</a>
            exp = new Regex(@"\[url\=(?<link>(/|http:).+?)\](?<text>[^\]]+)\[/url\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            str = exp.Replace(str, "<noindex><a href=\"${link}\" rel=\"nofollow\">${text}</a></noindex>");

            // format the img tags: [img]www.website.com/img/image.jpeg[/img]
            // becomes: <img src="www.website.com/img/image.jpeg" />
            exp = new Regex(@"\[img\]((/|http:).+?)\[/img\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            str = exp.Replace(str, "<img src=\"$1\" />");


            //format the colour tags: [color=red][/color]

            //exp = new Regex(@"\[color\=([^\]]+)\]([^\]]+)\[/color\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            //str = exp.Replace(str, "<font color=\"$1\">$2</font>");


            // format the size tags: [size=3][/size]
            // becomes: <font size="+3"></font>
            //exp = new Regex(@"\[size\=([^\]]+)\]([^\]]+)\[/size\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            //str = exp.Replace(str, "<font size=\"+$1\">$2</font>");


            // flash video
            exp = new Regex(@"\[flash=(?<w>\d+?):(?<h>\d+?)\](?<link>http://(www.)?(youtube\.com|vimeo\.com|rutube\.ru)/.+?)\[/flash\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            str = exp.Replace(str, "<object width=\"${w}\" height=\"${h}\" data=\"${link}\" type=\"application/x-shockwave-flash\"><param name=\"src\" value=\"${link}\" /></object>");


            // lastly, replace any new line characters with <br />
            str = str.Replace("\r\n", "<br />\r\n");


            // убираем необработанные []
            str = Regex.Replace(str, @"\[[^\]\]*]", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);


            return str;
        }

        /// <summary>
		/// Обрезает строку по кол-ву символов с учетом слов
		/// </summary>
		/// <param name="input"></param>
		/// <param name="len"></param>
		/// <param name="removeTags"></param>
		/// <returns></returns>
		public static string CropStringWord(this string input, int len, bool removeTags) {
            if (string.IsNullOrEmpty(input))
                return input;

            string res = "";
            char[] chars = { ',', '.', '?', '!', '—', ' ' };

            if (removeTags)
                input = input.ReplaceHtmlTags(" ");

            if (input.Length > 0 && input.Length > len) {
                res = input.Substring(0, len);
                if (!chars.Contains(input.ToCharArray()[len])) {
                    int lastChar = res.LastIndexOfAny(chars);
                    if (lastChar > 0) {
                        res = res.Substring(0, lastChar);
                    }
                }

                res = res.TrimEnd(chars) + "...";

            } else {
                res = input;
            }

            return res;
        }

        /// <summary>
        /// Обрезает строку по кол-ву символов с учетом слов и того, что слова меньше N символов удаляются
        /// </summary>
        /// <param name="input"></param>
        /// <param name="len"></param>
        /// <param name="removeTags"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CropStringWord(this string input, int len, bool removeTags, int len2) {
            if (string.IsNullOrEmpty(input))
                return input;

            string res = "";
            char[] chars = { ',', '.', '?', '!', '—', ' ' };

            if (removeTags)
                input = input.ReplaceHtmlTags(" ");

            if (input.Length > len) {
                res = input.Substring(0, len);
                if (!chars.Contains(input.ToCharArray()[len])) {
                    int lastChar = res.LastIndexOfAny(chars);
                    if (lastChar > 0) {
                        res = res.Substring(0, lastChar);
                    }
                }
                res = res.TrimEnd(chars);

                #region remove last word less then len2 letters
                int lastSpace = res.LastIndexOf(" ");
                if (lastSpace >= (res.Length - len2) && res.Length < lastSpace - 1) {
                    res = res.Substring(0, lastSpace);
                }
                #endregion

                res = res.TrimEnd(chars) + "...";

            } else {
                res = input;
            }

            return res;
        }

        /// <summary>
        /// Обрезает строку по кол-ву символов с учетом слов
        /// </summary>
        /// <param name="input">Целая строка</param>
        /// <param name="len">Максимальное кол-во символов</param>
        /// <returns></returns>
        public static string CropStringWord(this string input, int len) {
            return CropStringWord(input, len, true);

        }
    }
}
