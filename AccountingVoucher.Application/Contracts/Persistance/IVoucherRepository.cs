using AccountingVoucher.Domain.Entities;
using AccountingVoucher.Domain.ValueObjects;
using FrameWork.Infrastruture.Persistance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingVoucher.Application.Contracts.Persistance
{
	public interface IVoucherRepository
	{

		IRepositoryBase<Voucher> IRepositoryBase { get; set; }
		Task<List<Voucher>> GetAllVouchers();
		Task<Voucher> GetVoucherByVoucherNumber(long voucher);

		Task<List<VoucherItems>> GetVoucherItemsByVoucherNumber(long voucher);
	

	}
}
