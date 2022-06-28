using Publish.Settings;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publish.Base
{
    public interface ISender : IDisposable
    {
        void Send(byte[] body, string routingKey);
    }

    public class Sender : ISender
    {
        // cấu hình
        public RabbitConfig rabbitConfig { set; get; }

        protected virtual ConnectionFactory CreateConnectionFactory()
        {
            return new ConnectionFactory
            {
                HostName = rabbitConfig.HostName,
                Port = rabbitConfig.Port,
                UserName = rabbitConfig.UserName,
                Password = rabbitConfig.Password
            };
        }

        private ShRabbit rabbit = null;

        // khởi tạo rabbit
        public ShRabbit Rabbit
        {
            get
            {
                if (rabbit == null) rabbit = new ShRabbit(CreateConnectionFactory());
                return rabbit;
            }
        }


        public void Dispose()
        {
            if (rabbit != null)
            {
                rabbit.Dispose();
                rabbit = null;
            }
        }

        public void Send(byte[] body, string routingKey)
        {
            var fail = 0;
            while (fail < 10)
            {
                try
                {
                    Rabbit.Publish(rabbitConfig.ExchangeKey, routingKey, body);
                    break;
                }
                catch (Exception)
                {

                    fail++;
                    System.Threading.Thread.Sleep(500);
                }
            }



        }
    }
}
