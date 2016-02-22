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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.菜单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户注册ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.richTextBoxMessages = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonSendFile = new System.Windows.Forms.Button();
            this.buttonPrtSc = new System.Windows.Forms.Button();
            this.buttonMB = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.菜单ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(449, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "顶部菜单";
            // 
            // 菜单ToolStripMenuItem
            // 
            this.菜单ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.用户登录ToolStripMenuItem,
            this.用户注册ToolStripMenuItem,
            this.系统设置ToolStripMenuItem});
            this.菜单ToolStripMenuItem.Name = "菜单ToolStripMenuItem";
            this.菜单ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.菜单ToolStripMenuItem.Text = "菜单";
            // 
            // 用户登录ToolStripMenuItem
            // 
            this.用户登录ToolStripMenuItem.Name = "用户登录ToolStripMenuItem";
            this.用户登录ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.用户登录ToolStripMenuItem.Text = "用户登录";
            this.用户登录ToolStripMenuItem.Click += new System.EventHandler(this.用户登录ToolStripMenuItem_Click);
            // 
            // 用户注册ToolStripMenuItem
            // 
            this.用户注册ToolStripMenuItem.Name = "用户注册ToolStripMenuItem";
            this.用户注册ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.用户注册ToolStripMenuItem.Text = "用户注册";
            this.用户注册ToolStripMenuItem.Click += new System.EventHandler(this.用户注册ToolStripMenuItem_Click);
            // 
            // 系统设置ToolStripMenuItem
            // 
            this.系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            this.系统设置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.系统设置ToolStripMenuItem.Text = "系统设置";
            this.系统设置ToolStripMenuItem.Click += new System.EventHandler(this.系统设置ToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 384);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip.Size = new System.Drawing.Size(449, 22);
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
            this.toolStripStatusLabel.Size = new System.Drawing.Size(29, 17);
            this.toolStripStatusLabel.Text = "test";
            // 
            // richTextBoxMessages
            // 
            this.richTextBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxMessages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxMessages.Location = new System.Drawing.Point(0, 28);
            this.richTextBoxMessages.Name = "richTextBoxMessages";
            this.richTextBoxMessages.Size = new System.Drawing.Size(449, 228);
            this.richTextBoxMessages.TabIndex = 2;
            this.richTextBoxMessages.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Location = new System.Drawing.Point(0, 262);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(372, 79);
            this.richTextBox2.TabIndex = 3;
            this.richTextBox2.Text = "";
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Location = new System.Drawing.Point(372, 262);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(77, 79);
            this.buttonSend.TabIndex = 4;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonSendFile
            // 
            this.buttonSendFile.Location = new System.Drawing.Point(12, 347);
            this.buttonSendFile.Name = "buttonSendFile";
            this.buttonSendFile.Size = new System.Drawing.Size(134, 23);
            this.buttonSendFile.TabIndex = 5;
            this.buttonSendFile.Text = "发送文件";
            this.buttonSendFile.UseVisualStyleBackColor = true;
            // 
            // buttonPrtSc
            // 
            this.buttonPrtSc.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonPrtSc.Location = new System.Drawing.Point(152, 347);
            this.buttonPrtSc.Name = "buttonPrtSc";
            this.buttonPrtSc.Size = new System.Drawing.Size(136, 23);
            this.buttonPrtSc.TabIndex = 5;
            this.buttonPrtSc.Text = "截图";
            this.buttonPrtSc.UseVisualStyleBackColor = true;
            // 
            // buttonMB
            // 
            this.buttonMB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMB.Location = new System.Drawing.Point(294, 347);
            this.buttonMB.Name = "buttonMB";
            this.buttonMB.Size = new System.Drawing.Size(143, 23);
            this.buttonMB.TabIndex = 5;
            this.buttonMB.Text = "鼠标/键盘";
            this.buttonMB.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 406);
            this.Controls.Add(this.buttonMB);
            this.Controls.Add(this.buttonPrtSc);
            this.Controls.Add(this.buttonSendFile);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBoxMessages);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "远程通信控制系统";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem 菜单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户登录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户注册ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripMenuItem 系统设置ToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBoxMessages;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonSendFile;
        private System.Windows.Forms.Button buttonPrtSc;
        private System.Windows.Forms.Button buttonMB;
    }
}

