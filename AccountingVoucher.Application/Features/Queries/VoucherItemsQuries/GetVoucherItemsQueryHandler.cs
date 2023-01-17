using AccountingVoucher.Application.Contracts.Persistance;
using AccountingVoucher.Application.Exception;
using AccountingVoucher.Application.Features.Queries.VoucherQuries.GetVoucherByVoucherNumber;
using AccountingVoucher.Domain.ValueObjects;
using AutoMapper;
using MassTransit.Transports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AccountingVoucher.Application.Features.Queries.VoucherItemsQuries
{
	class GetVoucherItemsQueryHandler : IRequestHandler<GetVoucherItemsQuery, List<VoucherItemsVM>>
	{
		private readonly IVoucherRepository _voucherRepository;
		private readonly IMapper _mapper;

		public GetVoucherItemsQueryHandler(IVoucherRepository orderRepository, IMapper mapper)
		{
			_voucherRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public async Task<List<VoucherItemsVM>> Handle(GetVoucherItemsQuery request, CancellationToken cancellationToken)
		{
			var vouocherList = await _voucherRepository.GetVoucherItemsByVoucherNumber(request.VoucherNumber);
			if (vouocherList == null)
			{
				throw new NotFoundException(nameof(vouocherList), request.VoucherNumber);
			}
			return MapVoucherItemsTovoucherList(vouocherList);

		}

		private List<VoucherItemsVM> MapVoucherItemsTovoucherList(List<VoucherItems> vouocherList)
		{
			var lst = new List<VoucherItemsVM>();
			vouocherList.ForEach(vitems =>
			{
				var obj = new VoucherItemsVM();
				obj.StreetPrice = vitems.StreetPrice.Value;
				obj.CreditorPrice = (vitems.CreditorPrice == null) ? 0 : vitems.CreditorPrice.Value;
				obj.DebtorPrice = (vitems.DebtorPrice == null) ? 0 : vitems.DebtorPrice.Value;
				obj.IsDeleted = vitems.IsDeleted;
				obj.Description = (string.IsNullOrEmpty(vitems.Description.Value)) ? string.Empty : vitems.Description.Value;
				obj.VoucherType = (short)vitems.VoucherType;

				lst.Add(obj);
			});
			return lst;
		}
	}
}
