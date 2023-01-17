using MediatR;
using static AccountingVoucher.Application.Features.Commands.VoucherCommands.Delete.DeleteVoucherCommandHandler;

namespace AccountingVoucher.Application.Features.Commands.VoucherCommands.Delete
{
	public class DeleteVoucherCommand : IRequest<DeletedResult>
	{
		public long VoucherNumber { get; set; }

		public bool HasPerrmission { get; set; }
		public DeleteVoucherCommand(long voucherNumber,bool hasperrmission)
		{
			VoucherNumber = voucherNumber;
			HasPerrmission= hasperrmission;
		}
	}
}
