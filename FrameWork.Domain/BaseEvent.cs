using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Domain
{
	public class BaseEvent
	{
		public BaseEvent()
		{
			Id = Guid.NewGuid();
			CreationDate = DateTime.UtcNow;
		}

		public BaseEvent(Guid id, DateTime createDate)
		{
			Id = id;
			CreationDate = createDate;
		}

		public Guid Id { get;  set; }

		public DateTime CreationDate { get;  set; }
	}
}
