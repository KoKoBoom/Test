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
