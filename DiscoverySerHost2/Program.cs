using System;
using System.ServiceModel;
using System.ServiceModel.Discovery;

namespace DiscoverySerHost2
{
	class Program
	{
		static void Main(string[] args)
		{
			Uri baseAddress = new Uri(string.Format("http://{0}:8000/discovery/scenarios/Myservice/{1}/", System.Net.Dns.GetHostName(), Guid.NewGuid().ToString()));

			Console.WriteLine(baseAddress);
			// Create a ServiceHost for the CalculatorService type.
			using (ServiceHost serviceHost = new ServiceHost(typeof(SampleDiscoveryService.Service1), baseAddress))
			{
				// add calculator endpoint
				serviceHost.AddServiceEndpoint(typeof(SampleDiscoveryService.IService1), new WSHttpBinding(), string.Empty);

				// ** DISCOVERY ** //
				// make the service discoverable by adding the discovery behavior
				serviceHost.Description.Behaviors.Add(new ServiceDiscoveryBehavior());

				// ** DISCOVERY ** //
				// add the discovery endpoint that specifies where to publish the services
				serviceHost.AddServiceEndpoint(new UdpDiscoveryEndpoint());

				// Open the ServiceHost to create listeners and start listening for messages.
				serviceHost.Open();

				// The service can now be accessed.
				Console.WriteLine("Press <ENTER> to terminate service.");
				Console.ReadLine();
			}
		}
	}
}
