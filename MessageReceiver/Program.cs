using System;

namespace MessageReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MQReceiver receiver = new MQReceiver();
            receiver.Receive();
        }
    }
}
