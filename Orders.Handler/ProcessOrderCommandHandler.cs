using NServiceBus;
using Orders.Messages;

namespace Orders.Handler
{
    using System;
    using System.Configuration;
    using NServiceBus.Logging;
    using Utils;

  
    public class ProcessOrderCommandHandler : IHandleMessages<PlaceOrder>
    {
        public static ILog Logger = LogManager.GetLogger("NSBPerformanceTests");
        Velocimeter accelerometer = Velocimeter.getInstance();

        public IBus Bus { get; set; }
        public void Handle(PlaceOrder placeOrder)
        {
            //Console.Out.WriteLine("Received PlaceOrderSaga command, order Id: " + placeOrder.OrderId);
            // Bus.Return(PlaceOrderStatus.Ok);
            // Console.Out.WriteLine("Sent Ok status for orderId [{0}].", placeOrder.OrderId);

            // Process Order...
            // Console.Out.WriteLine("Processing received order....");

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["EmitEvent"]))
            {
                Bus.Publish<OrderPlaced>(m => m.OrderId = placeOrder.OrderId);
            }
            // Console.Out.WriteLine("Sent Order placed event for orderId [{0}].", placeOrder.OrderId);

            System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["ThreadStopTime"]));

            accelerometer.IncrementMessages();
            // Console.Out.WriteLine("MSGs/Seconds [{0}].", accelerometer.GetSpeed());


            if (placeOrder.OrderId.EndsWith("00"))
                Logger.Warn(string.Format("MSGs/Seconds [{0}].", accelerometer.GetSpeed()));


        }
    }
}
