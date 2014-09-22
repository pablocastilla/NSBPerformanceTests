using System;
using NServiceBus;
using Orders.Messages;
using Utils;

namespace Orders.Sender
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        public static Velocimeter accelerometer = new Velocimeter();

        public void Handle(OrderPlaced orderPlaced)
        {
            Console.WriteLine("Received Event OrderPlaced for orderId: " + orderPlaced.OrderId);

            accelerometer.IncrementMessages();
            Console.Out.WriteLine("MSGs/Seconds [{0}].", accelerometer.GetSpeed());
        }
    }
    
}
