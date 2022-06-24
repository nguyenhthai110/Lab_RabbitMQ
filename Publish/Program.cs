using Publish.Entities;
using System;

namespace Publish
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 0; i < 10; i++)
            {
                var mess = new MessageDataEntity()
                {
                    Name = "Nguyen Huu Thai_" + i,
                    Age = 37 + i
                };

                mess.PublishMess();
            }
            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
