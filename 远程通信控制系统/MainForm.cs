using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 远程通信控制系统
{
    public partial class MainForm : Form
    {
        Service service = null;
        TcpClient client = null;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(richTextBoxSend.Text))
            {
                MessageBox.Show("请输入发送内容！");
                return;
            }
            if (GlobalVal.isLogin)
            {
                setText(richTextBoxSend.Text);
                if (GlobalVal.isService)
                {
                    if (client != null)
                    {
                        byte[] buff = Common.convertMessageToByte(MessageStr.getTalkMessage(richTextBoxSend.Text));
                        client.GetStream().Write(buff, 0, buff.Length);
                    }
                }
                else
                {
                    Client client = TcpCommon.getClient();
                    if (client == null)
                    {
                        MessageBox.Show("请先配置启动参数！");
                        this.Close();
                        return;
                    }
                    try
                    {
                        client.Connect();
                        byte[] buff = Common.convertMessageToByte(MessageStr.getTalkMessage(richTextBoxSend.Text));
                        client.stream.Write(buff, 0, buff.Length);
                    }
                    catch
                    {
                        MessageBox.Show("运行出错！");
                    }
                }
            }
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
                登录ToolStripMenuItem.Enabled = false;
                注册ToolStripMenuItem.Enabled = false;
                service = TcpCommon.getService();
                if (service == null)
                {
                    MessageBox.Show("服务器启动参数错误！");
                    return;
                }
                service.Start(ListenFun);
            }
            else
            {
                登录ToolStripMenuItem.Enabled = true;
                注册ToolStripMenuItem.Enabled = true;
            }
        }

        private void ListenFun()
        {
            client = null;
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
                        string username;
                        string password;
                        switch (obj["cmd"].ToString())
                        {
                            case "1": //Login
                                if (GlobalVal.isLogin)
                                {
                                    byte[] buff1 = Common.convertMessageToByte(MessageStr.getRetMessage(false, "已登录，请不要重复登录！"));
                                    stream.Write(buff1, 0, buff1.Length);
                                }
                                else
                                {
                                    username = obj["username"].ToString();
                                    password = obj["password"].ToString();
                                    // TODO 数据库操作
                                    byte[] buff1 = Common.convertMessageToByte(MessageStr.getRetMessage(true, ""));
                                    stream.Write(buff1, 0, buff1.Length);
                                    GlobalVal.isLogin = true;
                                    toolStripStatusLabel.Text = username + "登录成功！";
                                }
                                break;
                            case "2": //Logout
                                // TODO 可以先不写
                                break;
                            case "3": //Talk
                                if (GlobalVal.isLogin)
                                {
                                    setText(obj["msg"].ToString());
                                }
                                break;
                            case "4": //Regist
                                username = obj["username"].ToString();
                                password = obj["password"].ToString();
                                // TODO 增加数据库操作
                                byte[] buff = Common.convertMessageToByte(MessageStr.getRetMessage(true, ""));
                                GlobalVal.isLogin = true;
                                stream.Write(buff, 0, buff.Length);
                                toolStripStatusLabel.Text = username + "注册并登录成功！";
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.GetLog().Write("服务器监听出错", ex);
                MessageBox.Show("服务器监听出错！");
                toolStripStatusLabel.Text = "服务器监听出错！";
            }
            goto loop;
        }

        private void setText(string text)
        {
            if (this.richTextBoxMessages.InvokeRequired)
            {
                Action<string> d = setText;
                this.richTextBoxMessages.Invoke(d, new object[] { text });
            }
            else
            {
                this.richTextBoxMessages.AppendText(text + Environment.NewLine);
                this.richTextBoxMessages.ScrollToCaret();
            }
        }

        private void 登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GlobalVal.isLogin)
            {
                LoginForm loginForm = new LoginForm();
                ShowFormCenter(loginForm, this);
                if (!GlobalVal.isService && GlobalVal.isLogin)
                {
                    toolStripStatusLabel.Text = GlobalVal.username + "登录成功！";
                    Client client = TcpCommon.getClient();
                    try
                    {
                        client.Connect();
                        Thread talkThread = new Thread(() =>
                        {
                            Message message = null;
                            while (true)
                            {
                                try
                                {
                                    message = Common.GetMessage(client.stream);
                                    if(message.IsFile)
                                    {

                                    }
                                    else
                                    {
                                        string content = Encoding.UTF8.GetString(message.Content);
                                        JObject obj = JObject.Parse(content);
                                        if (obj["cmd"].ToString()=="3")//Talk
                                        {
                                            setText(obj["msg"].ToString());
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogUtil.GetLog().Write(ex.StackTrace);
                                    MessageBox.Show("运行出错！");
                                    return;
                                }
                            }
                        });
                        talkThread.IsBackground = true;
                        talkThread.Start();
                    }
                    catch (Exception ex)
                    {
                        LogUtil.GetLog().Write(ex);
                        MessageBox.Show("运行出错！");
                    }
                }
            }
        }

        private void 注册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GlobalVal.isLogin)
            {
                RegistForm registForm = new RegistForm();
                ShowFormCenter(registForm, this);
                if (!GlobalVal.isService && GlobalVal.isLogin)
                {
                    toolStripStatusLabel.Text = GlobalVal.username + "注册并登录成功！";
                    Client client = TcpCommon.getClient();
                    try
                    {
                        client.Connect();
                        Thread talkThread = new Thread(() =>
                        {
                            Message message = null;
                            while (true)
                            {
                                try
                                {
                                    message = Common.GetMessage(client.stream);
                                    if (message.IsFile)
                                    {

                                    }
                                    else
                                    {
                                        string content = Encoding.UTF8.GetString(message.Content);
                                        JObject obj = JObject.Parse(content);
                                        switch(obj["cmd"].ToString())
                                        {
                                            case "3":// Talk
                                                setText(obj["msg"].ToString());
                                                break;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogUtil.GetLog().Write(ex.StackTrace);
                                    MessageBox.Show("运行出错！");
                                    return;
                                }
                            }
                        });
                        talkThread.IsBackground = true;
                        talkThread.Start();
                    }
                    catch (Exception ex)
                    {
                        LogUtil.GetLog().Write(ex);
                        MessageBox.Show("运行出错！");
                    }
                }
            }
        }

        private void buttonSendFile_Click(object sender, EventArgs e)
        {
            if(GlobalVal.isService)
            {

            }
            else
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = false;
                fileDialog.Title = "文件选择";
                if(fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = fileDialog.FileName;// 获取文件路径
                    MessageBox.Show(fileName);
                }
            }
        }
    }
}
