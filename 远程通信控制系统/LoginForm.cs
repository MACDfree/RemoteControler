using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 远程通信控制系统
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (GlobalVal.isService)
            {
                MessageBox.Show("服务器端不需要登录！");
                this.Close();
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Client client = TcpCommon.getClient();
            if (client == null)
            {
                MessageBox.Show("请先配置启动参数！");
                this.Close();
                return;
            }

            #region 输入项检测
            if (String.IsNullOrWhiteSpace(textBoxUsername.Text) ||
                String.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                MessageBox.Show("文本框必填！");
                return;
            }
            #endregion

            try
            {
                client.Connect();
                byte[] buff = Common.convertMessageToByte(MessageStr.getLoginMessage(textBoxUsername.Text, textBoxPassword.Text));
                client.stream.Write(buff, 0, buff.Length);
                Thread loginThread = new Thread(() =>
                {
                    DateTime begin = DateTime.Now;
                    Message message = null;
                    // 超时10秒
                    while ((DateTime.Now - begin).TotalSeconds <= 10)
                    {
                        try
                        {
                            message = Common.GetMessage(client.stream);
                        }
                        catch (Exception ex)
                        {
                            LogUtil.GetLog().Write(ex.StackTrace);
                            MessageBox.Show("运行出错！");
                            return;
                        }
                        if (!message.IsFile)
                        {
                            string content = Encoding.UTF8.GetString(message.Content);
                            JObject obj = JObject.Parse(content);
                            if ((bool)obj["isSuccess"])
                            {
                                MessageBox.Show("登陆成功！");
                                GlobalVal.username = textBoxUsername.Text;
                                GlobalVal.isLogin = true;
                                return;
                            }
                            else
                            {
                                MessageBox.Show(obj["msg"].ToString());
                                return;
                            }
                        }
                    }
                    MessageBox.Show("超时出错！");

                });
                loginThread.IsBackground = true;
                loginThread.Start();
            }
            catch
            {
                MessageBox.Show("运行出错！");
            }
        }
    }
}
