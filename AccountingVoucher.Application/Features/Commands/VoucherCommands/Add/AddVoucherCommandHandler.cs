using AccountingVoucher.Application.Contracts.Persistance;
using AccountingVoucher.Application.Exception;
using AccountingVoucher.Domain.Entities;
using AutoMapper;
using FrameWork.Infrastruture.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AccountingVoucher.Application.Features.Commands.VoucherCommands.Add
{
	public class AddVouchercommandHandler : IRequestHandler<AddVoucherCommand, int>
	{
		private readonly IVoucherRepository _voucherRepository;
		private readonly IRepositoryBase<Voucher> _baseRepo;

		private readonly IMapper _mapper;
		private readonly ILogger<AddVouchercommandHandler> _logger;

		public AddVouchercommandHandler(IVoucherRepository voucherRepository, IRepositoryBase<Voucher> repo,
			ILogger<AddVouchercommandHandler> logger
			)
		{
		    _voucherRepository = voucherRepository ?? throw new ArgumentNullException(nameof(voucherRepository));
			_baseRepo = repo ?? throw new ArgumentNullException(nameof(repo));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public Task<int> Handle(AddVoucherCommand request, CancellationToken cancellationToken)
		{

			if (request == null)
			{
				throw new NotFoundException(nameof(request), request.VoucherSnapShots);
			}

			var voucher = new Voucher(request.VoucherSnapShots, request.Description);
			if (!voucher.IsBalance)
			{
				throw new DomainIsNotBalanceException();
			}
			var result= _baseRepo.Add(voucher);
			return Task.FromResult(1); ;
		}


	}
}
