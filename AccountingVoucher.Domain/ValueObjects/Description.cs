using FrameWork.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingVoucher.Domain.ValueObjects
{
	public  class Description:ValueObject
	{
		public Description() { }
		public string Value { get;private set; }
		public Description(string  value ) {

			if (string.IsNullOrEmpty(value))
			{
				throw new Exception("");
			}

		this.Value = value;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
	}
}
