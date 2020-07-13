using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PaymentGateway
{
    public static class Logs
    {
        private static log4net.ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Finds the calling method name by working fown the stacktrace
        /// </summary>
        /// <returns>The name of the calling method</returns>
        private static string GetMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame;
            MethodBase stackFrameMethod;
            int frameCount = 0;
            string typeName;

            do
            {
                frameCount++;
                stackFrame = stackTrace.GetFrame(frameCount);
                stackFrameMethod = stackFrame.GetMethod();
                typeName = stackFrameMethod.ReflectedType.FullName;
            }
            while (typeName.EndsWith("logs"));

            return typeName + "." + stackFrameMethod.Name;
        }

        /// <summary>
        /// Logs an Error message and and exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Error(string message, Exception ex)
        {
            _log.Error(GetMethodName() + ": " + message, ex);
        }

        /// <summary>
        /// Logs an Info message
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            _log.Info(GetMethodName() + ": " + message);
        }
    }
}
