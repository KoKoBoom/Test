/********************************************************************************
** Auth：	Taki
** Mail:	mister_zheng@sina.com
** Date：	2017/1/11 10:19:10
** Desc：	日志基类工厂  程序内部使用
*********************************************************************************/
using System;

namespace Taki.Logging
{
    /// <summary>
    /// 本类库已经对于本类库中所有可能出现的异常做出了处理， LoggerFactory.Create() 在一般情况下不可能出现异常。
    /// 如果还不放心，建议使用C# 6.0 语法 即 LoggerFactory.Create() ?. Error(ex); 绝对保证本条代码的安全性，绝不会出任何异常。
    /// </summary>
    public class LoggerFactory
    {
        #region Fields

        private static ILoggerFactory _currentLogFactory;

        #endregion

        #region Public Methods

        /// <summary>
        ///   Set the  log factory to use
        /// </summary>
        /// <param name="logFactory"> Log factory to use </param>
        public static void SetCurrent(ILoggerFactory logFactory)
        {
            _currentLogFactory = logFactory;
        }

        /// <summary>
        ///  创建指定配置文件指定的继承了 ILogger 的实例
        ///  默认创建 Log4net 的实例
        ///  本类库已经对于本类库中所有可能出现的异常做出了处理， LoggerFactory.Create() 在一般情况下不可能出现异常。
        ///  如果还不放心，建议使用C# 6.0 语法 即 LoggerFactory.Create() ?. Error(ex); 绝对保证本条代码的安全性，绝不会出任何异常。
        /// </summary>
        /// <returns> Created ILog </returns>
        public static ILogger Create()
        {
            try
            {
                if (_currentLogFactory == null) { DefaultConfig(); }
                return _currentLogFactory.Create();
            }
            catch (Exception) { }
            return new Logger4Helper(); //按照可扩展规则，本来这里不能出现特定的实例，但是为了解决 LoggerFactory.Create()?.Error(ex); 当 LoggerFactory.Create() 为空时，那就成了null.Error(ex) 会报异常，所以这里就默认返回Log4net的实例。当然LoggerFactory.Create()?.Error(ex);可以解决问题，当时你不能保证每个人都这么写。而且 ?. 必须4.5以上版本 C#6.0 的语法
        }

        /// <summary>
        /// 默认创建 Log4net 的实例
        /// </summary>
        static void DefaultConfig()
        {
            var strDefaultLogConfig = "Taki.Logging.Logger4Factory";
            var defColl = System.Configuration.ConfigurationManager.AppSettings["DefaultLogConfig"];
            if (defColl != null && !string.IsNullOrWhiteSpace(defColl.ToString()))
            {
                strDefaultLogConfig = defColl.ToString();
            }
            (System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(strDefaultLogConfig) as ILoggerFactory).DefaultFactory();
        }

        #endregion
    }
}
