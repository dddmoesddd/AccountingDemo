using FrameWork.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Domain.Events.VoucherEvents
{
	public class AddVoucherEvent : BaseEvent, INotification
	{
		public AddVoucherEvent()
		{

		}
		public long VoucherNumber { get; set; }

		public string? Description { get; set; }

		public string CreatedBy { get; set; }
		public List<VoucherItemsDTO>? VoucherItems { get; set; }
		public AddVoucherEvent(long voucherNumbder, string description, DateTime createdDate, string createdBy, List<VoucherItemsDTO> items)
		{
			this.VoucherNumber = voucherNumbder;
			this.Description = description;
			this.VoucherItems = items;
			this.CreatedBy = createdBy;
		}

	}
}
