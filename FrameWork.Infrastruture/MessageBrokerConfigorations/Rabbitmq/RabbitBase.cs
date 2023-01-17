using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Infrastruture.MessageBrokerConfigorations.Rabbitmq
{
	public  class RabbitBase
	{
		public const string RabbitMqRootUriWithOutContainerRoot = "rabbitmq://localhost:5672";
		public const string RabbitMqRootUriWithOutContainer = "amqp://localhost:5672";
		public const string UserName = "guest";
		public const string Password = "guest";
		public const string NotificationServiceQueue = "notification.service";
	}
}
