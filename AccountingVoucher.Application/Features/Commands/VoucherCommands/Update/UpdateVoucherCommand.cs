using AccountingVoucher.Domain.Entities;
using MassTransit.Mediator;
using MediatR;
using System.Collections.Generic;

namespace AccountingVoucher.Application.Features.Commands.VoucherCommands.Update
{
    public class UpdateVoucherCommand: IRequest<bool>
	{
		public string Description { get; set; }

		public long VoucherNumber { get; set; }
		public UpdateVoucherCommand(string description, long voucherNumber)
		{
			Description = description;
			VoucherNumber = voucherNumber;
		}
	}
}
