using AccountingJournal.Application.Contracts;
using AccountingJournal.Infrastrcture.Repositories;
using FrameWork.Infrastruture.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingJournal.Infrastrcture
{
	public static class InfrastructureServiceRegistration
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<JournalContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("JournalConnectionString")));
			services.AddDbContext<DbContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("JournalConnectionString")));
			services.AddScoped<IJournalRepository, JournalRepository>();
			services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
			//		services.AddDbContext<DbContext>(options =>
			//options.UseSqlServer(configuration.GetConnectionString("VoucherConnectionString")));


			//		services.AddScoped<IVoucherRepository, VoucherRepository>();
			//		services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
			//		services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}
}
