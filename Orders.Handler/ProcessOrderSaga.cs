﻿using NServiceBus;
using Orders.Messages;


namespace Orders.Handler
{
    using System;

    using NServiceBus.Saga;
    using Utils;
    using NServiceBus.Logging;
   



    public class ProcessOrderSagaData: IContainSagaData
    {

        public Guid Id { get; set; }


        public string OriginalMessageId { get; set; }


        public string Originator { get; set; }
     
    }

    public class ProcessOrderSaga : Saga<ProcessOrderSagaData>, IAmStartedByMessages<PlaceOrderSaga>
    {
       public  static ILog Logger = LogManager.GetLogger("NSBPerformanceTests");

         Velocimeter accelerometer = Velocimeter.getInstance();

     
        public void Handle(PlaceOrderSaga placeOrder)
        {
            //Console.Out.WriteLine("Received PlaceOrderSaga command, order Id: " + placeOrder.OrderId);
           // Bus.Return(PlaceOrderStatus.Ok);
           // Console.Out.WriteLine("Sent Ok status for orderId [{0}].", placeOrder.OrderId);

            // Process Order...
           // Console.Out.WriteLine("Processing received order....");
            
            Bus.Publish<OrderPlaced>(m => m.OrderId = placeOrder.OrderId);
            // Console.Out.WriteLine("Sent Order placed event for orderId [{0}].", placeOrder.OrderId);

            accelerometer.IncrementMessages();
           // Console.Out.WriteLine("MSGs/Seconds [{0}].", accelerometer.GetSpeed());


            if (placeOrder.OrderId.EndsWith("0"))
                Logger.Warn(string.Format("MSGs/Seconds [{0}].", accelerometer.GetSpeed()));

            this.MarkAsComplete();
        }
    }
}
