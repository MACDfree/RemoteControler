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
    }
}
