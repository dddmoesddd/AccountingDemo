using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Domain
{
	public abstract class EntityBase
	{
		public int Id { get; protected set; }
		public string? CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public string? LastModifiedBy { get; set; }
		public DateTime? LastModifiedDate { get; set; }

		private List<INotification>? _domainEvents;
		public IReadOnlyCollection<INotification>? DomainEvents =>
		_domainEvents?.AsReadOnly();

		public void AddDomainEvent(INotification eventItem)
		{
			_domainEvents = _domainEvents ?? new List<INotification>();
			_domainEvents.Add(eventItem);
		}
		public void RemoveDomainEvent(INotification eventItem)
		{
			_domainEvents?.Remove(eventItem);
		}
		public void ClearDomainEvents()
		{
			_domainEvents?.Clear();
		}
	}
}
