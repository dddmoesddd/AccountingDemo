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
	public class UpdateVocherConsumerHandler : IConsumer<UpdateVoucherEvent>
	{
		public UpdateVocherConsumerHandler()
		{

		}
		IJournalRepository _repository;
		public UpdateVocherConsumerHandler(IJournalRepository repository)
		{
			_repository = repository;
		}
		public Task Consume(ConsumeContext<UpdateVoucherEvent> context)
		{
			var journal = _repository.GetByVouchernumber(context.Message.VoucherNumber);
			journal.ForEach(e => { e.Description = context.Message.Description; });
			if (journal!=null)
			{
				_repository.UpdateJournalS(journal);
			}
			return Task.CompletedTask;
		}
	}
}
