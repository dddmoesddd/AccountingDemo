using AccountingVoucher.Application.Contracts.Persistance;
using AccountingVoucher.Domain.Entities;
using AccountingVoucher.Infrastructure;
using AccountingVoucher.Infrastructure.Repositories;
using AutoFixture;
using FrameWork.Infrastruture.Persistance;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VouchersTests.EfCoreTest
{
    public class VoucherDataBaseTest
    {

        Mock<IVoucherRepository>? _voucherRepositoryMock;
        Mock<IRepositoryBase<Voucher>>? _repositryBase;
        public DbContextOptions<VoucherContext> Seup()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = ":memory" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());
            _voucherRepositoryMock = new Mock<IVoucherRepository>();
            _repositryBase = new Mock<IRepositoryBase<Voucher>>();
            return new DbContextOptionsBuilder<VoucherContext>().UseSqlite().Options; ;
        }
        [Fact]
        public async void GetVoucher_EmptyVoucherNumber_ShouldThrowException()
        {
            var option = Seup();
            using (var context = new VoucherContext(option))
            {

                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                _repositryBase = new Mock<IRepositoryBase<Voucher>>();

                var voucherrepository = new VoucherRepository(_repositryBase.Object, context);

                await Assert.ThrowsAsync<ArgumentException>(() => voucherrepository.GetVoucherByVoucherNumber(0));

            }
        }

        [Fact]
        public void DescriptionShouldBe_Saved_AsExpected()
        {
            var option = Seup();

            using (var context = new VoucherContext(option))
            {

                context.Database.OpenConnection();
                context.Database.EnsureCreated();
                var fixture = new Fixture();
                var sut = fixture.Build<Voucher>()
                    .FromFactory<int>((x) => new Voucher(GetVoucherSnapShotList(), "decription"))
                    .Create();

                context.Voucher.Add(sut);
                context.SaveChanges();
                _repositryBase = new Mock<IRepositoryBase<Voucher>>();
                var voucherrepository = new VoucherRepository(_repositryBase.Object, context);
                var voucher = voucherrepository.GetVoucherByVoucherNumber(sut.VoucherNumber);

                Assert.Equal("decription", voucher.Result.Description.Value);


            }
        }

        public List<VoucherSnapShot> GetVoucherSnapShotList()
        {
            var fixture = new Fixture();
            var lst = new List<VoucherSnapShot>();
            var fixtureSnapshotSUT = fixture.Build<VoucherSnapShot>().With(t => t.Price, 2322).With(t => t.VoucherItemType, 1).Create();
            var fixtureSnapshotSUTTwo = fixture.Build<VoucherSnapShot>().With(t => t.Price, 43434).With(t => t.VoucherItemType, 3).Create();
            lst.Add(fixtureSnapshotSUT);
            lst.Add(fixtureSnapshotSUTTwo);
            return lst;
        }
    }
}
