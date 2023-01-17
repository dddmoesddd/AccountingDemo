using AccountingVoucher.Domain.Entities;

namespace AccountingVoucher.Application.Features.Queries
{
	public class VoucherItemsVM
	{
		public string Description { get; set; }
		public decimal CreditorPrice { get; set; }
		public decimal DebtorPrice { get; set; }
		public decimal StreetPrice { get; set; }
		public bool IsDeleted { get; set; }
		public short VoucherType { get; set; }
	}
}
