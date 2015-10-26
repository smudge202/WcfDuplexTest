using System;

namespace Client
{
	internal sealed class EventBusDispatcher : Server.SubscriberCallback
	{
		public void Dispatch(string message)
		{
			Console.WriteLine($"Message received:\r\n\t{message}");
		}
	}
}
