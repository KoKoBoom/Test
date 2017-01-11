using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Taki.Common
{
    public class HttpHelper
    {
        public static bool httpPost(string url, ref CookieContainer cc, ref string dataToPost, ref string dataget, bool isChangeCookie = false)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AllowAutoRedirect = false;
            request.KeepAlive = true;
            request.Method = "POST";
            request.ProtocolVersion = HttpVersion.Version11;
            request.Proxy = WebRequest.DefaultWebProxy;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Headers.Add("Cache-Control: max-age=0");
            request.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
            request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en-US;q=0.6,en;q=0.4,zh-TW;q=0.2,fr;q=0.2,ja;q=0.2");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2008.2 Safari/537.36";
            request.CookieContainer = cc;
            try
            {
                StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                writer.Write((string)dataToPost);
                writer.Flush();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.ContentEncoding == "gzip")
                {
                    MemoryStream ms = new MemoryStream();
                    GZipStream zip = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                    byte[] buffer = new byte[1024];
                    int l = zip.Read(buffer, 0, buffer.Length);
                    while (l > 0)
                    {
                        ms.Write(buffer, 0, l);
                        l = zip.Read(buffer, 0, buffer.Length);
                    }
                    ms.Dispose();
                    zip.Dispose();
                    dataget = Encoding.UTF8.GetString(ms.ToArray());
                }
                else
                    dataget = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8")).ReadToEnd();
                response.Close();
                request.Abort();
                return true;
            }
            catch (WebException exception)
            {
                if (exception.Response != null)
                {
                    dataget = ((HttpWebResponse)exception.Response).StatusCode.ToString();
                }
                request.Abort();
                return false;
            }
            catch
            {
                dataget = "error";
                request.Abort();
                return false;
            }
        }

        public static bool httpGet(string url, ref CookieContainer cc, ref string dataget, bool isChangeCookie = false)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AllowAutoRedirect = false;
            request.KeepAlive = true;
            request.Method = "GET";
            request.ProtocolVersion = HttpVersion.Version11;
            request.Proxy = WebRequest.DefaultWebProxy;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
            request.Headers.Add("Accept-Language", "q=0.8,en-US;q=0.6,en;q=0.2,ja;q=0.2");
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2008.2 Safari/537.36";
            request.CookieContainer = cc;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                dataget = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8")).ReadToEnd();
                response.Close();
                request.Abort();
                return true;
            }
            catch (WebException exception)
            {
                if (exception.Response != null)
                {
                    dataget = ((HttpWebResponse)exception.Response).StatusCode.ToString();
                }
                request.Abort();
                return false;
            }
            catch
            {
                dataget = "error";
                request.Abort();
                return false;
            }
        }

        /// <summary>
        /// 获取网络文件的字节信息
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static byte[] getUriBytes(string uri)
        {
            byte[] byData = null;
            try
            {
                WebRequest request = WebRequest.Create(uri);
                using (WebResponse response = request.GetResponse())
                {
                    Stream s = response.GetResponseStream();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int b;
                        while ((b = s.ReadByte()) != -1)
                        {
                            ms.WriteByte((byte)b);
                        }
                        byData = ms.ToArray();
                    }
                }
            }
            catch
            {

            }
            return byData;
        }
    }
}
