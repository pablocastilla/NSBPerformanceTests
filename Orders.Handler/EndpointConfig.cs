using System;
using System.Configuration;
using NServiceBus;

namespace Orders.Handler
{
    class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["DTCEnabled"]))
            {
                Console.Out.WriteLine("DTC disabled");

                Configure.Transactions.Advanced(settings =>
                {
                    settings.DisableDistributedTransactions();
                    settings.DefaultTimeout(TimeSpan.FromSeconds(120));
                });
            }

        }
    }

  
}
