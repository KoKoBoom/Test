/********************************************************************************
** Auth：	Taki
** Mail:	mister_zheng@sina.com
** Date：	2017/1/11 10:19:10
** Desc：	日志基类工厂接口
*********************************************************************************/
namespace Taki.Logging
{
    public interface ILoggerFactory
    {
        /// <summary>
        ///   Create a new ILog
        /// </summary>
        /// <returns> The ILog created </returns>
        ILogger Create();

        void DefaultFactory();
    }
}
