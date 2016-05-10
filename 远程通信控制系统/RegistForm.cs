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
    public partial class RegistForm : Form
    {
        public RegistForm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRegist_Click(object sender, EventArgs e)
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
                String.IsNullOrWhiteSpace(textBoxPassword.Text) ||
                String.IsNullOrWhiteSpace(textBoxConfirm.Text))
            {
                MessageBox.Show("文本框必填！");
                return;
            }
            if (textBoxPassword.Text != textBoxConfirm.Text)
            {
                MessageBox.Show("密码不一致，请修改！");
                return;
            }
            #endregion

            try
            {
                client.Connect();
                byte[] buff = Common.convertMessageToByte(MessageStr.getRegistMessage(textBoxUsername.Text, textBoxPassword.Text));
                client.stream.Write(buff, 0, buff.Length);
                Thread registThread = new Thread(() =>
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
                                MessageBox.Show("注册成功！");
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
                registThread.IsBackground = true;
                registThread.Start();
            }
            catch
            {
                MessageBox.Show("运行出错！");
            }
        }

        private void RegistForm_Load(object sender, EventArgs e)
        {
            if (GlobalVal.isService)
            {
                MessageBox.Show("服务器端不需要注册！");
                this.Close();
            }
        }
    }
}
