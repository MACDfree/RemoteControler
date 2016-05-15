using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace 远程通信控制系统
{
    /// <summary>
    /// 操作ini文件工具类
    /// </summary>
    static class IniUtil
    {
        // 默认为程序的当前路径
        private static string filepath = Application.StartupPath+"/config.ini";

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);

        /// <summary>
        /// 向指定ini文件写入配置
        /// </summary>
        /// <param name="filepath">ini文件路径</param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Write(string filepath, string section, string key, object value)
        {
            WritePrivateProfileString(section, key, value.ToString(), filepath);
        }

        public static string Read(string filepath, string section, string key)
        {
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", sb, 255, filepath);
            return sb.ToString();
        }

        public static void Write(string section, string key, object value)
        {
            Write(filepath, section, key, value);
        }

        public static string Read(string section, string key)
        {
            return Read(filepath, section, key);
        }

        public static int ReadInt(string section, string key)
        {
            int ret;
            return int.TryParse(Read(section, key), out ret) ? ret : 0;
        }

        public static bool ReadBool(string section, string key)
        {
            bool ret;
            return Boolean.TryParse(Read(section, key), out ret) ? ret : false;
        }
    }
}
