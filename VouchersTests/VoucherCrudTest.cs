using AccountingVoucher.Application.Contracts.Persistance;
using AccountingVoucher.Application.Exception;
using AccountingVoucher.Application.Features.Commands.VoucherCommands.Add;
using AccountingVoucher.Application.Features.Commands.VoucherCommands.Delete;
using AccountingVoucher.Domain.DomianService;
using AccountingVoucher.Domain.Entities;
using AutoFixture;
using AutoFixture.AutoMoq;
using FrameWork.Infrastruture.Persistance;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using static AccountingVoucher.Application.Features.Commands.VoucherCommands.Delete.DeleteVoucherCommandHandler;

namespace VouchersTests
{
	public class VoucherCrudTest
	{

		Mock<IVoucherRepository>? _voucherRepositoryMock;
		Mock<IRepositoryBase<Voucher>>? _repositryBase;
		Mock<ILogger<AddVouchercommandHandler>>? _addVoucherIloggerMock;
		Mock<ILogger<DeleteVoucherCommandHandler>>? _dellVoucherIloggerMock;
		AddVouchercommandHandler _addcommandHandler;
		DeleteVoucherCommandHandler _deletecommandhandler;
		CancellationToken _cancelationToken;

		public void Setup()
		{
			_voucherRepositoryMock = new Mock<IVoucherRepository>();
			_repositryBase = new Mock<IRepositoryBase<Voucher>>();
			_addVoucherIloggerMock = new Mock<ILogger<AddVouchercommandHandler>>();
			_addcommandHandler = new AddVouchercommandHandler(_voucherRepositoryMock.Object, _repositryBase.Object, _addVoucherIloggerMock.Object);
			_dellVoucherIloggerMock = new Mock<ILogger<DeleteVoucherCommandHandler>>();
			_cancelationToken = new CancellationToken();
		}
		[Fact]
		public void Should_ThrowException_IfVoucherInputIsNull_WhenSaveVoucher()
		{
			Setup();
			var commandHandler = new AddVouchercommandHandler(_voucherRepositoryMock.Object, _repositryBase.Object, _addVoucherIloggerMock.Object);
			Assert.ThrowsAnyAsync<NotFoundException>(() => _addcommandHandler.Handle(null, _cancelationToken));
		}

		[Fact]
		public void Shoulde_SaveVoucher()
		{
			Setup();
			var command = new AddVoucherCommand() { VoucherSnapShots = GetVoucherSnapShotList(), Description = "jh" };
			_addcommandHandler.Handle(command, _cancelationToken);
			_repositryBase.Setup(x => x.Add(It.IsAny<Voucher>()));
			_repositryBase.Verify(x => x.Add(It.IsAny<Voucher>()), Times.Once);
		}


		[Theory]
		[InlineData(true, DeletedResult.Sucess)]
		[InlineData(false, DeletedResult.Failed)]
		public void ShouldReturnExpectedResultCodeOnDelete(bool haspermission, DeletedResult expected)
		{
			Setup();
			var command = new DeleteVoucherCommand(4232434234, haspermission);
			var fixture = new Fixture();
			fixture.Register<IVoucherBalancDomainService>(() => null);
			var sut = fixture.Build<Voucher>().Create();
			_deletecommandhandler = new DeleteVoucherCommandHandler(_voucherRepositoryMock.Object, _repositryBase.Object, _dellVoucherIloggerMock.Object);
			_voucherRepositoryMock.Setup(d => d.GetVoucherByVoucherNumber(It.IsAny<long>())).ReturnsAsync(sut);
			var result = _deletecommandhandler.Handle(command, _cancelationToken);
			Assert.Equal(expected, result.Result);

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