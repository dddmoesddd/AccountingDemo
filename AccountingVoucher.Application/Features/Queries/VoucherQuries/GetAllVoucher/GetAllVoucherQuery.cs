using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingVoucher.Application.Features.Queries.VoucherQuries.GetAllVoucher
{
    public  class GetAllVoucherQuery : IRequest<List<VoucherVM>>
	{

	}
}
