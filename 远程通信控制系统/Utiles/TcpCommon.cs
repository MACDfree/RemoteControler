using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 远程通信控制系统
{
    class TcpCommon
    {
    }

    class Service
    {
        private TcpListener listener = null;
        private Thread listenThread = null;

        public Service(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        public Service(string ip, int port)
        {
            listener = new TcpListener(IPAddress.Parse(ip), port);
        }

        public void Start()
        {
            Console.WriteLine("开始监听...");
            listener.Start();
            listenThread = new Thread(ListenFun);
            Console.WriteLine("启动监听线程...");
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
                byte[] buff = Common.convertMessageToByte(MessageStr.getLoginRetMessage(true));
                stream.Write(buff, 0, buff.Length);
            }
        }
    }

    class Client
    {
        TcpClient client = null;
        int port = 1314;
        string ip = "127.0.0.1";
        NetworkStream stream = null;

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
                client = new TcpClient();
                client.Connect(IPAddress.Parse(ip), port);
                Console.WriteLine("连接成功");
            }
            catch (Exception e)
            {
                Console.WriteLine("连接失败");
                Console.WriteLine(e.Message);
                return;
            }
            stream = client.GetStream();
            byte[] buff = Common.convertMessageToByte(MessageStr.getLoginMessage("test", "11111"));
            stream.Write(buff, 0, buff.Length);
            Thread receiveThread = new Thread(ReceiveFun);
            receiveThread.IsBackground = true;
            receiveThread.Start();
            while (true) ;
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

    class Common
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
            stream.Read(content, 0, length);
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

    class Message
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
                return new MessageFile(false, length);
            }
            else
            {
                return new MessageStr(true, length);
            }
        }
    }

    /// <summary>
    /// 字符串消息类
    /// </summary>
    class MessageStr : Message
    {
        public enum CmdType
        {
            Login = 1,
            Logout = 2,
            Talk = 3
        }

        public MessageStr(bool isFile, int length) : base(isFile, length) { }

        public static MessageStr getLoginMessage(string username, string password)
        {
            JObject obj = new JObject(new JProperty("cmd", "1"), new JProperty("username", username), new JProperty("password", password));
            byte[] buff = Encoding.UTF8.GetBytes(obj.ToString());
            MessageStr loginMessage = new MessageStr(false, buff.Length);
            loginMessage.Content = buff;
            return loginMessage;
        }
        public static MessageStr getLoginRetMessage(bool isSuccess)
        {
            JObject obj = new JObject(new JProperty("isSuccess", isSuccess));
            byte[] buff = Encoding.UTF8.GetBytes(obj.ToString());
            MessageStr message = new MessageStr(false, buff.Length);
            message.Content = buff;
            return message;
        }
    }

    /// <summary>
    /// 文件消息类，其中conten为所有文件内容（包括文件正文和文件名），fileLength表示文件正文的长度
    /// fileLength为content的前4个字节
    /// </summary>
    class MessageFile : Message
    {
        // 文件内容
        private byte[] content;

        // 文件正文长度
        private int fileLength;

        public int FileLength
        {
            get
            {
                fileLength = Common.getInt(content[0], content[1], content[2], content[3]);
                return fileLength;
            }
        }

        public MessageFile(bool isFile, int length) : base(isFile, length) { }
    }
}
