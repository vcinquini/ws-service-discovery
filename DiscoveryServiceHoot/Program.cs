using System;
using System.ServiceModel;
using System.ServiceModel.Discovery;

namespace DiscoveryServiceHoot
{
	class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri(string.Format("http://{0}:8000/discovery/scenarios/Myservice/{1}/", System.Net.Dns.GetHostName(), Guid.NewGuid().ToString()));

            Console.WriteLine(baseAddress);

            using (ServiceHost serviceHost = new ServiceHost(typeof(SampleDiscoveryService.Service1), baseAddress))
            {

                serviceHost.AddServiceEndpoint(typeof(SampleDiscoveryService.IService1), new WSHttpBinding(), string.Empty);



                serviceHost.Description.Behaviors.Add(new ServiceDiscoveryBehavior());



                serviceHost.AddServiceEndpoint(new UdpDiscoveryEndpoint());


                serviceHost.Open();


                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.ReadLine();
            }
        }
    }
}
