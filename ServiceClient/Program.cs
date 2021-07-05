using System;
using System.ServiceModel;
using System.ServiceModel.Discovery;

namespace ServiceClient
{
	class Program
    {
        static EndpointAddress serviceAddress;

        static void Main()
        {
            if (FindService()) InvokeService();
        }

        static bool FindService()
        {
            Console.WriteLine("\nFinding Myservice Service ..");

            DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());

            var calculatorServices = discoveryClient.Find(new FindCriteria(typeof(ServiceReference1.IService1)));

            discoveryClient.Close();

            if (calculatorServices == null)
            {
                Console.WriteLine("\nNo services are found.");
                return false;
            }
            else
            {
                serviceAddress = calculatorServices.Endpoints[0].Address;
                return true;
            }
        }

        static void InvokeService()
        {
            Console.WriteLine("\nInvoking My Service at {0}\n", serviceAddress);

            // Create a client
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.Endpoint.Address = serviceAddress;
            client.GetData(1);
        }
    }
}
