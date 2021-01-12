using log4net;
using System;
using System.Diagnostics;
using System.IO;
//https://blog.csdn.net/glmushroom/article/details/110531336

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", ConfigFileExtension = "config", Watch = true)]
namespace Atlas_WebAPI_V03x64.Controllers
{
    public class Logger
    {
        //public static readonly ILog log = LogManager.GetLogger(typeof(Logger));

        private static readonly ILog _log = LogManager.GetLogger(typeof(Logger));
        public static void Error(string msg) => _log.Error(AppendClassLine(msg));
        public static void Error(Exception ex, string msg = null) => _log.Error(msg, ex);

        public static void Info(string msg) => _log.Info(AppendClassLine(msg));
        public static void Info(Exception ex, string msg = null) => _log.Info(msg, ex);

        public static void Debug(string msg) => _log.Debug(AppendClassLine(msg));
        public static void Debug(Exception ex, string msg = null) => _log.Debug(msg, ex);

        public static void Warn(string msg) => _log.Warn(AppendClassLine(msg));
        public static void Warn(Exception ex, string msg = null) => _log.Warn(msg, ex);

        public static void Fatal(string msg) => _log.Warn(AppendClassLine(msg));
        public static void Fatal(Exception ex, string msg = null) => _log.Fatal(msg, ex);

        static string AppendClassLine(string msg)
        {
            string logStr = msg;
            try
            {
                //StackTrace st = new StackTrace(true);
                //int line = st.GetFrame(0).GetFileLineNumber();
                //string filename = st.GetFrame(0).GetFileName();
                //logStr = $" [{msg} {st} ]";

                logStr = $" [{msg}]";

                ////原始
                //StackTrace st = new StackTrace(true);
                //StackFrame sf = st.GetFrame(2);
                //logStr = $" [{msg} {Path.GetFileName(sf.GetFileName())}:{sf.GetFileLineNumber().ToString()}]";
            }
            catch { }
            return logStr;
        }
    }
}
