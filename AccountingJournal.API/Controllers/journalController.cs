using AccountingJournal.Application.Contracts;
using AccountingJournal.Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AccountingJournal.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class journalController : ControllerBase
	{
		IJournalRepository _repository;


		public journalController(IJournalRepository repository)
		{
			_repository = repository;

		}

		[HttpGet(Name = "GetAllJournal")]
		[ProducesResponseType(typeof(IEnumerable<Journal>), (int)HttpStatusCode.OK)]
		public  ActionResult<IEnumerable<Journal>> GetAllVoucher()
		{
			var journal=	_repository.IRepositoryBase.GetAll();
			return Ok(journal) ;


		}
	}
}