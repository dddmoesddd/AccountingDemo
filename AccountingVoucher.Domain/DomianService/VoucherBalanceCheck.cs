using AccountingVoucher.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingVoucher.Domain.DomianService
{
	public  class VoucherBalanceCheck: IVoucherBalancDomainService
	{
		public  bool   IsVoucherBalance(List<VoucherItems> Vlist)
		{

			var sumdeb = Vlist.Sum(t =>(t.DebtorPrice!=null)? t.DebtorPrice.Value:0);
			var deumcre = Vlist.Sum(t => (t.CreditorPrice != null)? t.CreditorPrice.Value:0);
			return true;
		}
	}
}
