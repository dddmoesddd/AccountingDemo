using FrameWork.Domain.Events.VoucherEvents;
using FrameWork.Infrastruture.MessageBrokerConfigorations.Rabbitmq.Voucher;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AccountingVoucher.Application.Features.Events.Voucher
{
	public class AddVoucherEventHandler : INotificationHandler<AddVoucherEvent>
	{
		IBus _bus;
		public AddVoucherEventHandler(IBus bus)
		{
			_bus = bus ?? throw new ArgumentNullException(nameof(bus));
		}
		public async Task Handle(AddVoucherEvent notification, CancellationToken cancellationToken)
		{
			try
			{

				Uri uri = new Uri(AddVoucherRabbitmqConfig.RabbitMqAddUriWithoutContainer);
				var endPoint = await _bus.GetSendEndpoint(uri);
				await endPoint.Send(notification);
			}
			catch (System.Exception ex)
			{
				var ss = ex;
			}


		}
	}
}
