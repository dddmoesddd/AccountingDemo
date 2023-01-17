using AccountingVoucher.Application.Contracts.Persistance;
using AccountingVoucher.Domain.Entities;
using AccountingVoucher.Domain.ValueObjects;
using FrameWork.Infrastruture.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingVoucher.Infrastructure.Repositories
{
	public class VoucherRepository : IVoucherRepository
	{

		public IRepositoryBase<Voucher> IRepositoryBase { get; set; }

		VoucherContext _context;
		public VoucherRepository(IRepositoryBase<Voucher> repository, VoucherContext context)
		{
			IRepositoryBase = repository;
			IRepositoryBase.SetDbContext(context);
			_context = context;
		}

		public void Add(Voucher entity)
		{
			IRepositoryBase.Add(entity);
		}

		public async Task<Voucher> GetVoucherByVoucherNumber(long voucherNumber)
		{
			return await _context.Voucher.Where(t => t.VoucherNumber == voucherNumber).FirstOrDefaultAsync();
		}

		public async Task<List<Voucher>> GetAllVouchers()
		{
			return await _context.Voucher.AsNoTracking().ToListAsync();

		}

		public void Delete(Voucher voucher)
		{
			IRepositoryBase.Delete(voucher);

		}

		public async Task<List<VoucherItems>> GetVoucherItemsByVoucherNumber(long voucherNumber)
		{
			return await _context.Voucher.AsNoTracking().Where(v => v.VoucherNumber == voucherNumber).SelectMany(t => t.VoucherItems).ToListAsync();
		}

		public void SetDbContext(DbContext context)
		{

		}
	}
}
