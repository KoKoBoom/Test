namespace Taki.Logging
{
    internal class Logger4Factory : ILoggerFactory
    {
        public ILogger Create()
        {
            return new Logger4Helper();
        }

        public static void SetConfig(string configPath = "")
        {
            if (string.IsNullOrWhiteSpace(configPath))
            {
                log4net.Config.XmlConfigurator.Configure();
            }
            else
            {
                using (var fs = System.IO.File.OpenRead(configPath))
                {
                    log4net.Config.XmlConfigurator.Configure(fs);
                }
            }
        }


        public void DefaultFactory()
        {
            var path = System.AppDomain.CurrentDomain.BaseDirectory + "\\bin\\Extend\\log4net\\log4net.config"; //Web网站
            if (!System.IO.File.Exists(path))
            {
                path = System.Windows.Forms.Application.StartupPath + "\\Extend\\log4net\\log4net.config";//WinForm程序
            }
            SetConfig(path);
            LoggerFactory.SetCurrent(new Logger4Factory());
        }
    }
}
