using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Taki.Common
{
    public static class Utils
    {
        #region ============= HtmlHelper ===============

        #region 获取压缩的html字符串
        /// <summary>
        /// 获取压缩的html字符串
        /// </summary>
        /// <returns></returns>
        public static string GetZipHtml(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 30000;
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0";
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            //设置连接超时时间
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string result = "";
            using (Stream streamReceive = response.GetResponseStream())
            {
                using (GZipStream zipStream = new GZipStream(streamReceive, CompressionMode.Decompress))
                using (StreamReader sr = new StreamReader(zipStream, encoding))
                    result = sr.ReadToEnd();
            }
            return result;
        }
        #endregion

        #endregion

        #region 获取本应用启动程序的上一级物理路径 （即\Debug\*.exe 的上一级目录）
        /// <summary>
        /// 获取本应用启动程序的上一级物理路径 （即\Debug\*.exe 的上一级目录）
        /// </summary>
        /// <returns></returns>
        public static string GetParentDirectory()
        {
            return Directory.GetParent(AppDomain
                    .CurrentDomain
                    .SetupInformation
                    .ApplicationBase.TrimEnd(Path.DirectorySeparatorChar)).FullName;
        }

        /// <summary>
        /// 获取上一级物理路径
        /// </summary>
        /// <returns></returns>
        public static string GetParentDirectory(string pathOrDirectory)
        {
            return Directory.GetParent(pathOrDirectory.TrimEnd(Path.DirectorySeparatorChar)).FullName;
        }
        #endregion
    }
}
