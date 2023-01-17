using MediatR;
using System;
using System.Collections.Generic;

namespace AccountingVoucher.Application.Features.Queries.VoucherQuries.GetVoucherByVoucherNumber
{
    public class GetVouchersQuery : IRequest<VoucherVM>
	{

		public long VoucherNumber { get; set; }
		public GetVouchersQuery(long voucherNumber)
		{

			VoucherNumber = voucherNumber == 0 ? throw new ArgumentNullException(nameof(voucherNumber)) : voucherNumber;
		}


	}
}
