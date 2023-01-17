using MediatR;
using System;
using System.Collections.Generic;

namespace AccountingVoucher.Application.Features.Queries.VoucherItemsQuries
{
	public class GetVoucherItemsQuery: IRequest<List<VoucherItemsVM>>
	{
		public long VoucherNumber { get; set; }
		public GetVoucherItemsQuery(long voucherNumber)
		{

			VoucherNumber = voucherNumber == 0 ? throw new ArgumentNullException(nameof(voucherNumber)) : voucherNumber;
		}

	}

}
