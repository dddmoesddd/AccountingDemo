using FrameWork.Domain;
using FrameWork.Domain.Events.VoucherEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingJournal.Domain.Entities
{
	public class Journal : EntityBase, IAggregateRoot
	{

		public long VoucherNumber { get; set; }
		public string? Description { get; set; }
		public decimal CreditorPrice { get; set; }
		public decimal DebtorPrice { get; set; }


	}
}
