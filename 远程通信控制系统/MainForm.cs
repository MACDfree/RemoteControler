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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SettingsForm settingForm = new SettingsForm();
            ShowFormCenter(settingForm, this);
        }

        private void 用户登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            ShowFormCenter(loginForm, this);
        }

        private void 用户注册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistForm registForm = new RegistForm();
            ShowFormCenter(registForm, this);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {

        }

        private void ShowFormCenter(Form form, Form parentForm)
        {
            form.Location = new Point(parentForm.Location.X + parentForm.Size.Width / 2 - form.Size.Width / 2, parentForm.Location.Y + parentForm.Size.Height / 2 - form.Size.Height / 2);
            form.ShowDialog();
        }
    }
}
