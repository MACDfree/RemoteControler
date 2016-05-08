using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 远程通信控制系统
{
    public partial class MainForm : Form
    {
        Service service = null;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonSend_Click(object sender, EventArgs e)
        {

        }

        private void ShowFormCenter(Form form, Form parentForm)
        {
            form.Location = new Point(parentForm.Location.X + parentForm.Size.Width / 2 - form.Size.Width / 2, parentForm.Location.Y + parentForm.Size.Height / 2 - form.Size.Height / 2);
            form.ShowDialog();
        }

        private void 启动项配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingForm = new SettingsForm();
            ShowFormCenter(settingForm, this);
            if (GlobalVal.isService)
            {
                service = TcpCommon.getService();
                if (service == null)
                {
                    MessageBox.Show("服务器启动参数错误！");
                    return;
                }
                service.Start(ListenFun);
            }
        }

        private void ListenFun()
        {
            TcpClient client = null;
        loop:
            try
            {
                client = service.listener.AcceptTcpClient();

                while (true)
                {
                    NetworkStream stream = client.GetStream();
                    Message message = Common.GetMessage(stream);
                    if (message.IsFile)
                    {

                    }
                    else
                    {
                        string content = Encoding.UTF8.GetString(message.Content);
                        JObject obj = JObject.Parse(content);
                        switch (obj["cmd"].ToString())
                        {
                            case "1": //Login
                                break;
                            case "2": //Logout
                                break;
                            case "3": //Talk
                                break;
                            case "4": //Regist
                                string username = obj["username"].ToString();
                                string password = obj["password"].ToString();

                                byte[] buff = Common.convertMessageToByte(MessageStr.getRetMessage(true, ""));
                                stream.Write(buff, 0, buff.Length);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.GetLog().Write("服务器监听出错", ex);
                MessageBox.Show("服务器监听出错！");
            }
            goto loop;
        }

        private void 登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            ShowFormCenter(loginForm, this);
        }

        private void 注册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistForm registForm = new RegistForm();
            ShowFormCenter(registForm, this);
        }
    }
}
