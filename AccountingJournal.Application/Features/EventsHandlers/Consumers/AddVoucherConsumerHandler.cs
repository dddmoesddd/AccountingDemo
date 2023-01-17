using AccountingJournal.Application.Contracts;
using AccountingJournal.Domain.Entities;
using FrameWork.Domain.Events.VoucherEvents;
using FrameWork.Infrastruture.MessageBrokerConfigorations.Rabbitmq.Voucher;
using FrameWork.Infrastruture.Persistance;
using MassTransit;
using MassTransit.JobService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingJournal.Application.Features.EventsHandlers.Consumers
{
	public class AddVoucherConsumer : IConsumer<AddVoucherEvent>
	{
		public AddVoucherConsumer()
		{

		}
		IJournalRepository _repository;
		public AddVoucherConsumer(IJournalRepository repository)
		{
			_repository = repository;
		}

		public Task Consume(ConsumeContext<AddVoucherEvent> context)
		{

			var lst = new List<Journal>();
			context?.Message?.VoucherItems?.ForEach(t => {
				var obj = new Journal();
				obj.VoucherNumber = context.Message.VoucherNumber;
				obj.CreatedDate = context.Message.CreationDate;
				obj.Description =( context.Message.Description==null)?string.Empty: context.Message.Description;
				obj.CreatedBy = context.Message.CreatedBy;
				obj.DebtorPrice = t.DebtorPrice;
				obj.CreditorPrice = t.CreditorPrice; ;
		        lst.Add(obj);
			});
			_repository.AddList(lst);
			return Task.CompletedTask;
		}
	}
}

