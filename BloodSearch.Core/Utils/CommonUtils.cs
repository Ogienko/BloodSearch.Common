using System;
using System.Collections.Generic;
using System.Threading;

namespace BloodSearch.Core.Utils {

    public static class CommonUtils {

        public static int GetRandom(int maxValue = 100) {
            Random random = new Random();
            return random.Next(0, 100);
        }

        public static class Retry {
            public static void Do(
                Action action,
                int retryIntervalMilliseconds = 300,
                int retryCount = 3) {
                Do<object>(() => {
                    action();
                    return null;
                }, retryIntervalMilliseconds, retryCount);
            }

            public static T Do<T>(
                Func<T> action,
                int retryIntervalMilliseconds = 300,
                int retryCount = 3) {
                var exceptions = new List<Exception>();

                for (int retry = 0; retry < retryCount; retry++) {
                    try {
                        if (retry > 0)
                            Thread.Sleep(TimeSpan.FromMilliseconds(retryIntervalMilliseconds));

                        return action();
                    } catch (Exception ex) {
                        exceptions.Add(ex);
                    }
                }

                throw new AggregateException(exceptions);
            }
        }

        public static string Declension(int number, string nominative, string genitiveSingular, string genitivePlural) {
            int last = number % 10;
            int last2 = number % 100;

            if (last == 1 && last2 != 11)
                return nominative;
            if (last == 2 && last2 != 12 || last == 3 && last2 != 13 || last == 4 && last2 != 14)
                return genitiveSingular;

            return genitivePlural;
        }
    }
}