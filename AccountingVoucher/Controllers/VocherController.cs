using AccountingVoucher.Application.Features.Commands.VoucherCommands.Add;
using AccountingVoucher.Application.Features.Commands.VoucherCommands.Delete;
using AccountingVoucher.Application.Features.Commands.VoucherCommands.Update;
using AccountingVoucher.Application.Features.Queries;
using AccountingVoucher.Application.Features.Queries.VoucherItemsQuries;
using AccountingVoucher.Application.Features.Queries.VoucherQuries.GetAllVoucher;
using AccountingVoucher.Application.Features.Queries.VoucherQuries.GetVoucherByVoucherNumber;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AccountingVoucher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocherController : ControllerBase
    {
        private readonly IMediator _mediator;


		public VocherController(IMediator mediator, IBus bus)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

		}

        [HttpGet("{VoucherNumber}",Name = "GetVouchersByVoucherNumber")]
        [ProducesResponseType(typeof(VoucherVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<VoucherVM>> GetVouchersByVoucherNumber(long VoucherNumber)
        {
            var query = new GetVouchersQuery(VoucherNumber);
            var voucher = await _mediator.Send(query);
            return Ok(voucher);
        }

		[HttpGet(Name = "GetAllVoucher")]
		[ProducesResponseType(typeof(IEnumerable<VoucherVM>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<VoucherVM>>> GetAllVoucher()
		{
			var query = new GetAllVoucherQuery();
			var vouchers = await _mediator.Send(query);
			return Ok(vouchers);
		}

		[Route("[action]/{VoucherNumber}", Name = "GetAllVoucherItems")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VoucherItemsVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<VoucherItemsVM>>> GetAllVoucherItems(long VoucherNumber)
        {
            var query = new GetVoucherItemsQuery( VoucherNumber);
            var voucherItems = await _mediator.Send(query);
            return Ok(voucherItems);
        }



        [HttpPost(Name = "AddVoucher")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> Add([FromBody] AddVoucherCommand command)
        {

	      	var result = await _mediator.Send(command);
            return Ok();
        }

        [HttpPut(Name = "UpdateVoucher")]
		[ProducesResponseType(typeof(UpdateVoucherCommand), (int)HttpStatusCode.OK)]
		public async Task<ActionResult> Update([FromBody] UpdateVoucherCommand command)
        {
            await _mediator.Send(command);
			return Ok();
		}

        [HttpDelete("{VoucherNumber}", Name = "DeleteVoucher")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<ActionResult> Delete(long VoucherNumber, bool haspermission)
        {
            var command = new DeleteVoucherCommand(VoucherNumber,haspermission);
            await _mediator.Send(command);
			return Ok();
		}
    }
}
