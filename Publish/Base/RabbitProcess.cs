using Publish.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publish.Base
{
    public static class RabbitProcess
    {
        public static RabbitConfig RabbitConfigS = new RabbitConfig()
        {
            HostName = "192.168.1.49",
            Port = 4141,
            UserName = "admin",
            Password = "admin",
            ExchangeKey = "Reader2014"
        };

        public static Sender ObjSender = new Sender()
        {
            rabbitConfig = RabbitConfigS
        };
    }
}
