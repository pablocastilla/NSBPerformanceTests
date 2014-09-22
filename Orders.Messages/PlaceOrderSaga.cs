using NServiceBus;

namespace Orders.Messages
{
    public class PlaceOrderSaga : ICommand
    {
        public string OrderId { get; set; }
    }
}
