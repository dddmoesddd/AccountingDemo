using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Domain.Events.VoucherEvents
{
    public  class DeleteVoucherEvent:BaseEvent, INotification
    {

		public long VoucherNumber { get; set; }
        public DeleteVoucherEvent()
        {

        }
		public DeleteVoucherEvent(long voucherNumbder)
		{
			this.VoucherNumber = voucherNumbder;
	
		}
	}
}
