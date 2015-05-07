using System;
using System.Configuration;
using NServiceBus;
using NServiceBus.Persistence;

namespace Orders.Handler
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

            configuration.UsePersistence<InMemoryPersistence>();
        }
    }

  
}
