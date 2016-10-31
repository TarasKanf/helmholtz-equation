using System.Diagnostics;
using NLog;

namespace SocialNetwork.Common
{
    public class Logger
    {
        private readonly NLog.Logger logger;

        /// <summary>
        ///     Creates a logger with the name of class which calls this constructor
        /// </summary>
        public Logger()
        {
            var stackTrace = new StackTrace(); // get call stack
            var stackFrames = stackTrace.GetFrames(); // get method calls (frames)
            var callingFrame = stackFrames?[1];
            var method = callingFrame?.GetMethod();
            logger = LogManager.GetLogger(method?.DeclaringType?.Name);
        }

        /// <summary>
        ///     Creates a logger with specified name
        /// </summary>
        public Logger(string className)
        {
            logger = LogManager.GetLogger(className);
        }

        public void Trace(string traceText)
        {
            logger.Trace(traceText);
        }

        /// <summary>
        ///     Writes the diagnostic message at the Debug level
        /// </summary>
        /// <param name="debugText"></param>
        public void Debug(string debugText)
        {
            logger.Debug(debugText);
        }

        /// <summary>
        ///     Writes the diagnostic message at the Info level
        /// </summary>
        /// <param name="infoText"></param>
        public void Info(string infoText)
        {
            logger.Info(infoText);
        }

        /// <summary>
        ///     Writes the diagnostic message at the Warn level
        /// </summary>
        /// <param name="warnText"></param>
        public void Warn(string warnText)
        {
            logger.Warn(warnText);
        }

        /// <summary>
        ///     Writes the diagnostic message at the Error level
        /// </summary>
        /// <param name="errorText"></param>
        public void Error(string errorText)
        {
            logger.Error(errorText);
        }

        /// <summary>
        ///     Writes the diagnostic message at the Fatal level
        /// </summary>
        /// <param name="fatalErrorText"></param>
        public void Fatal(string fatalErrorText)
        {
            logger.Fatal(fatalErrorText);
        }
    }
}