using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Server.Host
{
	class Program
	{
		static void Main(string[] args)
		{
			var baseAddress = new Uri("http://localhost:8733/server/subscriber/");
			using (var host = new ServiceHost(typeof(TimedConcurrentQueueSubscriber), baseAddress))
			{
				var serviceMetaDataBehavior = new ServiceMetadataBehavior
				{
					HttpGetEnabled = true,
					HttpsGetEnabled = true
				};
				host.Description.Behaviors.Add(serviceMetaDataBehavior);

				var serviceDebugBehavior = new ServiceDebugBehavior
				{
					IncludeExceptionDetailInFaults = true
				};
				if (host.Description.Behaviors.Find<ServiceDebugBehavior>() == null)
					host.Description.Behaviors.Add(serviceDebugBehavior);

				var duplexBinding = new WSDualHttpBinding
				{
					ClientBaseAddress = new Uri("http://localhost:8734/client/dispatcher"),
					MaxBufferPoolSize = 5242880,
					MaxReceivedMessageSize = 6553600
				};
				var serviceEndpoint = new ServiceEndpoint(
					ContractDescription.GetContract(typeof(Subscriber)), 
					duplexBinding, new EndpointAddress(baseAddress));
				host.Description.Endpoints.Add(serviceEndpoint);

				host.Open();

				Console.WriteLine($"{nameof(Subscriber)} service listening on {baseAddress}");
				Console.WriteLine("Press any key to exit....");
				Console.ReadKey(true);

				host.Close();
			}
		}
	}
}
