using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Domain.Events.VoucherEvents
{
    public  class UpdateVoucherEvent:BaseEvent, INotification
    {
		public long VoucherNumber { get; set; }
		public string Description { get; set; }
		public UpdateVoucherEvent()
        {

        }
		public UpdateVoucherEvent( long voucherNumber, string description)
		{

			VoucherNumber = voucherNumber;
			Description = description;
		}
	}
}
