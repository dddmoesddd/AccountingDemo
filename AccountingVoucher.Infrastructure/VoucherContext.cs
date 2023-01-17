using AccountingVoucher.Domain.Entities;
using AccountingVoucher.Infrastructure.ConfigorationEntity;
using FrameWork.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AccountingVoucher.Infrastructure
{
	public class VoucherContext: DbContext
	{

		public VoucherContext()
		{
				
		}
		public VoucherContext(DbContextOptions<VoucherContext> options) : base(options)
		{
		
		}

		public DbSet<Voucher> Voucher { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new VoucherConfiguration());
			foreach (var relationship in modelBuilder.Model.GetEntityTypes().Where(e => e.IsOwned()).SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Cascade;
			}

		}
		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			try
			{
				foreach (var entry in ChangeTracker.Entries<EntityBase>())
				{
					switch (entry.State)
					{
						case EntityState.Added:
							entry.Entity.CreatedDate = DateTime.Now;
							entry.Entity.CreatedBy = "swn";
							break;
						case EntityState.Modified:
							entry.Entity.LastModifiedDate = DateTime.Now;
							entry.Entity.LastModifiedBy = "swn";
							break;
					}
				}
	
			return await base.SaveChangesAsync(cancellationToken);
		
		
			}
			catch (Exception ex)
			{
				//Todo ShouleBeImplement Best ApproachFor ExceptionHandeling
				throw;
			}
		
		}
	}
}
