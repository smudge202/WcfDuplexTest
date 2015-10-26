using System;
using System.ServiceModel;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Press any key to begin client test...");
			Console.ReadKey(true);
			var context = new InstanceContext(new EventBusDispatcher());
			using (var wcfClient = new Server.SubscriberClient(context))
			{
				Console.WriteLine("Connection established to server.");

				Console.WriteLine($"Subscribing for {typeof(string)} Events");
				var subscription = wcfClient.Subscribe(Guid.NewGuid().ToString());
				Console.WriteLine($"Subscription received: {subscription}");

				Console.WriteLine("Press any key to close connection...");
				Console.ReadKey(true);
				wcfClient.Close();
			}
		}
	}
}
