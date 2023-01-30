using AccountingVoucher.Application.Contracts.Persistance;
using AccountingVoucher.Domain.Entities;
using AccountingVoucher.Infrastructure;
using AccountingVoucher.Infrastructure.Repositories;
using FrameWork.Infrastruture.Persistance;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VouchersTests
{
	public class VoucherDataBaseTest
	{


		public  DbContextOptions<VoucherContext> Seup()
		{
			var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = ":memory" };
			var connection = new SqliteConnection(connectionStringBuilder.ToString());
			return new DbContextOptionsBuilder<VoucherContext>().UseSqlite().Options; ;
		}
		[Fact]
		public async  void  GetVoucher_EmptyVoucherNumber_ShouldThrowException()
		{
			var option = Seup();
			using (var  context=new VoucherContext(option))
			{
			
				context.Database.OpenConnection();
				context.Database.EnsureCreated();
		
			var repositryBase = new Mock<IRepositoryBase<Voucher>>();
			var	_voucherRepositoryMock = new Mock<IVoucherRepository>();
			var voucherrepository = new VoucherRepository(repositryBase.Object, context);

		    await    Assert.ThrowsAsync<ArgumentException>(() => voucherrepository.GetVoucherByVoucherNumber(0));

			}
		}
	}
}
