using AccountingVoucher.Application.Contracts.Persistance;
using AccountingVoucher.Application.Exception;
using AccountingVoucher.Application.Features.Queries.VoucherQuries.GetVoucherByVoucherNumber;
using AccountingVoucher.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AccountingVoucher.Application.Features.Queries.VoucherQuries.GetAllVoucher
{
    public class GetAllVoucherCommandHandler : IRequestHandler<GetAllVoucherQuery, List<VoucherVM>>
	{
		private readonly IVoucherRepository _voucherRepository;
		private readonly IMapper _mapper;

		public GetAllVoucherCommandHandler(IVoucherRepository orderRepository, IMapper mapper)
		{
			_voucherRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}
		public async Task<List<VoucherVM>> Handle(GetAllVoucherQuery request, CancellationToken cancellationToken)
		{
			var voucherLst = await _voucherRepository.GetAllVouchers();
			if (voucherLst==null)
			{
				throw new NotFoundException(nameof(GetAllVoucherQuery), "Voucher");
			}
			return MapToVoucherVN(voucherLst);
		}

		private List<VoucherVM> MapToVoucherVN(List<Voucher> vouchers)
		{
			var lst = new List<VoucherVM>();
			vouchers.ForEach(v =>
			{
				var obj = new VoucherVM();
				obj.VoucherNumber = v.VoucherNumber;
				obj.Description = (string.IsNullOrEmpty(v.Description.Value))?string.Empty: v.Description.Value;
				obj.IsBalance = v.IsBalance;
				obj.CreatedBy = v.CreatedBy;
				obj.CreatedDate = v.CreatedDate.ToShortDateString();
				obj.LastModifiedBy = v.LastModifiedBy;
				obj.LastModifiedData = v.LastModifiedDate.ToString();
				obj.WholePrice = v.VoucherItems.Sum(
					t =>
				t.CreditorPrice != null && t.CreditorPrice.Value != 0 ? t.CreditorPrice.Value :
				t.DebtorPrice != null && t.DebtorPrice.Value != 0 ? t.DebtorPrice.Value : 0);
				lst.Add(obj);
			});
			return lst;
		}
	}
}
