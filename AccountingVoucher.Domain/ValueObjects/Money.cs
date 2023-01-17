
using FrameWork.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingVoucher.Domain.ValueObjects
{
	public class Money : ValueObject
	{

		public decimal Value { get; private set; }

		public Money SetPrice(decimal price)
		{
			if (price < 0)
			{
				throw new Exception("sdsd");
			}
			this.Value = price;
			return this;

		}
		protected override IEnumerable<object> GetEqualityComponents()
		{
			throw new NotImplementedException();
		}
	}
}
