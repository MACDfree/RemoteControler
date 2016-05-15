using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        private void MainForm_Load(object sender, EventArgs e)
        {

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
                setText(richTextBoxSend.Text);
                if (GlobalVal.isService)
                {
                    if (client != null)
                    {
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
                        client.Connect();
                        byte[] buff = Common.convertMessageToByte(MessageStr.getTalkMessage(richTextBoxSend.Text));
                        client.stream.Write(buff, 0, buff.Length);
                    }
                    catch
                    {
                        MessageBox.Show("运行出错！");
                    }
                }
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
                                    // TODO 数据库操作
                                    byte[] buff1 = Common.convertMessageToByte(MessageStr.getRetMessage(true, ""));
                                    stream.Write(buff1, 0, buff1.Length);
                                    GlobalVal.isLogin = true;
                                    toolStripStatusLabel.Text = username + "登录成功！";
                                }
                                break;
                            case "2": //Logout
                                // TODO 可以先不写
                                break;
                            case "3": //Talk
                                if (GlobalVal.isLogin)
                                {
                                    setText(obj["msg"].ToString());
                                }
                                break;
                            case "4": //Regist
                                username = obj["username"].ToString();
                                password = obj["password"].ToString();
                                // TODO 增加数据库操作
                                byte[] buff = Common.convertMessageToByte(MessageStr.getRetMessage(true, ""));
                                GlobalVal.isLogin = true;
                                stream.Write(buff, 0, buff.Length);
                                toolStripStatusLabel.Text = username + "注册并登录成功！";
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
                            case "6":
                                Image baseImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                                Graphics g = Graphics.FromImage(baseImage);
                                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
                                g.Dispose();
                                baseImage.Save("tmp\\sc.png", ImageFormat.Png);
                                FileStream fileStream = new FileStream("tmp\\sc.png", FileMode.Open, FileAccess.Read);
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
                                byte[] buff3 = Common.convertMessageToByte(MessageFile.getFileMessage("sc.png", temp));
                                stream.Write(buff3, 0, buff3.Length);
                                break;
                            case "7":
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
            if(this.pictureBox.InvokeRequired)
            {
                Action<Image> d = setImg;
                this.pictureBox.Invoke(d, img);
            }
            else
            {
                this.pictureBox.Image = img;
            }
        }

        private Bitmap GetThumbnail(Image b, int destHeight, int destWidth)
        {
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
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
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
                                            setImg(GetThumbnail(img, this.pictureBox.Height, this.pictureBox.Width));
                                        }
                                    }
                                    else
                                    {
                                        string content = Encoding.UTF8.GetString(message.Content);
                                        JObject obj = JObject.Parse(content);
                                        if (obj["cmd"].ToString() == "3")//Talk
                                        {
                                            setText(obj["msg"].ToString());
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
                                                for (int i = 0; i<read.BaseStream.Length; i++)
                                                {
                                                    temp[i] = read.ReadByte();
                                                }
                                                read.Close();
                                                fileStream.Close();
                                                byte[] buff1 = Common.convertMessageToByte(MessageFile.getFileMessage(fileName.Substring(fileName.LastIndexOf('\\') + 1), temp));
                                                client.stream.Write(buff1, 0, buff1.Length);
                                                MessageBox.Show("文件已传输！");
                                                return;
                                            }
                                            else
                                            {
                                                MessageBox.Show("服务器拒绝了您的请求！");
                                                return;
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogUtil.GetLog().Write(ex.StackTrace);
                                    MessageBox.Show("运行出错！");
                                    return;
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
                    toolStripStatusLabel.Text = GlobalVal.username + "注册并登录成功！";
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

                                    }
                                    else
                                    {
                                        string content = Encoding.UTF8.GetString(message.Content);
                                        JObject obj = JObject.Parse(content);
                                        switch (obj["cmd"].ToString())
                                        {
                                            case "3":// Talk
                                                setText(obj["msg"].ToString());
                                                break;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogUtil.GetLog().Write(ex.StackTrace);
                                    MessageBox.Show("运行出错！");
                                    return;
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

        private void buttonSendFile_Click(object sender, EventArgs e)
        {
            if (!GlobalVal.isLogin)
            {
                MessageBox.Show("请先登录！");
                return;
            }
            if (GlobalVal.isService)
            {

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
                    JObject obj = new JObject(new JProperty("cmd", (int)MessageStr.CmdType.Cmd), new JProperty("content", textBoxCmd.Text));
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
