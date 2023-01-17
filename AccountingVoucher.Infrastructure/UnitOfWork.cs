using FrameWork.Domain;
using FrameWork.Infrastruture.Persistance;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace AccountingVoucher.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{

		private TransactionScope scope;
		VoucherContext _context;
		private IMediator _mediator;
		public UnitOfWork(VoucherContext context, IMediator mediator)
		{
			_context = context;
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		public void Begin()
		{
			var txOptions = new TransactionOptions
			{ IsolationLevel = IsolationLevel.Serializable };
			scope = new TransactionScope(TransactionScopeOption.Required, txOptions);
		}

		public async Task Commit()
		{

			try
			{
				var entityDeleted = (object)null;

				var txOptions = new TransactionOptions { IsolationLevel = IsolationLevel.Serializable };

				using (scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
				{
					foreach (var entry in _context.ChangeTracker.Entries<EntityBase>())
					{
						if (entry.State == EntityState.Deleted)
						{
							entityDeleted = entry.Entity;

						}
					}
					await _context.SaveChangesAsync();
					if ((entityDeleted != null))
					{

						_context.Attach(entityDeleted);
					}

					await _mediator.DispatchDomainEventsAsync(_context);
					scope.Complete();
				}



			}
			catch (Exception ex)
			{
				var exception = ex;
				scope.Dispose();
			}




		}

		public void Rollback()
		{
			scope.Dispose();

		}
	}
}