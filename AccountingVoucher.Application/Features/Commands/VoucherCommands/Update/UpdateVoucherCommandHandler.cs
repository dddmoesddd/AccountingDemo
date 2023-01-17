using AccountingVoucher.Application.Contracts.Persistance;
using AccountingVoucher.Application.Features.Commands.VoucherCommands.Add;
using AccountingVoucher.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using FrameWork.Infrastruture.Persistance;
using AccountingVoucher.Application.Features.Commands.VoucherCommands.Delete;
using MediatR;
using AccountingVoucher.Application.Exception;

namespace AccountingVoucher.Application.Features.Commands.VoucherCommands.Update
{
    public class UpdateVoucherCommandHandler :IRequestHandler<UpdateVoucherCommand, bool>
	{
		private readonly IVoucherRepository _voucherRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<UpdateVoucherCommand> _logger;
		private readonly IRepositoryBase<Voucher> _baseRepo;
		public UpdateVoucherCommandHandler(IVoucherRepository voucherRepository, IRepositoryBase<Voucher> repo,
			ILogger<UpdateVoucherCommand> logger,
			IMapper mapper)
		{
			_voucherRepository = voucherRepository ?? throw new ArgumentNullException(nameof(voucherRepository));
			_baseRepo = repo ?? throw new ArgumentNullException(nameof(repo));
			mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<bool> Handle(UpdateVoucherCommand request, CancellationToken cancellationToken)
		{

			var voucher = await  _voucherRepository.GetVoucherByVoucherNumber(request.VoucherNumber);
			if (voucher == null)
			{
				throw new NotFoundException(nameof(voucher), request.VoucherNumber);
			}
			var updatedVoucher=voucher.UpdateVoucher(voucher, request.VoucherNumber, request.Description);
			_baseRepo.Update(updatedVoucher);

			return await Task.FromResult(true); ;
		}

	}
}
