

using AccountingVoucher.Domain.DomianService;
using AccountingVoucher.Domain.ValueObjects;
using FrameWork.Domain;
using FrameWork.Domain.Events.VoucherEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace AccountingVoucher.Domain.Entities
{
	public class Voucher : EntityBase, IAggregateRoot
	{

		IVoucherBalancDomainService _service;

		private Voucher()
		{
			_service = new VoucherBalanceCheck();
		}
		private readonly List<VoucherItems> _voucherItems = new List<VoucherItems>();
		public IReadOnlyList<VoucherItems> VoucherItems => _voucherItems.AsReadOnly();
	    public	bool IsDeleted { get; private set; } 
		public long VoucherNumber { get; private set; }
	
		public Description Description { get; private set; }
		public bool IsBalance { get; private set; }
		public Voucher(List<VoucherSnapShot> vshot, string description)
		{

			//Todo
			//checkInvarients
			
			AddVoucher(vshot, description);

		}

		public void AddVoucher(List<VoucherSnapShot> vshot, string description)
		{
			VoucherNumber = new Random().NextInt64();
			this.Description = new Description(description);

			AddVoucherItem(vshot);
			//ToDo
			////  Domain Service Should call in raised event
			if (IsBalance = IsVoucherBalance())
			{
				AddVoucherEvent();


			}
		}

		private void AddVoucherEvent()
		{
			var items = new List<VoucherItemsDTO>();

			_voucherItems.ForEach(t =>
			{

				var obj = new VoucherItemsDTO();

				obj.VoucherItemType = (short)t.VoucherType;
				obj.DebtorPrice = (t.DebtorPrice == null) ? 0 :t.DebtorPrice.Value;
				obj.CreditorPrice = (t.CreditorPrice == null)?0: t.CreditorPrice.Value;
				obj.StreetPrice = (t.StreetPrice == null)?0: t.StreetPrice.Value;
				items.Add(obj);

			});
			AddDomainEvent(new AddVoucherEvent(this.VoucherNumber, this.Description.Value, this.CreatedDate, this.CreatedBy, items));
		}

		public Voucher(IVoucherBalancDomainService services)
		{
			_service = services;
		}

		private void AddVoucherItem(List<VoucherSnapShot> vshot)
		{

			vshot.ForEach(v =>
			{

				_voucherItems.Add(new VoucherItems(v.Price, (VoucherType)v.VoucherItemType));
			});


		}

		public  Voucher UpdateVoucher(Voucher voucher, long voucherNumber,string description)
		{
			this.VoucherNumber= voucherNumber;
			this.Description= new Description(description);
			AddDomainEvent(new UpdateVoucherEvent(this.VoucherNumber,this.Description.Value ));
			return this;
		}



		public Voucher DeleteVocher(Voucher voucher)
		{

			voucher.IsDeleted = true;
			foreach (var item in voucher.VoucherItems)
			{
				item.DeleteVocherItems(item);
			}
			//ToDo any bussines need while delete
	
			voucher.AddDomainEvent(new DeleteVoucherEvent(this.VoucherNumber));
			return voucher;
		}

		public virtual bool IsVoucherBalance()
		{

			if (_service==null)
			{
				_service = new VoucherBalanceCheck();
			}
			return _service.IsVoucherBalance(this._voucherItems);

		}

	}

	public enum VoucherType
	{

		Creditor,
		Debtor

	}
}
