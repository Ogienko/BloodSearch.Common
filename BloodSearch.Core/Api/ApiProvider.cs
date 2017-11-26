using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BloodSearch.Core.Api {

    public static class ApiProvider {

        public class TimedWebClient : WebClient {

            // Timeout in milliseconds, default = 600,000 msec
            public int Timeout { get; set; } = 10000;

            public TimedWebClient() {
                Encoding = Encoding.UTF8;
            }

            protected override WebRequest GetWebRequest(Uri address) {
                var objWebRequest = base.GetWebRequest(address);
                objWebRequest.Timeout = this.Timeout;
                return objWebRequest;
            }
        }

        public static async Task<T> ExecuteGet<T>(string url) {
            using (var wc = new TimedWebClient()) {
                var str = await wc.DownloadStringTaskAsync(new Uri(url));

                return JsonConvert.DeserializeObject<T>(str);
            }
        }

        public static T ExecuteGetSync<T>(string url) {
            using (var wc = new TimedWebClient()) {
                var str = wc.DownloadString(new Uri(url));

                return JsonConvert.DeserializeObject<T>(str);
            }
        }

        public static async Task<string> ExecuteGetAsString(string url) {
            using (var wc = new TimedWebClient()) {
                return await wc.DownloadStringTaskAsync(new Uri(url));
            }
        }

        public static string ExecuteGetAsStringSync(string url, int? timeOut = null) {
            using (var wc = new TimedWebClient()) {
                if (timeOut.HasValue) {
                    wc.Timeout = timeOut.Value;
                }
                return wc.DownloadString(new Uri(url));
            }
        }

        public static byte[] ExecuteGetData(string url) {
            using (var wc = new TimedWebClient()) {
                return wc.DownloadData(new Uri(url));
            }
        }

        public static async Task<T> ExecutePost<T>(string url, string parameters) {
            using (var wc = new TimedWebClient()) {
                wc.Timeout = 30000;
                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                string HtmlResult = await wc.UploadStringTaskAsync(new Uri(url), parameters);

                return JsonConvert.DeserializeObject<T>(HtmlResult);
            }
        }

        public static T ExecutePostSync<T>(string url, string parameters, int timeout = 30000) {
            using (var wc = new TimedWebClient()) {
                wc.Timeout = timeout;
                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                string HtmlResult = wc.UploadString(new Uri(url), parameters);

                return JsonConvert.DeserializeObject<T>(HtmlResult);
            }
        }

        public static T ExecutePutSync<T>(string url, string parameters, int timeout = 30000) {
            using (var wc = new TimedWebClient()) {
                wc.Timeout = timeout;
                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                string HtmlResult = wc.UploadString(new Uri(url), "PUT", parameters);

                return JsonConvert.DeserializeObject<T>(HtmlResult);
            }
        }

        public static T ExecuteDeleteSync<T>(string url, int timeout = 30000) {
            using (var wc = new TimedWebClient()) {
                wc.Timeout = timeout;
                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                string HtmlResult = wc.UploadString(new Uri(url), "DELETE", string.Empty);

                return JsonConvert.DeserializeObject<T>(HtmlResult);
            }
        }

        public static string ExecutePostSyncToString<T>(string url, string parameters) {
            using (var wc = new TimedWebClient()) {
                wc.Timeout = 30000;
                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                string HtmlResult = wc.UploadString(new Uri(url), parameters);

                return HtmlResult;
            }
        }
    }
}