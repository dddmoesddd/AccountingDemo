using AccountingJournal.Domain.Entities;
using FrameWork.Infrastruture.Persistance;
using Microsoft.EntityFrameworkCore;

namespace AccountingJournal.Application.Contracts
{
	public interface IJournalRepository
	{
		IRepositoryBase<Journal> IRepositoryBase { get; set; }
		void AddList(List<Journal> lst);
		List<Journal> GetByVouchernumber(long voucherNumber);
		void DeleteJournals(List<Journal> journals);
		void UpdateJournalS(List<Journal> journals);
		void SaveChange();
	}
}
