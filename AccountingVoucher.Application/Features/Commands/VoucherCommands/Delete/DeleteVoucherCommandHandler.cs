using AccountingVoucher.Application.Contracts.Persistance;
using AccountingVoucher.Application.Exception;
using AccountingVoucher.Domain.Entities;
using AutoMapper;
using FrameWork.Infrastruture.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using static AccountingVoucher.Application.Features.Commands.VoucherCommands.Delete.DeleteVoucherCommandHandler;

namespace AccountingVoucher.Application.Features.Commands.VoucherCommands.Delete
{
	public class DeleteVoucherCommandHandler : IRequestHandler<DeleteVoucherCommand, DeletedResult>
	{
		private readonly IVoucherRepository _voucherRepository;
		private readonly ILogger<DeleteVoucherCommandHandler> _logger;
		private readonly IRepositoryBase<Voucher> _baseRepo;
		public DeleteVoucherCommandHandler(IVoucherRepository voucherRepository, IRepositoryBase<Voucher> repo,
			ILogger<DeleteVoucherCommandHandler> logger
			)
		{
			_voucherRepository = voucherRepository ?? throw new ArgumentNullException(nameof(voucherRepository));
			_baseRepo = repo ?? throw new ArgumentNullException(nameof(repo));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<DeletedResult> Handle(DeleteVoucherCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var voucher = await _voucherRepository.GetVoucherByVoucherNumber(request.VoucherNumber);
				if (voucher == null)
				{
					return await Task.FromResult(DeletedResult.Failed);

				}
			
				else if ( request.HasPerrmission)
				{
					var voucherDeleting = voucher.DeleteVocher(voucher);
					_baseRepo.Delete(voucherDeleting);
					return await Task.FromResult(DeletedResult.Sucess);
				}

				else if (request.HasPerrmission)
				{
					return await Task.FromResult(DeletedResult.Failed);
				}
			}
			catch (System.Exception ex)
			{
				////Todo implementation best approch for exception handeling
				return await Task.FromResult(DeletedResult.Failed);
			}

			return await 	Task.FromResult(DeletedResult.Failed);


		}


		public  enum DeletedResult
		{
			Failed,
			Sucess


		}
	}
}
