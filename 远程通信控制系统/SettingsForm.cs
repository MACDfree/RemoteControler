using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 远程通信控制系统
{
    public partial class SettingsForm : Form
    {
        Config config = null;
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(this, "测试成功！");
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            String[] service = this.textBoxServiceIP.Text.Split(new char[] { ':' });
            String[] client = this.textBoxClientIP.Text.Split(new char[] { ':' });
            config.ClientIp = client[0].Trim();
            config.ClientPort = client[1].Trim();
            config.ServiceIp = service[0].Trim();
            config.ServicePort = service[1].Trim();
            config.IsClient = this.radioButtonClient.Checked;
            ConfigUtil.update(config);
            MessageBox.Show(this, "配置成功！");
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // 读取配置文件中的配置项
            config = ConfigUtil.getConfig();

            if(!String.IsNullOrEmpty(config.ClientIp))
            {
                this.textBoxClientIP.Text = config.ClientIp + ":" + config.ClientPort;
                this.textBoxServiceIP.Text = config.ServiceIp + ":" + config.ServicePort;
                this.radioButtonClient.Checked = config.IsClient;
                this.radioButtonService.Checked = !config.IsClient;
            }
        }
    }
}
