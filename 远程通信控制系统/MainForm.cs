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

        }

        private void 用户登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Location = new Point(this.Location.X + this.Size.Width / 2 - loginForm.Size.Width / 2, this.Location.Y + this.Size.Height / 2 - loginForm.Size.Height / 2);
            loginForm.ShowDialog();
        }

        private void 用户注册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistForm registForm = new RegistForm();
            registForm.Location = new Point(this.Location.X + this.Size.Width / 2 - registForm.Size.Width / 2, this.Location.Y + this.Size.Height / 2 - registForm.Size.Height / 2);
            registForm.ShowDialog();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {

        }
    }
}
