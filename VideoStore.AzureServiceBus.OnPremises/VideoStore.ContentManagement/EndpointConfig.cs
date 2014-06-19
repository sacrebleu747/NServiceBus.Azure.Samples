using NServiceBus.Azure.Transports.WindowsAzureServiceBus;
using NServiceBus.Config;
using NServiceBus.Hosting.Helpers;

namespace VideoStore.ContentManagement
{
    using System;
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, UsingTransport<AzureServiceBus> {

        public void Customize(ConfigurationBuilder builder)
        {
            builder.Conventions(c =>
                c.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("VideoStore") && t.Namespace.EndsWith("Commands"))
                 .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("VideoStore") && t.Namespace.EndsWith("Events"))
                 .DefiningMessagesAs(t => t.Namespace != null && t.Namespace.StartsWith("VideoStore") && t.Namespace.EndsWith("RequestResponse"))
                 .DefiningEncryptedPropertiesAs(p => p.Name.StartsWith("Encrypted")));
        }
      
    }
    
    public class MyClass : IWantToRunWhenBusStartsAndStops
    {
        public void Start()
        {
            Console.Out.WriteLine("The VideoStore.ContentManagement endpoint is now started and subscribed to OrderAccepted events from VideoStore.Sales");
        }

        public void Stop()
        {

        }
    }

    ///// <summary>
    ///// This is just here so that topics are created irrespective of boot order of the processes
    ///// </summary>
    //public class AutoCreateDependantTopics : IWantToRunWhenConfigurationIsComplete
    //{
    //    public void Run()
    //    {
    //        var topicCreator = new AzureServicebusTopicCreator();

    //        topicCreator.Create(AzureServiceBusPublisherAddressConventionForSubscriptions.Apply(Address.Parse("VideoStore.Sales")));
    //    }
    //}
}
