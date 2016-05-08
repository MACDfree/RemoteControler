using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 远程通信控制系统
{
    public partial class SettingsForm : Form
    {
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
            if (String.IsNullOrWhiteSpace(comboBoxServiceIP.Text))
            {
                MessageBox.Show("服务器IP必填！");
                return;
            }
            GlobalVal.serviceIP = comboBoxServiceIP.Text;
            GlobalVal.clientIP = comboBoxClientIP.Text;
            GlobalVal.isService = radioButtonService.Checked;
            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // 加载本机IP地址
            comboBoxServiceIP.Items.AddRange(Dns.GetHostAddresses(Dns.GetHostName()));
            comboBoxClientIP.Items.AddRange(Dns.GetHostAddresses(Dns.GetHostName()));
            GlobalVal.servicePort = IniUtil.ReadInt("common", "serviceport");
            if (!String.IsNullOrEmpty(GlobalVal.serviceIP))
            {
                comboBoxServiceIP.Text = GlobalVal.serviceIP;
            }
            if(!String.IsNullOrEmpty(GlobalVal.clientIP))
            {
                comboBoxClientIP.Text = GlobalVal.clientIP;
            }
            radioButtonService.Checked = GlobalVal.isService;
        }

        private void radioButtonClient_CheckedChanged(object sender, EventArgs e)
        {
            linkLabel1.Visible = radioButtonClient.Checked;
            comboBoxClientIP.Enabled = radioButtonClient.Checked;
        }
    }
}
