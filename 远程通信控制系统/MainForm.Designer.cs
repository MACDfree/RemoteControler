namespace 远程通信控制系统
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.richTextBoxMessages = new System.Windows.Forms.RichTextBox();
            this.richTextBoxSend = new System.Windows.Forms.RichTextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.menuStripTool = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.启动项配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.注册ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开启键盘监听ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开始截图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.发送文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.buttonCmd = new System.Windows.Forms.Button();
            this.comboBoxCmd = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.textBoxCmdRet = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageSc = new System.Windows.Forms.TabPage();
            this.statusStrip.SuspendLayout();
            this.menuStripTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.tabPageSc.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 588);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip.Size = new System.Drawing.Size(975, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "底部状态栏";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel.Text = "状态";
            // 
            // richTextBoxMessages
            // 
            this.richTextBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxMessages.BackColor = System.Drawing.Color.White;
            this.richTextBoxMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxMessages.Location = new System.Drawing.Point(0, 3);
            this.richTextBoxMessages.Name = "richTextBoxMessages";
            this.richTextBoxMessages.ReadOnly = true;
            this.richTextBoxMessages.Size = new System.Drawing.Size(590, 446);
            this.richTextBoxMessages.TabIndex = 2;
            this.richTextBoxMessages.Text = "";
            // 
            // richTextBoxSend
            // 
            this.richTextBoxSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxSend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxSend.Location = new System.Drawing.Point(0, 455);
            this.richTextBoxSend.Name = "richTextBoxSend";
            this.richTextBoxSend.Size = new System.Drawing.Size(505, 72);
            this.richTextBoxSend.TabIndex = 3;
            this.richTextBoxSend.Text = "";
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSend.Location = new System.Drawing.Point(511, 455);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(79, 72);
            this.buttonSend.TabIndex = 4;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // menuStripTool
            // 
            this.menuStripTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.开启键盘监听ToolStripMenuItem,
            this.开始截图ToolStripMenuItem,
            this.发送文件ToolStripMenuItem});
            this.menuStripTool.Location = new System.Drawing.Point(0, 0);
            this.menuStripTool.Name = "menuStripTool";
            this.menuStripTool.Size = new System.Drawing.Size(975, 25);
            this.menuStripTool.TabIndex = 6;
            this.menuStripTool.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.启动项配置ToolStripMenuItem,
            this.登录ToolStripMenuItem,
            this.注册ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 启动项配置ToolStripMenuItem
            // 
            this.启动项配置ToolStripMenuItem.Name = "启动项配置ToolStripMenuItem";
            this.启动项配置ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.启动项配置ToolStripMenuItem.Text = "启动项配置";
            this.启动项配置ToolStripMenuItem.Click += new System.EventHandler(this.启动项配置ToolStripMenuItem_Click);
            // 
            // 登录ToolStripMenuItem
            // 
            this.登录ToolStripMenuItem.Name = "登录ToolStripMenuItem";
            this.登录ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.登录ToolStripMenuItem.Text = "登录";
            this.登录ToolStripMenuItem.Click += new System.EventHandler(this.登录ToolStripMenuItem_Click);
            // 
            // 注册ToolStripMenuItem
            // 
            this.注册ToolStripMenuItem.Name = "注册ToolStripMenuItem";
            this.注册ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.注册ToolStripMenuItem.Text = "注册";
            this.注册ToolStripMenuItem.Click += new System.EventHandler(this.注册ToolStripMenuItem_Click);
            // 
            // 开启键盘监听ToolStripMenuItem
            // 
            this.开启键盘监听ToolStripMenuItem.Name = "开启键盘监听ToolStripMenuItem";
            this.开启键盘监听ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.开启键盘监听ToolStripMenuItem.Text = "开启键盘监听";
            this.开启键盘监听ToolStripMenuItem.Click += new System.EventHandler(this.开启键盘监听ToolStripMenuItem_Click);
            // 
            // 开始截图ToolStripMenuItem
            // 
            this.开始截图ToolStripMenuItem.Name = "开始截图ToolStripMenuItem";
            this.开始截图ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.开始截图ToolStripMenuItem.Text = "开始截图";
            this.开始截图ToolStripMenuItem.Click += new System.EventHandler(this.开始截图ToolStripMenuItem_Click);
            // 
            // 发送文件ToolStripMenuItem
            // 
            this.发送文件ToolStripMenuItem.Name = "发送文件ToolStripMenuItem";
            this.发送文件ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.发送文件ToolStripMenuItem.Text = "发送文件";
            this.发送文件ToolStripMenuItem.Click += new System.EventHandler(this.发送文件ToolStripMenuItem_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(3, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(490, 299);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 7;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
            this.pictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDoubleClick);
            // 
            // buttonCmd
            // 
            this.buttonCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCmd.Location = new System.Drawing.Point(884, 7);
            this.buttonCmd.Name = "buttonCmd";
            this.buttonCmd.Size = new System.Drawing.Size(75, 23);
            this.buttonCmd.TabIndex = 8;
            this.buttonCmd.Text = "执行命令";
            this.buttonCmd.UseVisualStyleBackColor = true;
            this.buttonCmd.Click += new System.EventHandler(this.buttonCmd_Click);
            // 
            // comboBoxCmd
            // 
            this.comboBoxCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCmd.FormattingEnabled = true;
            this.comboBoxCmd.Location = new System.Drawing.Point(671, 9);
            this.comboBoxCmd.Name = "comboBoxCmd";
            this.comboBoxCmd.Size = new System.Drawing.Size(207, 20);
            this.comboBoxCmd.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(967, 534);
            this.panel1.TabIndex = 11;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageMain);
            this.tabControlMain.Controls.Add(this.tabPageSc);
            this.tabControlMain.Location = new System.Drawing.Point(0, 29);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(975, 556);
            this.tabControlMain.TabIndex = 12;
            // 
            // tabPageMain
            // 
            this.tabPageMain.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageMain.Controls.Add(this.textBoxCmdRet);
            this.tabPageMain.Controls.Add(this.label1);
            this.tabPageMain.Controls.Add(this.richTextBoxMessages);
            this.tabPageMain.Controls.Add(this.richTextBoxSend);
            this.tabPageMain.Controls.Add(this.buttonCmd);
            this.tabPageMain.Controls.Add(this.comboBoxCmd);
            this.tabPageMain.Controls.Add(this.buttonSend);
            this.tabPageMain.Location = new System.Drawing.Point(4, 22);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(967, 530);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "主页";
            // 
            // textBoxCmdRet
            // 
            this.textBoxCmdRet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCmdRet.BackColor = System.Drawing.Color.White;
            this.textBoxCmdRet.Location = new System.Drawing.Point(596, 35);
            this.textBoxCmdRet.Multiline = true;
            this.textBoxCmdRet.Name = "textBoxCmdRet";
            this.textBoxCmdRet.ReadOnly = true;
            this.textBoxCmdRet.Size = new System.Drawing.Size(371, 495);
            this.textBoxCmdRet.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(606, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "CMD指令：";
            // 
            // tabPageSc
            // 
            this.tabPageSc.Controls.Add(this.panel1);
            this.tabPageSc.Location = new System.Drawing.Point(4, 22);
            this.tabPageSc.Name = "tabPageSc";
            this.tabPageSc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSc.Size = new System.Drawing.Size(967, 530);
            this.tabPageSc.TabIndex = 1;
            this.tabPageSc.Text = "截图";
            this.tabPageSc.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 610);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStripTool);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripTool;
            this.Name = "MainForm";
            this.Text = "远程通信控制系统";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStripTool.ResumeLayout(false);
            this.menuStripTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.tabPageMain.PerformLayout();
            this.tabPageSc.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.RichTextBox richTextBoxMessages;
        private System.Windows.Forms.RichTextBox richTextBoxSend;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.MenuStrip menuStripTool;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 启动项配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 登录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 注册ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button buttonCmd;
        private System.Windows.Forms.ComboBox comboBoxCmd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem 开启键盘监听ToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPageSc;
        private System.Windows.Forms.ToolStripMenuItem 开始截图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 发送文件ToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxCmdRet;
        private System.Windows.Forms.Label label1;
    }
}

