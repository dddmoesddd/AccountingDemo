using AccountingVoucher.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace AccountingVoucher.Application.Features.Commands.VoucherCommands.Add
{
    public class AddVoucherCommand : IRequest<int>
    {
        public string Description { get; set; }
        public List<VoucherSnapShot> VoucherSnapShots { get; set; }
    }
}
