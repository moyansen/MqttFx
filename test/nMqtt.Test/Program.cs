﻿using System;
using System.Text;
using System.Threading;
using nMqtt.Messages;

namespace nMqtt.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MqttClient("127.0.0.1");
            var state = client.Connect();
            if (state == ConnectionState.Connected)
            {
                client.MessageReceived += OnMessageReceived;
                client.Subscribe("a/b", Qos.AtLeastOnce);

                for (int i = 0; i < 100; i++)
                {
                    client.Publish("a/b", Encoding.UTF8.GetBytes("Hello World!" + i.ToString()), Qos.AtLeastOnce);
                    Thread.Sleep(100);
                }
            }
            Console.ReadKey();
        }

        static void OnMessageReceived(string topic, byte[] data)
        {
            Console.WriteLine("-------------------");
            Console.WriteLine("topic:{0}", topic);
            Console.WriteLine("data:{0}", Encoding.UTF8.GetString(data));
        }
    }
}