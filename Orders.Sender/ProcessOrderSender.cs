using System;
using System.Threading.Tasks;
using NServiceBus;
using Orders.Messages;

namespace Orders.Sender
{
    class ProcessOrderSender : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Console.WriteLine("Press 'm' to send a bulk of messages, press 's' to send a bulk of messages that inits a saga. To exit, Ctrl + C");

            while (Console.ReadLine() != "m")
            {
                Console.WriteLine("Starting to send 40k messages");
                SendBulk();
                Console.WriteLine("Done sending 40k messages");
            }

            while (Console.ReadLine() != "s")
            {
                Console.WriteLine("Starting to send 40k messages");
                SendBulk();
                Console.WriteLine("Done sending 40k messages");
            }
        }

        private void SendBulk()
        {
          

            Parallel.For(0, 40000, i=>
            {
               
                var placeOrder = new PlaceOrder {OrderId = "order" + i};
                Bus.Send(placeOrder).Register(PlaceOrderReturnCodeHandler, this);
                Console.WriteLine(string.Format("Sent PlacedOrder command with order id [{0}].", placeOrder.OrderId));
            });
        }

        private void SendBulkSaga()
        {


            Parallel.For(0, 40000, i =>
            {

                var placeOrder = new PlaceOrderSaga { OrderId = "order" + i };
                Bus.Send(placeOrder).Register(PlaceOrderReturnCodeHandler, this);
                Console.WriteLine(string.Format("Sent PlacedOrderSaga command with order id [{0}].", placeOrder.OrderId));
            });
        }

        private static void PlaceOrderReturnCodeHandler(IAsyncResult asyncResult)
        {
            var result = asyncResult.AsyncState as CompletionResult;
            Console.WriteLine(string.Format("Received [{0}] Return code for Placing Order.", Enum.GetName(typeof (PlaceOrderStatus), result.ErrorCode)));
        }

        public void Stop()
        {
            
        }
    }
}
