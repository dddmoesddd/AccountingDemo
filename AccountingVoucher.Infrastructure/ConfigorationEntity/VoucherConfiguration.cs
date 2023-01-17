using AccountingVoucher.Domain.Entities;
using AccountingVoucher.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingVoucher.Infrastructure.ConfigorationEntity
{
	public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
	{
		public void Configure(EntityTypeBuilder<Voucher> builder)
		{
			builder.ToTable("Voucher");
			builder.Property(x => x.VoucherNumber).ValueGeneratedNever();
			builder.HasKey(x => x.VoucherNumber); ;
			builder.Ignore(x => x.Id);
			builder.Ignore(x => x.DomainEvents);
			builder.Property(x => x.IsBalance);
			builder.Property(x => x.LastModifiedBy);

			//	builder.Property(x=>x.VoucherItems).HasField("_VoucherItems")
			//.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);

			builder.OwnsOne(x => x.Description).Property(t => t.Value).HasColumnName("Description");
			builder.HasAnnotation("Sqlite:Autoincrement", true)
				.HasAnnotation("MySql:ValueGeneratedOnAdd", true)
				.HasAnnotation("SqlServer:ValueGenerationStrategy", Microsoft.EntityFrameworkCore.Metadata.SqlServerValueGenerationStrategy.IdentityColumn);

			builder.OwnsMany<VoucherItems>("VoucherItems", vodetail =>
			{

				vodetail.WithOwner().HasForeignKey("VoucherNumber");
				vodetail.HasKey("Id");
				vodetail.Property<int>("Id").ValueGeneratedOnAdd();
				vodetail.Property(t => t.VoucherType);
				vodetail.OwnsOne(t => t.Description).Property(t => t.Value).HasColumnName("Description");
				vodetail.OwnsOne(t => t.DebtorPrice).Property(t => t.Value).HasColumnName("DebtorPrice");
				vodetail.OwnsOne(t => t.CreditorPrice).Property(t => t.Value).HasColumnName("CreditorPrice"); ;
				vodetail.OwnsOne(t => t.StreetPrice).Property(t => t.Value).HasColumnName("StreetPrice"); ;
			}

			);
		}
	}
}
