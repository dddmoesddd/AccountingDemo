using AccountingVoucher.Domain.Entities;
using FrameWork.Domain;
using System;
using System.Collections.Generic;

namespace AccountingVoucher.Domain.ValueObjects
{
	public class VoucherItems : ValueObject
	{
		private VoucherItems()
		{

		}
		public Description Description { get; private set; }
		public Money CreditorPrice { get; private set; }
		public Money DebtorPrice { get; private set; }
		public Money StreetPrice { get; private set; }

		public bool IsDeleted { get; private set; }
		public VoucherType VoucherType { get; private set; }

		public VoucherItems(decimal price, VoucherType type)
		{
			StreetPrice = new Money().SetPrice(price);
			VoucherType = type;
			DebtorPrice = (type == VoucherType.Debtor) ? new Money().SetPrice(price) : new Money().SetPrice(price); ;
			Description = new Description("jkhkj");
		}

		public void   DeleteVocherItems (VoucherItems item)
		{

			item.IsDeleted = true;
		}
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Description;
			yield return CreditorPrice;
			yield return DebtorPrice;


		}
	}
}
