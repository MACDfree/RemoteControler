﻿using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace 远程通信控制系统
{
    public class TcpCommon
    {
        private static Service service;
        private static Client client;

        public static Service getService()
        {
            if (service == null)
            {
                if (String.IsNullOrWhiteSpace(GlobalVal.serviceIP) || GlobalVal.servicePort == 0)
                {
                    return null;
                }
                service = new Service(GlobalVal.serviceIP, GlobalVal.servicePort);
            }
            return service;
        }

        public static Client getClient()
        {
            if (client == null)
            {
                if (String.IsNullOrWhiteSpace(GlobalVal.serviceIP) || GlobalVal.servicePort == 0)
                {
                    return null;
                }
                client = new Client(GlobalVal.serviceIP, GlobalVal.servicePort);
            }
            return client;
        }
    }

    public class Service
    {
        public TcpListener listener = null;
        private Thread listenThread = null;

        public Service(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        public Service(string ip, int port)
        {
            listener = new TcpListener(IPAddress.Parse(ip), port);
        }

        public void Start(ThreadStart listenFun)
        {
            LogUtil.GetLog().Write("开始监听...");
            listener.Start(1);
            listenThread = new Thread(listenFun);
            listenThread.IsBackground = true;
            LogUtil.GetLog().Write("开始启动监听线程...");
            listenThread.Start();
        }

        private void ListenFun()
        {
            while (true)
            {
                TcpClient client = null;
                try
                {
                    client = listener.AcceptTcpClient();
                }
                catch
                {
                    break;
                }
                NetworkStream stream = client.GetStream();
                Message message = Common.GetMessage(stream);
                Console.WriteLine("Length:{0},isFile:{1}", message.Length, message.IsFile);
                string content = Encoding.UTF8.GetString(message.Content);
                Console.WriteLine(content);
                byte[] buff = Common.convertMessageToByte(MessageStr.getRetMessage(true, ""));
                stream.Write(buff, 0, buff.Length);
            }
        }
    }

    public class Client
    {
        TcpClient client = null;
        int port = 1314;
        string ip = "127.0.0.1";
        public NetworkStream stream = null;

        public Client() { }

        public Client(int port)
        {
            this.port = port;
        }

        public Client(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public void Connect()
        {
            try
            {
                if (client == null)
                {
                    client = new TcpClient();
                }
                if (!client.Connected)
                {
                    client.Connect(IPAddress.Parse(ip), port);
                }
                LogUtil.GetLog().Write("连接成功");
            }
            catch (Exception e)
            {
                LogUtil.GetLog().Write("连接失败", e);
                throw e;
            }
            stream = client.GetStream();
        }

        private void ReceiveFun()
        {
            Message message = null;
            while (true)
            {
                try
                {
                    message = Common.GetMessage(stream);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
                Console.WriteLine("Length:{0},isFile:{1}", message.Length, message.IsFile);
            }
        }
    }

    public class Common
    {
        public static Message GetMessage(NetworkStream stream)
        {
            int H = stream.ReadByte();
            int MH = stream.ReadByte();
            int ML = stream.ReadByte();
            int L = stream.ReadByte();
            int T = stream.ReadByte();
            if (H == -1 || MH == -1 || ML == -1 || L == -1 || T == -1)
            {
                throw new SystemException();
            }

            int length = getInt(H, MH, ML, L);
            bool isFile = (T & 0xff) == 14;

            Message message = Message.getInstance(isFile, length);
            byte[] content = new byte[length];
            for(int i=0;i<length;i++)
            {
                content[i] = (byte)stream.ReadByte();
            }
            message.Content = content;
            return message;
        }

        /// <summary>
        /// 将4个字节转换成32位int长度
        /// </summary>
        /// <param name="H"></param>
        /// <param name="MH"></param>
        /// <param name="ML"></param>
        /// <param name="L"></param>
        /// <returns></returns>
        public static int getInt(int H, int MH, int ML, int L)
        {
            return ((H & 0xff) << 24) | ((MH & 0xff) << 16) | ((ML & 0xff) << 8) | (L & 0xff);
        }

        /// <summary>
        /// 将int长度转换成字节数组
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] getByte(int length)
        {
            byte[] buff = new byte[4];
            buff[0] = (byte)((length & 0xff000000) >> 24);
            buff[1] = (byte)((length & 0x00ff0000) >> 16);
            buff[2] = (byte)((length & 0x0000ff00) >> 8);
            buff[3] = (byte)(length & 0x000000ff);
            return buff;
        }

        public static void copyByteArray(byte[] src, byte[] dest, int start, int length)
        {
            for (int i = start, j = 0; i < start + length; i++, j++)
            {
                dest[i] = src[j];
            }
        }

        public static byte[] convertMessageToByte(Message message)
        {
            byte[] buff = new byte[message.Length + 5];
            byte[] length = getByte(message.Length);
            copyByteArray(length, buff, 0, 4);
            buff[4] = (byte)(message.IsFile ? 14 : 13);
            copyByteArray(message.Content, buff, 5, message.Length);
            return buff;
        }
    }

    public class Message
    {
        private bool isFile = false;
        private int length;
        private byte[] content;

        public bool IsFile
        {
            get
            {
                return isFile;
            }

            private set
            {
                isFile = value;
            }
        }

        public int Length
        {
            get
            {
                return length;
            }

            private set
            {
                length = value;
            }
        }

        public byte[] Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }

        public Message() { }

        /// <summary>
        /// 消息构造方法
        /// </summary>
        /// <param name="isFile">是否是文件消息（分为文件消息和字符串消息）</param>
        /// <param name="length">消息内容长度（注意文件消息有两个长度）</param>
        protected Message(bool isFile, int length)
        {
            this.isFile = isFile;
            this.length = length;
        }

        /// <summary>
        /// 消息静态工厂方法
        /// </summary>
        /// <param name="isFile">是否是文件消息（分为文件消息和字符串消息）</param>
        /// <param name="length">消息内容长度（注意文件消息有两个长度）</param>
        /// <returns></returns>
        public static Message getInstance(bool isFile, int length)
        {
            if (isFile)
            {
                return new MessageFile(true, length);
            }
            else
            {
                return new MessageStr(false, length);
            }
        }
    }

    /// <summary>
    /// 字符串消息类
    /// </summary>
    public class MessageStr : Message
    {
        public enum CmdType
        {
            Login = 1,
            Logout = 2,
            Talk = 3,
            Regist = 4,
            FileConfirm = 5,
            sc = 6,
            Cmd = 7,
            MLeft1 = 8,//鼠标左单击
            MLeft2 = 9,//鼠标左双击
            MRight1 = 10,//鼠标右单击
            MRight2 = 11,//鼠标左双击
            K1 = 12//键盘值
        }

        public MessageStr(bool isFile, int length) : base(isFile, length) { }

        public static MessageStr getLoginMessage(string username, string password)
        {
            JObject obj = new JObject(new JProperty("cmd", (int)CmdType.Login), new JProperty("username", username), new JProperty("password", password));
            byte[] buff = Encoding.UTF8.GetBytes(obj.ToString());
            MessageStr loginMessage = new MessageStr(false, buff.Length);
            loginMessage.Content = buff;
            return loginMessage;
        }
        public static MessageStr getRetMessage(bool isSuccess, string msg)
        {
            JObject obj = new JObject(new JProperty("isSuccess", isSuccess), new JProperty("msg", msg));
            byte[] buff = Encoding.UTF8.GetBytes(obj.ToString());
            MessageStr message = new MessageStr(false, buff.Length);
            message.Content = buff;
            return message;
        }

        public static MessageStr getRetMessageWithCmd(int cmd, bool isSuccess, string msg)
        {
            JObject obj = new JObject(new JProperty("cmd", cmd), new JProperty("isSuccess", isSuccess), new JProperty("msg", msg));
            byte[] buff = Encoding.UTF8.GetBytes(obj.ToString());
            MessageStr message = new MessageStr(false, buff.Length);
            message.Content = buff;
            return message;
        }

        public static MessageStr getCommMessage(string json)
        {
            byte[] buff = Encoding.UTF8.GetBytes(json);
            MessageStr message = new MessageStr(false, buff.Length);
            message.Content = buff;
            return message;
        }

        public static MessageStr getTalkMessage(string msg)
        {
            JObject obj = new JObject(new JProperty("cmd", (int)CmdType.Talk), new JProperty("msg", msg));
            byte[] buff = Encoding.UTF8.GetBytes(obj.ToString());
            MessageStr message = new MessageStr(false, buff.Length);
            message.Content = buff;
            return message;
        }

        public static MessageStr getRegistMessage(string username, string password)
        {
            // 获取json对象
            JObject obj = new JObject(new JProperty("cmd", (int)CmdType.Regist), new JProperty("username", username), new JProperty("password", password));
            // 将json字符串用utf-8编码
            byte[] buff = Encoding.UTF8.GetBytes(obj.ToString());
            // 封装成message对象
            MessageStr loginMessage = new MessageStr(false, buff.Length);
            loginMessage.Content = buff;
            return loginMessage;
        }
    }

    /// <summary>
    /// 文件消息类，其中conten为所有文件内容（包括文件正文和文件名），fileLength表示文件正文的长度
    /// fileLength为content的前4个字节
    /// </summary>
    public class MessageFile : Message
    {
        //// 文件内容
        //private byte[] content;

        //// 文件正文长度
        //private int fileLength;

        //public int FileLength
        //{
        //    get
        //    {
        //        return fileLength;
        //    }
        //}

        public MessageFile(bool isFile, int length) : base(isFile, length) { }

        public static MessageFile getFileMessage(string filename, byte[] filecontent)
        {
            byte[] fname = Encoding.UTF8.GetBytes(filename);
            byte[] content = new byte[4 + filecontent.Length + fname.Length];
            byte[] filelength = Common.getByte(filecontent.Length);
            Common.copyByteArray(filelength, content, 0, 4);
            Common.copyByteArray(filecontent, content, 4, filecontent.Length);
            Common.copyByteArray(fname, content, 4 + filecontent.Length, fname.Length);
            MessageFile msg = new MessageFile(true, content.Length);
            msg.Content = content;
            return msg;
        }

        public static void getFileAndInfo(byte[] content, out int length, out byte[] filecontent, out string filename)
        {
            length = Common.getInt(content[0], content[1], content[2], content[3]);
            filecontent = new byte[length];
            for (int i = 0; i < length; i++)
            {
                filecontent[i] = content[i + 4];
            }
            int filenamelength = content.Length - 4 - length;
            byte[] bfilename = new byte[filenamelength];
            for (int i = 0; i < filenamelength; i++)
            {
                bfilename[i] = content[4 + length + i];
            }
            filename = Encoding.UTF8.GetString(bfilename);
        }
    }
}
