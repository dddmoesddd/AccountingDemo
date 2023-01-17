using System.Threading;

namespace AccountingVoucher.Domain.Entities
{
	public  class VoucherSnapShot
	{
	
		public decimal Price { get; set; }

		public short  VoucherItemType { get; set; }



		public VoucherSnapShot(decimal price, short voucherItemType)
		{
			Price = price;
			VoucherItemType = voucherItemType;
		}
	}
}
