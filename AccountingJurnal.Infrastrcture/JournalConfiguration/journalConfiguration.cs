using AccountingJournal.Domain.Entities;
using FrameWork.Domain.Events.VoucherEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingJournal.Infrastrcture.JournalConfiguration
{
	public class journalConfiguration : IEntityTypeConfiguration<Journal>
	{
		public void Configure(EntityTypeBuilder<Journal> builder)
		{

			builder.ToTable("Journal");
			builder.Property(t => t.Id).ValueGeneratedOnAdd().HasAnnotation("SqlServer:ValueGenerationStrategy", Microsoft.EntityFrameworkCore.Metadata.SqlServerValueGenerationStrategy.IdentityColumn); ;
			builder.HasKey(t => t.Id);
			builder.Property(x => x.VoucherNumber);
			builder.Property(x => x.CreditorPrice);
			builder.Property(x => x.DebtorPrice);
			builder.Property(x => x.Description);



		}
	}
}
