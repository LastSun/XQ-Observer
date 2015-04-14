using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Newtonsoft.Json;

namespace Core
{
    public class Watcher
    {
        private static Histories _histories;
        private static HttpWebRequest _httpWebRequest;
        private long _lastUpdate;

        public bool IsUpdated
        {
            get { return _histories.List.First().UpdatedAt != _lastUpdate; }
        }

        private static string MailTitle
        {
            get
            {
                return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))
                    .AddMilliseconds(_histories.List.First().UpdatedAt)
                    .ToString(CultureInfo.CurrentCulture);
            }
        }

        private static string MailBody
        {
            get
            {
                var bodyBuilder = new StringBuilder();
                foreach (var rebalancingHistory in _histories.List.First().RebalancingHistories)
                {
                    bodyBuilder.AppendFormat("{0} {1}%\t->\t{2}% @ {3}",
                        rebalancingHistory.StockName,
                        rebalancingHistory.PrevWeight ?? 0,
                        rebalancingHistory.TargetWeight,
                        rebalancingHistory.Price).AppendLine();
                }
                return bodyBuilder.ToString();
            }
        }

        public void ConstructRequest()
        {
            const string historyUrl = @"http://xueqiu.com/cubes/rebalancing/history.json?cube_symbol=ZH010389&count=2&page=1";
            _httpWebRequest = (HttpWebRequest)WebRequest.Create(historyUrl);
            _httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
            _httpWebRequest.Method = "Get";
            _httpWebRequest.Accept = @"text/html, application/xhtml+xml, */*";
            _httpWebRequest.UserAgent = @"Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko";
            _httpWebRequest.Host = @"xueqiu.com";
            _httpWebRequest.KeepAlive = true;
            _httpWebRequest.Headers.Add("Accept-Language", "zh-Hans-CN,zh-Hans;q=0.8,en-US;q=0.5,en;q=0.3");
            _httpWebRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            _httpWebRequest.Headers.Add("DNT", "1");
            _httpWebRequest.Headers.Add("Cache-Control", "no-cache");
            _httpWebRequest.CookieContainer = new CookieContainer();
            _httpWebRequest.CookieContainer.Add(new Cookie("__utmc", "1") { Domain = _httpWebRequest.Host });
            _httpWebRequest.CookieContainer.Add(new Cookie("Hm_lpvt_1db88642e346389874251b5a1eded6e3", "1428761416") { Domain = _httpWebRequest.Host });
            _httpWebRequest.CookieContainer.Add(new Cookie("Hm_lvt_1db88642e346389874251b5a1eded6e3", "1428599367%2C1428635934%2C1428726915%2C1428745718") { Domain = _httpWebRequest.Host });
            _httpWebRequest.CookieContainer.Add(new Cookie("xq_a_token", "44ee92e8e9963a215403b5f1132d55c91b5bf4cf") { Domain = _httpWebRequest.Host });
            _httpWebRequest.CookieContainer.Add(new Cookie("xq_r_token", "967f59ca3edf2adb1314f7e4a723371534f563cb") { Domain = _httpWebRequest.Host });
            _httpWebRequest.CookieContainer.Add(new Cookie("xqat", "44ee92e8e9963a215403b5f1132d55c91b5bf4cf") { Domain = _httpWebRequest.Host });
            _httpWebRequest.CookieContainer.Add(new Cookie("xq_token_expire", "Tue%20May%2005%202015%2001%3A09%3A42%20GMT%2B0800%20(CST)") { Domain = _httpWebRequest.Host });
            _httpWebRequest.CookieContainer.Add(new Cookie("xq_is_login", "1") { Domain = _httpWebRequest.Host });
            _httpWebRequest.CookieContainer.Add(new Cookie("bid", "92cba84adb301a85a3c2b2bff259abd6_i8af6qbi") { Domain = _httpWebRequest.Host });
            _httpWebRequest.CookieContainer.Add(new Cookie("__utma", "1.1105528213.1428726915.1428759326.1428761416.6") { Domain = _httpWebRequest.Host });
            _httpWebRequest.CookieContainer.Add(new Cookie("__utmz", "1.1428726915.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)") { Domain = _httpWebRequest.Host });
            _httpWebRequest.CookieContainer.Add(new Cookie("__utmb", "1.1.10.1428761416") { Domain = _httpWebRequest.Host });
            _httpWebRequest.CookieContainer.Add(new Cookie("__utmt", "1") { Domain = _httpWebRequest.Host });
        }

        public void QueryData()
        {
            using (var response = (HttpWebResponse)_httpWebRequest.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK) return;
                Stream receiveStream = response.GetResponseStream() ?? new MemoryStream();
                using (var readStream = response.CharacterSet == null ? new StreamReader(receiveStream) : new StreamReader(receiveStream, Encoding.UTF8))
                {
                    string data = readStream.ReadToEnd();
                    _histories = JsonConvert.DeserializeObject<Histories>(data);
                    _lastUpdate = _histories.List.First().UpdatedAt;
                }
            }
        }

        public void SendMail()
        {
            new SmtpClient
            {
                Host = "smtp-mail.outlook.com",
                Port = 25,
                Credentials = new NetworkCredential("lastsun@outlook.com", "!zhxcql120503"),
                EnableSsl = true
            }.Send(new MailMessage("lastsun@outlook.com", "lastsun@outlook.com,hylkcxt@163.com", MailTitle, MailBody));
        }
    }
}