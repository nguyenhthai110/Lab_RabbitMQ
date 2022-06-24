using RabbitMQ.Client;
using System;

namespace Publish.Base
{
    public class ShRabbit : IDisposable
    {
        private ConnectionFactory factory = null;

        public ShRabbit(ConnectionFactory factory)
        {
            this.factory = factory;
        }

        private IConnection _connection = null;
        /// <summary>
        /// Lấy ra connection
        /// </summary>
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

        private IModel _channel = null;
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
        public void Publish(string exchangeKey, string routingKey, byte[] body)
        {
            Channel.BasicPublish(exchangeKey, routingKey, null, body);
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
