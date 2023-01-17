using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Domain.Events.VoucherEvents
{
	public  class VoucherItemsDTO
	{
		public decimal Price { get; set; }
		public short VoucherItemType { get; set; }
		public double VoucherNumber { get; set; }
		public string? CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public string? Description { get; set; }
		public decimal CreditorPrice { get;  set; }
		public decimal DebtorPrice { get;  set; }
		public decimal StreetPrice { get;  set; }
		public bool IsBalance { get; set; }
	}
}
