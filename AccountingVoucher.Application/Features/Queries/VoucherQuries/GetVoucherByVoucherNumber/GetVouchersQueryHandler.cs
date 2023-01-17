using AccountingVoucher.Application.Contracts.Persistance;
using AccountingVoucher.Application.Exception;
using AccountingVoucher.Application.Features.Queries.VoucherQuries.GetAllVoucher;
using AccountingVoucher.Domain.Entities;
using AutoMapper;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AccountingVoucher.Application.Features.Queries.VoucherQuries.GetVoucherByVoucherNumber
{
	public class GetVouchersQueryHandler : IRequestHandler<GetVouchersQuery, VoucherVM>
	{
		private readonly IVoucherRepository _voucherRepository;
		private readonly IMapper _mapper;

		public GetVouchersQueryHandler(IVoucherRepository orderRepository, IMapper mapper)
		{
			_voucherRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public async Task<VoucherVM> Handle(GetVouchersQuery request, CancellationToken cancellationToken)
		{
			var voucher = await _voucherRepository.GetVoucherByVoucherNumber(request.VoucherNumber);
			if (voucher==null)
			{
				throw new NotFoundException(nameof(GetVouchersQuery), request.VoucherNumber);
			}
			return MapToVoucherVN(voucher);


		}

		private VoucherVM MapToVoucherVN(Voucher voucher)
		{


			var obj = new VoucherVM();
			obj.VoucherNumber = voucher.VoucherNumber;
			obj.Description = voucher.Description.Value;
			obj.IsBalance = voucher.IsBalance;
			obj.CreatedBy = voucher.CreatedBy;
			obj.CreatedDate = voucher.CreatedDate.ToShortDateString();
			obj.LastModifiedBy = voucher.LastModifiedBy;
			obj.LastModifiedData = voucher.LastModifiedDate.ToString();
			obj.WholePrice = voucher.VoucherItems.Sum(
				t =>
			t.CreditorPrice != null && t.CreditorPrice.Value != 0 ? t.CreditorPrice.Value :
			t.DebtorPrice != null && t.DebtorPrice.Value != 0 ? t.DebtorPrice.Value : 0);
			obj.VoucherItems = new List<VoucherItemsVM>();
			voucher.VoucherItems.ToList().ForEach(v =>
			{
				var voucherItemsVM = new VoucherItemsVM();
				voucherItemsVM.IsDeleted = v.IsDeleted;
				voucherItemsVM.StreetPrice = (v.StreetPrice==null)?0:v.StreetPrice.Value;
				voucherItemsVM.CreditorPrice =(v.CreditorPrice==null)?0: v.CreditorPrice.Value;
				voucherItemsVM.DebtorPrice =(v.DebtorPrice==null)?0: v.DebtorPrice.Value;
				voucherItemsVM.VoucherType =(short) v.VoucherType;
				obj.VoucherItems.Add(voucherItemsVM);

			});

			return obj;
		}
	}
}
