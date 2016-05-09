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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.richTextBoxMessages = new System.Windows.Forms.RichTextBox();
            this.richTextBoxSend = new System.Windows.Forms.RichTextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonSendFile = new System.Windows.Forms.Button();
            this.buttonPrtSc = new System.Windows.Forms.Button();
            this.buttonMB = new System.Windows.Forms.Button();
            this.menuStripTool = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.启动项配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.注册ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.menuStripTool.SuspendLayout();
            this.SuspendLayout();
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
            this.toolStripStatusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel.Text = "状态";
            // 
            // richTextBoxMessages
            // 
            this.richTextBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxMessages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxMessages.Location = new System.Drawing.Point(0, 28);
            this.richTextBoxMessages.Name = "richTextBoxMessages";
            this.richTextBoxMessages.Size = new System.Drawing.Size(449, 228);
            this.richTextBoxMessages.TabIndex = 2;
            this.richTextBoxMessages.Text = "";
            // 
            // richTextBoxSend
            // 
            this.richTextBoxSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxSend.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxSend.Location = new System.Drawing.Point(0, 262);
            this.richTextBoxSend.Name = "richTextBoxSend";
            this.richTextBoxSend.Size = new System.Drawing.Size(372, 79);
            this.richTextBoxSend.TabIndex = 3;
            this.richTextBoxSend.Text = "";
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            this.buttonSendFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSendFile.Location = new System.Drawing.Point(12, 347);
            this.buttonSendFile.Name = "buttonSendFile";
            this.buttonSendFile.Size = new System.Drawing.Size(134, 23);
            this.buttonSendFile.TabIndex = 5;
            this.buttonSendFile.Text = "发送文件";
            this.buttonSendFile.UseVisualStyleBackColor = true;
            // 
            // buttonPrtSc
            // 
            this.buttonPrtSc.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonPrtSc.Location = new System.Drawing.Point(152, 347);
            this.buttonPrtSc.Name = "buttonPrtSc";
            this.buttonPrtSc.Size = new System.Drawing.Size(136, 23);
            this.buttonPrtSc.TabIndex = 5;
            this.buttonPrtSc.Text = "截图";
            this.buttonPrtSc.UseVisualStyleBackColor = true;
            // 
            // buttonMB
            // 
            this.buttonMB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMB.Location = new System.Drawing.Point(294, 347);
            this.buttonMB.Name = "buttonMB";
            this.buttonMB.Size = new System.Drawing.Size(143, 23);
            this.buttonMB.TabIndex = 5;
            this.buttonMB.Text = "鼠标/键盘";
            this.buttonMB.UseVisualStyleBackColor = true;
            // 
            // menuStripTool
            // 
            this.menuStripTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem});
            this.menuStripTool.Location = new System.Drawing.Point(0, 0);
            this.menuStripTool.Name = "menuStripTool";
            this.menuStripTool.Size = new System.Drawing.Size(449, 25);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 406);
            this.Controls.Add(this.buttonMB);
            this.Controls.Add(this.buttonPrtSc);
            this.Controls.Add(this.buttonSendFile);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.richTextBoxSend);
            this.Controls.Add(this.richTextBoxMessages);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStripTool);
            this.MainMenuStrip = this.menuStripTool;
            this.Name = "MainForm";
            this.Text = "远程通信控制系统";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStripTool.ResumeLayout(false);
            this.menuStripTool.PerformLayout();
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
        private System.Windows.Forms.Button buttonSendFile;
        private System.Windows.Forms.Button buttonPrtSc;
        private System.Windows.Forms.Button buttonMB;
        private System.Windows.Forms.MenuStrip menuStripTool;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 启动项配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 登录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 注册ToolStripMenuItem;
    }
}

