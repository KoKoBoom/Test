using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace Taki.Common
{
    /// <summary>
    /// File操作
    /// </summary>
    public static class FileExtend
    {
        /* 
         * MemoryStream 不能释放，不然读取gif图片时会发生错误，原因未知。 
         * 不过GC回收机制对 MemoryStream 这种托管代码管理的还行，释不释放都还OK 
         */


        #region ReadAllBytes 方式读取图片 （推荐。可以读取任何图片文件 代码量少）
        /// <summary>
        /// ReadAllBytes 方式读取图片 （推荐。可以读取任何图片文件）
        /// </summary>
        /// <param name="fileAbsolutePath"></param>
        /// <returns></returns>
        public static Image GetImageFromReadAllBytes(string fileAbsolutePath)
        {
            if (File.Exists(fileAbsolutePath))
            {
                return Image.FromStream(new MemoryStream(File.ReadAllBytes(fileAbsolutePath)));
            }
            return null;
        }
        #endregion

        #region 通过FileStream 来打开文件，这样就可以实现不锁定Image文件，到时可以让多用户同时访问Image文件
        /// <summary>
        /// 通过FileStream 来打开文件，这样就可以实现不锁定Image文件，到时可以让多用户同时访问Image文件
        /// </summary>
        /// <param name="fileAbsolutePath"></param>
        /// <returns></returns>
        public static System.Drawing.Image ReadImageFileFromFileStream(string fileAbsolutePath)
        {
            using (FileStream fs = File.OpenRead(fileAbsolutePath))
            {
                int fileLength = 0;
                fileLength = (int)fs.Length; //获得文件长度 
                Byte[] image = new Byte[fileLength]; //建立一个字节数组 
                fs.Read(image, 0, fileLength); //按字节流读取 
                MemoryStream ms = new MemoryStream(image);  // ====== 加了这段就可以读取 gif 图片 =======
                return Image.FromStream(ms);
            }
        }
        #endregion

        #region FileStream 方式读取图片   （读取 jpg png 正常  读取其它的图片报错）
        /// <summary>
        /// FileStream 方式读取图片   （读取jpg png 正常  读取其它的图片报错）
        /// </summary>
        /// <param name="fileAbsolutePath"></param>
        /// <returns></returns>
        public static Image GetImageFromFileStream(string fileAbsolutePath)
        {
            if (File.Exists(fileAbsolutePath))
            {
                using (var stream = new FileStream(fileAbsolutePath, FileMode.Open))
                {
                    return Image.FromStream(stream);
                }
            }
            return null;
        }
        #endregion

        #region 可以读取任何图片，但是 gif 不能动
        /// <summary>
        /// 可以读取任何图片，但是 gif 不能动
        /// </summary>
        /// <param name="fileAbsolutePath"></param>
        /// <returns></returns>
        public static Image GetImage(string fileAbsolutePath)
        {
            using (Image img = System.Drawing.Image.FromFile(fileAbsolutePath))
            {
                System.Drawing.Image image = new System.Drawing.Bitmap(img);
                return image;
            }
        }
        #endregion

        #region 读取文件到 byte[]
        /// <summary>
        /// 读取文件到 byte[]
        /// </summary>
        /// <param name="fileAbsolutePath"></param>
        /// <returns></returns>
        public static byte[] ReadFileToBytes(string fileAbsolutePath)
        {
            byte[] buffer = null;
            if (File.Exists(fileAbsolutePath))
            {
                using (FileStream fs = new FileStream(fileAbsolutePath, FileMode.Open))
                {
                    buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, int.Parse(fs.Length.ToString()));
                }
            }
            return buffer;
        }

        /// <summary>
        /// 读取文件到 byte[]
        /// </summary>
        /// <param name="fileAbsolutePath"></param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(string fileAbsolutePath)
        {
            if (File.Exists(fileAbsolutePath))
            {
                return File.ReadAllBytes(fileAbsolutePath);
            }
            return null;
        }
        #endregion

        #region 将image转化为二进制 byte[]
        /// <summary>
        /// 将image转化为二进制 
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] GetBytesFromImage(Image img)
        {
            byte[] bt = null;
            if (!img.Equals(null))
            {
                using (MemoryStream mostream = new MemoryStream())
                {
                    Bitmap bmp = new Bitmap(img);
                    bmp.Save(mostream, img.RawFormat);//将图像以指定的格式存入缓存内存流
                    bt = new byte[mostream.Length];
                    mostream.Position = 0;//设置留的初始位置
                    mostream.Read(bt, 0, Convert.ToInt32(bt.Length));
                }
            }
            return bt;
        }
        #endregion

        #region 读取二进制byte[]并转化为图片
        /// <summary>
        /// 读取byte[]并转化为图片
        /// </summary>
        /// <param name="bytes">byte[]</param>
        /// <returns>Image</returns>
        public static Image GetImageFromBytes(byte[] bytes)
        {
            //using (MemoryStream ms = new MemoryStream(bytes))  //用了 using 不能读取 gif 图片  原因未知
            MemoryStream ms = new MemoryStream(bytes);
            ms.Write(bytes, 0, bytes.Length);
            return Image.FromStream(ms, true);
        }
        #endregion

        #region 读取文本文件
        /// <summary>
        /// 读取文本文件
        /// </summary>
        /// <param name="fileAbsolutePath"></param>
        /// <returns></returns>
        public static string ReadTextFile(string fileAbsolutePath)
        {
            if (File.Exists(fileAbsolutePath))
            {
                return File.ReadAllText(fileAbsolutePath, Encoding.Default);
            }
            return null;
        }
        #endregion

        #region 写入文本文件 如果存在则覆盖
        /// <summary>
        /// 写入文本文件 如果存在则覆盖
        /// </summary>
        /// <param name="fileAbsolutePath"></param>
        /// <returns></returns>
        public static void WriteTextFile(string fileAbsolutePath, string contents)
        {
            if (!Directory.Exists(Path.GetDirectoryName(fileAbsolutePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileAbsolutePath));
            }
            File.WriteAllText(fileAbsolutePath, contents, Encoding.Default);
        }
        #endregion

        #region 删除指定目录文件 文件不存在不报异常
        /// <summary>
        /// 删除指定目录文件 文件不存在不报异常
        /// </summary>
        public static void DeleteFile(string fileAbsolutePath)
        {
            if (File.Exists(fileAbsolutePath))
            {
                File.Delete(fileAbsolutePath);
            }
        }
        #endregion

    }
}
