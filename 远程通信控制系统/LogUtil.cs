using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 远程通信控制系统
{
    public sealed class LogUtil
    {
        // 单例模式（饿汉模式）
        private static readonly LogUtil instance = new LogUtil();
        private LogUtil() { }
        private string path = Application.StartupPath+@"/message.log";

        public static LogUtil GetLog()
        {
            return instance;
        }

        public void Write(string filepath, string log)
        {
            using (StreamWriter sw = new StreamWriter(filepath, true, Encoding.UTF8))
            {
                sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + log);
            }
        }

        public void Write(string log)
        {
            Write(path, log);
        }

        public void Write(Exception e)
        {
            Write(path, e.Message);
        }
    }
}
