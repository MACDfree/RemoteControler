﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 远程通信控制系统
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            GlobalVal.currentDir = Application.StartupPath;
            if(!Directory.Exists(GlobalVal.currentDir+"\\tmp"))
            {
                Directory.CreateDirectory(GlobalVal.currentDir + "\\tmp");
            }
            if (!Directory.Exists(GlobalVal.currentDir + "\\res"))
            {
                Directory.CreateDirectory(GlobalVal.currentDir + "\\res");
            }
            if (!File.Exists(Application.StartupPath + "/config.ini"))
            {
                IniUtil.Write("common", "serviceport", "1314");
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
