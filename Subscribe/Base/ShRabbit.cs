using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscribe.Base
{
    public class ShRabbit : IDisposable
    {
        private ConnectionFactory factory = null;

        public ShRabbit(ConnectionFactory factory)
        {
            this.factory = factory;
        }

        private IConnection _connection = null;
        private IModel _channel = null;
        public IConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = factory.CreateConnection();
                }
                return _connection;
            }
        }

        public IModel Channel
        {
            get
            {
                if (_channel == null)
                {
                    _channel = Connection.CreateModel();
                }

                return _channel;
            }
        }

        public void Dequeue(string routingKey)
        {

            var consumer = new EventingBasicConsumer(Channel);
            Channel.BasicConsume(queue: routingKey,
                                    autoAck: false,
                                    consumer: consumer);

            consumer.Received += (model, ea) =>
             {
                 using (MemoryStream stream = new MemoryStream(ea.Body.ToArray()))
                 {
                     var mes = stream;
                 }
             };

        }


        public void Dispose()
        {
            if (_channel != null)
            {
                _channel.Dispose();
                _channel = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
