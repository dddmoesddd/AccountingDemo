using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingVoucher.Application.Exception
{
	public class NotFoundException : ApplicationException
	{
		public NotFoundException(string name, object key)
	  : base($"Entity \"{name}\" ({key}) was not found.")
		{
		}
	}

	public class DomainIsNotBalanceException : ApplicationException
	{
		public DomainIsNotBalanceException()
	  : base($"Voucher Is Not Balance")
		{
		}
	}
}
