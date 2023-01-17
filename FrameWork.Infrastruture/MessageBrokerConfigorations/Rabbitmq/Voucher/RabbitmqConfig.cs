using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Infrastruture.MessageBrokerConfigorations.Rabbitmq.Voucher
{
	public class AddVoucherRabbitmqConfig : RabbitBase
	{

		public const string AddVoucherQueue = "addvoucher-queue";
		public const string RabbitMqAddUriWithoutContainer = "amqp://localhost/addvoucher-queue";
	}
	public class UpdateVoucherRabbitmqConfig : RabbitBase
	{
		public const string UpdateVoucherQueue = "updatevoucher-queue";
		public const string RabbitUpdateMqUriWithoutContainer = "amqp://localhost/updatevoucher-queue";
	}

	public class DeleteVoucherRabbitmqConfig : RabbitBase
	{
		public const string DeleteVoucherQueue = "deletevoucher-queue";
		public const string RabbitMqDeleteUriWithoutContainer = "amqp://localhost/deletevoucher-queue";
	}
}
//docker run --rm -it -p 15672:15672 - p 5672:5672 rabbitmq: 3 - management