using FrameWork.Domain.Events.VoucherEvents;
using FrameWork.Infrastruture.MessageBrokerConfigorations.Rabbitmq.Voucher;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AccountingVoucher.Application.Features.Events.Voucher
{
	public class UpdateVoucherEventhandler : INotificationHandler<UpdateVoucherEvent>
	{
		IBus _bus;
		public UpdateVoucherEventhandler(IBus bus)
		{
			_bus = bus ?? throw new ArgumentNullException(nameof(bus));
		}
		public async Task Handle(UpdateVoucherEvent notification, CancellationToken cancellationToken)
		{
			try
			{

				Uri uri = new Uri(UpdateVoucherRabbitmqConfig.RabbitUpdateMqUriWithoutContainer);
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
