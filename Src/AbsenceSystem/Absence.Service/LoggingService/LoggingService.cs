using System;
using System.Runtime.CompilerServices;

namespace Absence.Service.LoggingService
{

    /// <summary>
    /// The service responsible for writing logfiles
    /// </summary>
    public abstract class LoggingService
    {
        #region Static prop's.

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _logPrefix;

        #endregion

        #region Ctor 

        public LoggingService()
        {
            _logPrefix = this.GetType().UnderlyingSystemType.Name + " - ";
        }

        #endregion

        /// <summary>
        /// Logs an information message with log prefix
        /// </summary>
        /// <param name="message">Message</param>
        protected void LogInformation(string message)
        {
            _log.Info(_logPrefix + message);

        }

        /// <summary>
        /// Logs a warning message with log prefix
        /// </summary>
        /// <param name="message">Message</param>
        protected void LogWarning(string message)
        {
            _log.Warn(_logPrefix + message);

        }

        /// <summary>
        /// Logs a error message with log prefix and exception
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        protected void LogError(string message, Exception exception)
        {
            _log.Error(_logPrefix + message, exception);
        }

        /// <summary>
        /// Gets method name on async methods instead of movenext
        /// </summary>
        /// <param name="name">Method name</param>
        /// <returns>Method name</returns>
        protected static string GetActualAsyncMethodName([CallerMemberName] string name = null) => name;
    }

}
