using AccountingJournal.Application.Contracts;
using AccountingJournal.Domain.Entities;
using FrameWork.Infrastruture.Persistance;
using Microsoft.EntityFrameworkCore;

namespace AccountingJournal.Infrastrcture.Repositories
{
	public class JournalRepository : IJournalRepository
	{

		JournalContext _context;
		public IRepositoryBase<Journal> IRepositoryBase { get; set; }
		public JournalRepository(IRepositoryBase<Journal> repository, JournalContext context)
		{
			IRepositoryBase = repository;
			IRepositoryBase.SetDbContext(context);
			_context = context;
		}




		public void AddList(List<Journal> lst)
		{

			_context.Journals.AddRange(lst);
			_context.SaveChanges();
		}


		public List<Journal> GetByVouchernumber(long voucherNumber)
		{
			return _context.Journals.Where(s => s.VoucherNumber == voucherNumber).ToList();
		}

		public void SaveChange()
		{
			_context.SaveChanges();
		}

		public void DeleteJournals(List<Journal> journals)
		{
			_context.Journals.RemoveRange(journals);
			_context.SaveChanges();
		}

		public void UpdateJournalS(List<Journal> journals)
		{
			if (journals!=null && journals.Count!=0)
			{
				var entity = _context.Journals.Where(t => t.VoucherNumber == journals.First().VoucherNumber).ToList();
				entity.ForEach(en => { _context.Entry(en).State = EntityState.Modified; });
				_context.SaveChanges();


			}
			
		}
	}
}
