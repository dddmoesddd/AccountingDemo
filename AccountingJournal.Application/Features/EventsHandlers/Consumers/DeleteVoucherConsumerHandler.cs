using AccountingJournal.Application.Contracts;
using FrameWork.Domain.Events.VoucherEvents;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingJournal.Application.Features.EventsHandlers.Consumers
{
	public  class DeleteVoucherConsumerHandler:IConsumer<DeleteVoucherEvent>
	{
		public DeleteVoucherConsumerHandler()
		{

		}
		IJournalRepository _repository;
		public DeleteVoucherConsumerHandler(IJournalRepository repository)
		{
			_repository = repository;
		}
		public Task Consume(ConsumeContext<DeleteVoucherEvent> context)
		{
			var journal = _repository.GetByVouchernumber(context.Message.VoucherNumber);
			if (journal!=null)
			{
				_repository.DeleteJournals(journal);
				_repository.SaveChange();
			}
			
			return Task.CompletedTask;
		}
	}
}
