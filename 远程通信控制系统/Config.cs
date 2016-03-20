using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 远程通信控制系统
{
    /// <summary>
    /// 参数类
    /// </summary>
    class Config
    {
        // 服务器端地址
        private string serviceIp;
        private string servicePort;

        // 客户端地址
        private string clientIp;
        private string clientPort;

        // 服务器模式or客户端模式
        private bool isClient;

        public string ServiceIp
        {
            get
            {
                return serviceIp;
            }

            set
            {
                serviceIp = value;
            }
        }

        public string ServicePort
        {
            get
            {
                return servicePort;
            }

            set
            {
                servicePort = value;
            }
        }

        public string ClientIp
        {
            get
            {
                return clientIp;
            }

            set
            {
                clientIp = value;
            }
        }

        public string ClientPort
        {
            get
            {
                return clientPort;
            }

            set
            {
                clientPort = value;
            }
        }

        public bool IsClient
        {
            get
            {
                return isClient;
            }

            set
            {
                isClient = value;
            }
        }
    }
}
