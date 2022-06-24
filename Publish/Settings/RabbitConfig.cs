using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publish.Settings
{
    public class RabbitConfig
    {
        public int PK_SystemSend { set; get; }
        public string HostName { set; get; }
        public int Port { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string ExchangeKey { set; get; }
        public string RoutingKey { get; set; }
    }
}
