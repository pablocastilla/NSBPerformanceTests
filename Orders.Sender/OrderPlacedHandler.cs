using System;
using NServiceBus;
using Orders.Messages;
using Utils;

namespace Orders.Sender
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        

        public void Handle(OrderPlaced orderPlaced)
        {
            Console.WriteLine("Received Event OrderPlaced for orderId: " + orderPlaced.OrderId);

            Velocimeter.getInstance().IncrementMessages();
            Console.Out.WriteLine("MSGs/Seconds [{0}].", Velocimeter.getInstance().GetSpeed());


            if( Velocimeter.getInstance().IsFinished())
            {
                Console.Out.WriteLine("Work finished in " + Velocimeter.getInstance().TotalTime());
            }
        }
    }
    
}
