using System;
using System.Configuration;
using System.Threading.Tasks;
using NServiceBus;
using Orders.Messages;
using Utils;

namespace Orders.Sender
{
    class ProcessOrderSender : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public int NUMBEROFMESSAGES = Convert.ToInt32(ConfigurationManager.AppSettings["MESSAGES"]);

        public void Start()
        {
            Console.WriteLine("Press 'm' to send a bulk of messages, press 's' to send a bulk of messages that inits a saga. To exit, Ctrl + C");



            Velocimeter accelerometer = Velocimeter.getInstance(NUMBEROFMESSAGES);

            var option = Console.ReadLine();

            if (option == "m")
            {
                
                Console.WriteLine("Starting to send 40k messages");
                SendBulk(NUMBEROFMESSAGES);
                Console.WriteLine("Done sending 40k messages");
            }

             if (option == "s")
            {
                Console.WriteLine("Starting to send 40k messages");
                SendBulkSaga(NUMBEROFMESSAGES);
                Console.WriteLine("Done sending 40k messages");
            }
        }

        private void SendBulk(int numberOfMessages)
        {

            Parallel.For(0, numberOfMessages, i =>
            {               
                var placeOrder = new PlaceOrder {OrderId = "order" + i};
                Bus.Send(placeOrder);//.Register(PlaceOrderReturnCodeHandler, this);
              
            });

            Console.WriteLine(string.Format("Commands for handlers sent"));
        }

        private void SendBulkSaga(int numberOfMessages)
        {            
            Parallel.For(0, numberOfMessages, i =>
            {
                var placeOrder = new PlaceOrderSaga { OrderId = "order" + i };
                Bus.Send(placeOrder);//.Register(PlaceOrderReturnCodeHandler, this);
               
            });

            Console.WriteLine(string.Format("Commands for sagas sent"));
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
