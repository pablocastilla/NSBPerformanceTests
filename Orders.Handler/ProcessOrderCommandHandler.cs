using NServiceBus;
using Orders.Messages;

namespace Orders.Handler
{
    using System;

    public class Accelerometer
    {
        DateTime initialTime;
        double numberOfMessages;

        public Accelerometer()
        {
            initialTime = DateTime.Now;
            numberOfMessages = 0;
        }

        public void IncrementMessages()
        {
            numberOfMessages++;

        }

        public double GetSpeed()
        {

            return numberOfMessages / (DateTime.Now - initialTime).TotalSeconds;

        }

    }

    public class ProcessOrderCommandHandler : IHandleMessages<PlaceOrder>
    {
        public static Accelerometer accelerometer = new Accelerometer();

        public IBus Bus { get; set; }
        public void Handle(PlaceOrder placeOrder)
        {
            Console.Out.WriteLine("Received ProcessOrder command, order Id: " + placeOrder.OrderId);
            Bus.Return(PlaceOrderStatus.Ok);
            Console.Out.WriteLine("Sent Ok status for orderId [{0}].", placeOrder.OrderId);

            // Process Order...
            Console.Out.WriteLine("Processing received order....");
            
            Bus.Publish<OrderPlaced>(m => m.OrderId = placeOrder.OrderId);
            Console.Out.WriteLine("Sent Order placed event for orderId [{0}].", placeOrder.OrderId);

            accelerometer.IncrementMessages();
            Console.Out.WriteLine("MSGs/Seconds [{0}].", accelerometer.GetSpeed());


        }
    }
}
