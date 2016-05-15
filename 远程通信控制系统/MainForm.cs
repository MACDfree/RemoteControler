using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 远程通信控制系统
{
    public partial class MainForm : Form
    {
        Service service = null;
        TcpClient client = null;
        string fileName = "";
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(richTextBoxSend.Text))
            {
                MessageBox.Show("请输入发送内容！");
                return;
            }
            if (GlobalVal.isLogin)
            {

                if (GlobalVal.isService)
                {
                    if (client != null)
                    {
                        setText("[服务器]：" + richTextBoxSend.Text);
                        byte[] buff = Common.convertMessageToByte(MessageStr.getTalkMessage(richTextBoxSend.Text));
                        client.GetStream().Write(buff, 0, buff.Length);
                    }
                }
                else
                {
                    Client client = TcpCommon.getClient();
                    if (client == null)
                    {
                        MessageBox.Show("请先配置启动参数！");
                        this.Close();
                        return;
                    }
                    try
                    {
                        setText("[" + GlobalVal.username + "]：" + richTextBoxSend.Text);
                        client.Connect();
                        byte[] buff = Common.convertMessageToByte(MessageStr.getTalkMessage(richTextBoxSend.Text));
                        client.stream.Write(buff, 0, buff.Length);
                    }
                    catch
                    {
                        MessageBox.Show("运行出错！");
                    }
                }
                richTextBoxSend.Text = "";
            }
            else
            {
                MessageBox.Show("请先登录！");
            }
        }

        private void ShowFormCenter(Form form, Form parentForm)
        {
            form.Location = new Point(parentForm.Location.X + parentForm.Size.Width / 2 - form.Size.Width / 2, parentForm.Location.Y + parentForm.Size.Height / 2 - form.Size.Height / 2);
            form.ShowDialog();
        }

        private void 启动项配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingForm = new SettingsForm();
            ShowFormCenter(settingForm, this);
            if (GlobalVal.isService)
            {
                登录ToolStripMenuItem.Visible = false;
                注册ToolStripMenuItem.Visible = false;
                service = TcpCommon.getService();
                if (service == null)
                {
                    MessageBox.Show("服务器启动参数错误！");
                    return;
                }
                service.Start(ListenFun);
            }
            else
            {
                登录ToolStripMenuItem.Visible = true;
                注册ToolStripMenuItem.Visible = true;
            }
        }

        private void ListenFun()
        {
            client = null;
        loop:
            try
            {
                client = service.listener.AcceptTcpClient();

                while (true)
                {
                    NetworkStream stream = client.GetStream();
                    Message message = Common.GetMessage(stream);
                    if (message.IsFile)
                    {
                        int filelength;
                        byte[] filecontent;
                        string filename;
                        MessageFile.getFileAndInfo(message.Content, out filelength, out filecontent, out filename);
                        LogUtil.GetLog().Write(GlobalVal.currentDir + "\\res\\" + filename);
                        FileStream fs = new FileStream(GlobalVal.currentDir + "\\res\\" + filename, FileMode.OpenOrCreate, FileAccess.Write);
                        fs.Write(filecontent, 0, filecontent.Length);
                        fs.Close();
                        toolStripStatusLabel.Text = filename + "上传成功！";
                    }
                    else
                    {
                        string content = Encoding.UTF8.GetString(message.Content);
                        JObject obj = JObject.Parse(content);
                        string username;
                        string password;
                        switch (obj["cmd"].ToString())
                        {
                            case "1": //Login
                                if (GlobalVal.isLogin)
                                {
                                    byte[] buff1 = Common.convertMessageToByte(MessageStr.getRetMessage(false, "已登录，请不要重复登录！"));
                                    stream.Write(buff1, 0, buff1.Length);
                                }
                                else
                                {
                                    username = obj["username"].ToString();
                                    password = obj["password"].ToString();
                                    CommonDao dao = new CommonDao();
                                    String cnt = dao.GetStr("select count(1) from userinfo where username='{0}' and password='{1}'", username, password);
                                    if (cnt == "0")
                                    {
                                        byte[] buff1 = Common.convertMessageToByte(MessageStr.getRetMessage(false, "用户未注册，请先注册！"));
                                        GlobalVal.isLogin = false;
                                        stream.Write(buff1, 0, buff1.Length);
                                        toolStripStatusLabel.Text = username + "未注册！";
                                    }
                                    else
                                    {
                                        byte[] buff1 = Common.convertMessageToByte(MessageStr.getRetMessage(true, ""));
                                        stream.Write(buff1, 0, buff1.Length);
                                        GlobalVal.isLogin = true;
                                        GlobalVal.username = username;
                                        toolStripStatusLabel.Text = username + "登录成功！";
                                    }
                                }
                                break;
                            case "2": //Logout
                                // TODO 可以先不写
                                break;
                            case "3": //Talk
                                if (GlobalVal.isLogin)
                                {
                                    setText("[" + GlobalVal.username + "]:" + obj["msg"].ToString());
                                }
                                break;
                            case "4": //Regist
                                username = obj["username"].ToString();
                                password = obj["password"].ToString();
                                CommonDao dao4 = new CommonDao();
                                String cnt4 = dao4.GetStr("select count(1) from userinfo where username='{0}'", username);
                                if (cnt4 != "0")
                                {
                                    byte[] buff4 = Common.convertMessageToByte(MessageStr.getRetMessage(false, "用户已注册！"));
                                    GlobalVal.isLogin = false;
                                    stream.Write(buff4, 0, buff4.Length);
                                    toolStripStatusLabel.Text = username + "已注册！";
                                }
                                else
                                {
                                    dao4.Execute("insert into userinfo ([userguid],[loginid],[username],[password],[serverorclient],[ipaddress]) values ('{0}','{1}','{2}','{3}','{4}','{5}')", Guid.NewGuid().ToString(), username, username, password, "1", "0");
                                    byte[] buff = Common.convertMessageToByte(MessageStr.getRetMessage(true, ""));
                                    stream.Write(buff, 0, buff.Length);
                                    toolStripStatusLabel.Text = username + "注册成功！";
                                }
                                break;
                            case "5": // 确认是否同意上传文件
                                DialogResult ret = MessageBox.Show("确认接收客户端文件：" + obj["filename"], "确认", MessageBoxButtons.OKCancel);
                                if (ret == DialogResult.OK)
                                {
                                    byte[] buff2 = Common.convertMessageToByte(MessageStr.getRetMessageWithCmd(5, true, ""));
                                    stream.Write(buff2, 0, buff2.Length);
                                }
                                else
                                {
                                    byte[] buff2 = Common.convertMessageToByte(MessageStr.getRetMessageWithCmd(5, false, ""));
                                    stream.Write(buff2, 0, buff2.Length);
                                }
                                break;
                            case "6": // 截图
                                Thread sendPic = new Thread(() =>
                                {
                                    try
                                    {
                                        while (true)
                                        {
                                            Image baseImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                                            Graphics g = Graphics.FromImage(baseImage);
                                            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
                                            g.Dispose();
                                            MemoryStream ms = new MemoryStream();
                                            baseImage.Save(ms, ImageFormat.Png);
                                            //baseImage.Save("tmp\\sc.png", ImageFormat.Png);
                                            //FileStream fileStream = new FileStream("tmp\\sc.png", FileMode.Open, FileAccess.Read);
                                            //BinaryReader read = new BinaryReader(fileStream);
                                            long length = ms.Length;
                                            //// 获取文件字节数组
                                            byte[] temp = ms.GetBuffer();
                                            //read.Close();
                                            //fileStream.Close();
                                            ms.Dispose();
                                            byte[] buff3 = Common.convertMessageToByte(MessageFile.getFileMessage("sc.png", temp));
                                            stream.Write(buff3, 0, buff3.Length);
                                            Thread.Sleep(50);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LogUtil.GetLog().Write(ex);
                                        return;
                                    }
                                });
                                sendPic.IsBackground = true;
                                sendPic.Start();
                                break;
                            case "7": // 执行命令
                                Process p = new Process();
                                p.StartInfo.FileName = "cmd.exe";
                                p.StartInfo.FileName = "cmd.exe";
                                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                                p.Start();//启动程序

                                //向cmd窗口发送输入信息
                                p.StandardInput.WriteLine(obj["content"] + "&exit");
                                p.StandardInput.AutoFlush = true;
                                string output = p.StandardOutput.ReadToEnd();
                                p.WaitForExit();//等待程序执行完退出进程
                                p.Close();
                                byte[] buff7 = Common.convertMessageToByte(MessageStr.getRetMessageWithCmd(7, true, output));
                                stream.Write(buff7, 0, buff7.Length);
                                break;
                            case "8": //MLeft1
                                int x = int.Parse(obj["x"].ToString());
                                int y = int.Parse(obj["y"].ToString());
                                WinApi.SetCursorPos(x, y);
                                WinApi.mouse_event(WinApi.MouseEventFlag.LeftDown | WinApi.MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
                                break;
                            case "10": //MRight1
                                x = int.Parse(obj["x"].ToString());
                                y = int.Parse(obj["y"].ToString());
                                WinApi.SetCursorPos(x, y);
                                WinApi.mouse_event(WinApi.MouseEventFlag.RightDown | WinApi.MouseEventFlag.RightUp, 0, 0, 0, UIntPtr.Zero);
                                break;
                            case "9": //MLeft2
                                x = int.Parse(obj["x"].ToString());
                                y = int.Parse(obj["y"].ToString());
                                WinApi.SetCursorPos(x, y);
                                WinApi.mouse_event(WinApi.MouseEventFlag.LeftDown | WinApi.MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
                                WinApi.mouse_event(WinApi.MouseEventFlag.LeftDown | WinApi.MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
                                break;
                            case "11": //MRight2
                                x = int.Parse(obj["x"].ToString());
                                y = int.Parse(obj["y"].ToString());
                                WinApi.SetCursorPos(x, y);
                                WinApi.mouse_event(WinApi.MouseEventFlag.RightDown | WinApi.MouseEventFlag.RightUp, 0, 0, 0, UIntPtr.Zero);
                                WinApi.mouse_event(WinApi.MouseEventFlag.RightDown | WinApi.MouseEventFlag.RightUp, 0, 0, 0, UIntPtr.Zero);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.GetLog().Write("服务器监听出错", ex);
                //MessageBox.Show("服务器监听出错！");
                toolStripStatusLabel.Text = "服务器监听出错！";
            }
            goto loop;
        }

        private void setText(string text)
        {
            if (this.richTextBoxMessages.InvokeRequired)
            {
                Action<string> d = setText;
                this.richTextBoxMessages.Invoke(d, new object[] { text });
            }
            else
            {
                this.richTextBoxMessages.AppendText(text + Environment.NewLine);
                this.richTextBoxMessages.ScrollToCaret();
            }
        }

        private void setImg(Image img)
        {
            if (this.pictureBox.InvokeRequired)
            {
                Action<Image> d = setImg;
                this.pictureBox.Invoke(d, img);
            }
            else
            {
                this.pictureBox.Image = img;
                this.pictureBox.Height = img.Height;
                this.pictureBox.Width = img.Width;
            }
        }

        private Image GetThumbnail(Image b, int destHeight, int destWidth)
        {
            if (destHeight == 0 || destWidth == 0)
            {
                return b;
            }
            Image imgSource = b;
            ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // 按比例缩放           
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }
            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
            // 设置画布的描绘质量         
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.Low;
            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            // 以下代码为保存图片时，设置压缩质量
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            imgSource.Dispose();
            return outBmp;
        }

        private void 登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GlobalVal.isLogin)
            {
                LoginForm loginForm = new LoginForm();
                ShowFormCenter(loginForm, this);
                if (!GlobalVal.isService && GlobalVal.isLogin)
                {
                    toolStripStatusLabel.Text = GlobalVal.username + "登录成功！";
                    Client client = TcpCommon.getClient();
                    try
                    {
                        client.Connect();
                        Thread talkThread = new Thread(() =>
                        {
                            Message message = null;
                            while (true)
                            {
                                try
                                {
                                    message = Common.GetMessage(client.stream);
                                    if (message.IsFile)
                                    {
                                        int filelength;
                                        byte[] filecontent;
                                        string filename;
                                        MessageFile.getFileAndInfo(message.Content, out filelength, out filecontent, out filename);
                                        if (filename == "sc.png")
                                        {
                                            Image img = Image.FromStream(new MemoryStream(filecontent));
                                            //setImg(GetThumbnail(img, this.pictureBox.Height, this.pictureBox.Width));
                                            setImg(img);
                                        }
                                    }
                                    else
                                    {
                                        string content = Encoding.UTF8.GetString(message.Content);
                                        JObject obj = JObject.Parse(content);
                                        if (obj["cmd"].ToString() == "3")//Talk
                                        {
                                            setText("[服务器]：" + obj["msg"].ToString());
                                        }
                                        else if (obj["cmd"].ToString() == "5")
                                        {
                                            if ((bool)obj["isSuccess"])
                                            {
                                                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                                                BinaryReader read = new BinaryReader(fileStream);
                                                long length = read.BaseStream.Length;
                                                // 获取文件字节数组
                                                byte[] temp = new byte[length];
                                                for (int i = 0; i < read.BaseStream.Length; i++)
                                                {
                                                    temp[i] = read.ReadByte();
                                                }
                                                read.Close();
                                                fileStream.Close();
                                                byte[] buff1 = Common.convertMessageToByte(MessageFile.getFileMessage(fileName.Substring(fileName.LastIndexOf('\\') + 1), temp));
                                                client.stream.Write(buff1, 0, buff1.Length);
                                                MessageBox.Show("文件已传输！");
                                            }
                                            else
                                            {
                                                MessageBox.Show("服务器拒绝了您的请求！");
                                            }
                                        }
                                        else if (obj["cmd"].ToString() == "7") // cmd
                                        {
                                            setText("指令返回值：");
                                            setText(obj["msg"].ToString());
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogUtil.GetLog().Write(ex.StackTrace);
                                    MessageBox.Show("运行出错！");
                                }
                            }
                        });
                        talkThread.IsBackground = true;
                        talkThread.Start();
                    }
                    catch (Exception ex)
                    {
                        LogUtil.GetLog().Write(ex);
                        MessageBox.Show("运行出错！");
                    }
                }
            }
        }

        private void 注册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GlobalVal.isLogin)
            {
                RegistForm registForm = new RegistForm();
                ShowFormCenter(registForm, this);
                if (!GlobalVal.isService && GlobalVal.isLogin)
                {
                    toolStripStatusLabel.Text = GlobalVal.username + "注册成功！";
                }
            }
        }

        private void buttonSendFile_Click(object sender, EventArgs e)
        {
            if (!GlobalVal.isLogin)
            {
                MessageBox.Show("请先登录！");
                return;
            }
            if (GlobalVal.isService)
            {
                return;
            }
            else
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = false;
                fileDialog.Title = "文件选择";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = fileDialog.FileName;// 获取文件路径
                    Client client = TcpCommon.getClient();
                    try
                    {
                        client.Connect();
                        JObject obj = new JObject(new JProperty("cmd", 5), new JProperty("filename", fileName.Substring(fileName.LastIndexOf('\\') + 1)));
                        byte[] buff = Common.convertMessageToByte(MessageStr.getCommMessage(obj.ToString()));
                        client.stream.Write(buff, 0, buff.Length);
                    }
                    catch (Exception ex)
                    {
                        LogUtil.GetLog().Write(ex);
                        MessageBox.Show("运行出错！");
                    }
                }
            }
        }

        private void buttonPrtSc_Click(object sender, EventArgs e)
        {
            if (!GlobalVal.isLogin)
            {
                MessageBox.Show("请先登录！");
                return;
            }
            if (GlobalVal.isService)
            {
                return;
            }
            else
            {
                Client client = TcpCommon.getClient();
                try
                {
                    client.Connect();
                    JObject obj = new JObject(new JProperty("cmd", (int)MessageStr.CmdType.sc));
                    byte[] buff = Common.convertMessageToByte(MessageStr.getCommMessage(obj.ToString()));
                    client.stream.Write(buff, 0, buff.Length);
                }
                catch (Exception ex)
                {
                    LogUtil.GetLog().Write(ex);
                    MessageBox.Show("运行出错！");
                }
            }
        }

        private void buttonCmd_Click(object sender, EventArgs e)
        {
            if (!GlobalVal.isLogin)
            {
                MessageBox.Show("请先登录！");
                return;
            }
            if (GlobalVal.isService)
            {
                return;
            }
            else
            {
                Client client = TcpCommon.getClient();
                try
                {
                    client.Connect();
                    JObject obj = new JObject(new JProperty("cmd", (int)MessageStr.CmdType.Cmd), new JProperty("content", comboBoxCmd.Text));
                    byte[] buff = Common.convertMessageToByte(MessageStr.getCommMessage(obj.ToString()));
                    client.stream.Write(buff, 0, buff.Length);
                }
                catch (Exception ex)
                {
                    LogUtil.GetLog().Write(ex);
                    MessageBox.Show("运行出错！");
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            comboBoxCmd.Items.Add("shutdown");
        }

        /// <summary>
        /// 鼠标单击处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (!GlobalVal.isLogin)
            {
                return;
            }
            if (GlobalVal.isService)
            {
                return;
            }
            else
            {
                Client client = TcpCommon.getClient();
                try
                {
                    client.Connect();
                    if (e.Button == MouseButtons.Left)
                    {
                        JObject obj = new JObject(new JProperty("cmd", (int)MessageStr.CmdType.MLeft1), new JProperty("x", e.X), new JProperty("y", e.Y));
                        byte[] buff = Common.convertMessageToByte(MessageStr.getCommMessage(obj.ToString()));
                        client.stream.Write(buff, 0, buff.Length);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        JObject obj = new JObject(new JProperty("cmd", (int)MessageStr.CmdType.MRight1), new JProperty("x", e.X), new JProperty("y", e.Y));
                        byte[] buff = Common.convertMessageToByte(MessageStr.getCommMessage(obj.ToString()));
                        client.stream.Write(buff, 0, buff.Length);
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.GetLog().Write(ex);
                    MessageBox.Show("运行出错！");
                }
            }
        }

        private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!GlobalVal.isLogin)
            {
                return;
            }
            if (GlobalVal.isService)
            {
                return;
            }
            else
            {
                Client client = TcpCommon.getClient();
                try
                {
                    client.Connect();
                    if (e.Button == MouseButtons.Left)
                    {
                        JObject obj = new JObject(new JProperty("cmd", (int)MessageStr.CmdType.MLeft2), new JProperty("x", e.X), new JProperty("y", e.Y));
                        byte[] buff = Common.convertMessageToByte(MessageStr.getCommMessage(obj.ToString()));
                        client.stream.Write(buff, 0, buff.Length);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        JObject obj = new JObject(new JProperty("cmd", (int)MessageStr.CmdType.MRight2), new JProperty("x", e.X), new JProperty("y", e.Y));
                        byte[] buff = Common.convertMessageToByte(MessageStr.getCommMessage(obj.ToString()));
                        client.stream.Write(buff, 0, buff.Length);
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.GetLog().Write(ex);
                    MessageBox.Show("运行出错！");
                }
            }
        }

        private void pictureBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
            //if (!GlobalVal.isLogin)
            //{
            //    return;
            //}
            //if (GlobalVal.isService)
            //{
            //    return;
            //}
            //else
            //{
            //    Client client = TcpCommon.getClient();
            //    try
            //    {
            //        client.Connect();
            //        JObject obj = new JObject(new JProperty("cmd", (int)MessageStr.CmdType.K1), new JProperty("key", e.), new JProperty("y", e.Y));
            //        byte[] buff = Common.convertMessageToByte(MessageStr.getCommMessage(obj.ToString()));
            //        client.stream.Write(buff, 0, buff.Length);
            //    }
            //    catch (Exception ex)
            //    {
            //        LogUtil.GetLog().Write(ex);
            //        MessageBox.Show("运行出错！");
            //    }
            //}
        }
    }
}
