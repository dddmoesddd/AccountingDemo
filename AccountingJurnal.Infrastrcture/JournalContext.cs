using AccountingJournal.Domain.Entities;
using AccountingJournal.Infrastrcture.JournalConfiguration;
using Microsoft.EntityFrameworkCore;

namespace AccountingJournal.Infrastrcture
{
	public class JournalContext : DbContext
	{

		public JournalContext()
		{

		}
		public JournalContext(DbContextOptions<JournalContext> options) : base(options)
		{

		}

		public DbSet<Journal> Journals { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new journalConfiguration());


		}
	}
}
