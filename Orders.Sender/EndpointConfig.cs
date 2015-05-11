using System;
using System.Configuration;
using NServiceBus;
using NServiceBus.Persistence;

namespace Orders.Sender
{
    class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public void Customize(BusConfiguration configuration)
        {
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["DTCEnabled"]))
            {
                Console.Out.WriteLine("DTC disabled");


                configuration.Transactions().DisableDistributedTransactions();
            }
            else
            {
                Console.Out.WriteLine("DTC enabled");
            }

            configuration.UsePersistence<InMemoryPersistence>();
        }

      
    }

   
}
