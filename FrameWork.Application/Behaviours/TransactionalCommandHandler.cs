using FrameWork.Infrastruture.Persistance;
using MassTransit;
using MassTransit.Mediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Application.Behaviours
{
	public class TransactionalCommandHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{

		private readonly IUnitOfWork _unitOfWork;

		public TransactionalCommandHandler(IUnitOfWork unitOfWork)
		{

			_unitOfWork = unitOfWork;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{

			try
			{

				var response = await next();
				await _unitOfWork.Commit();
				return response;

			}
			catch (Exception ex)
			{
				var exeception = ex;
				_unitOfWork.Rollback();
				var response = await next();
				return response;
			}

		}
	}
}
