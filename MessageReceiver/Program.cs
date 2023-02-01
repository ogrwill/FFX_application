using System;

namespace MessageReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var startup = new Startup();
            MQReceiver receiver = new MQReceiver(startup.AppSettings.ConnectionString);
            receiver.Receive();
        }
    }
}
