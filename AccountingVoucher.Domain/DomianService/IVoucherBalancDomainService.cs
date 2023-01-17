using AccountingVoucher.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingVoucher.Domain.DomianService
{
	public  interface IVoucherBalancDomainService
	{
		bool IsVoucherBalance(List<VoucherItems> Vlist);
	}
}
