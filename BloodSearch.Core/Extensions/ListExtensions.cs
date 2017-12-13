using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodSearch.Core.Extensions {

    public static class ListExtensions {

        public static string ToStringEx<T>(this IEnumerable<T> list, string separator = ",", string perfIfNotNull = "") {
            if (list == null || !list.Any())
                return string.Empty;

            if (separator == null)
                separator = string.Empty;

            var r = list.Aggregate(new StringBuilder(), (s, i) => s.Append(separator + i)).Remove(0, separator.Length).ToString();

            return string.IsNullOrEmpty(perfIfNotNull) ? r : perfIfNotNull + r;
        }

        public static void AddSafety(this IList<string> list, string value) {
            if (list == null)
                return;

            if (string.IsNullOrEmpty(value) == false) {
                list.Add(value);
            }
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int parts) {
            return list.Select((item, index) => new { index, item })
                       .GroupBy(x => x.index % parts)
                       .Select(x => x.Select(y => y.item));
        }
    }
}