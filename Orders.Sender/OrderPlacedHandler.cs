using System;
using NServiceBus;
using NServiceBus.Logging;
using Orders.Messages;
using Utils;

namespace Orders.Sender
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        public static ILog Logger = LogManager.GetLogger("NSBPerformanceTests");

        public void Handle(OrderPlaced orderPlaced)
        {
           // Console.WriteLine("Received Event OrderPlaced for orderId: " + orderPlaced.OrderId);

            Velocimeter.getInstance().IncrementMessages();
          //  Console.Out.WriteLine("MSGs/Seconds [{0}].", Velocimeter.getInstance().GetSpeed());
            if (orderPlaced.OrderId.EndsWith("0"))
                Logger.Warn(string.Format("MSGs/Seconds [{0}].", Velocimeter.getInstance().GetSpeed()));

            if( Velocimeter.getInstance().IsFinished())
            {
                 Logger.Warn("Work finished in " + Velocimeter.getInstance().TotalTime());
            }
        }
    }
    
}
