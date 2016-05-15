using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            if (!Regex.IsMatch(textBoxUsername.Text, @"^[^0-9][\S]*$"))
            {
                MessageBox.Show("用户名不能以数字开头！");
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
