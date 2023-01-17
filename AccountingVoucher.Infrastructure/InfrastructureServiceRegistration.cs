using AccountingVoucher.Application.Contracts.Persistance;
using AccountingVoucher.Infrastructure.Repositories;
using FrameWork.Infrastruture.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountingVoucher.Infrastructure
{
	public static class InfrastructureServiceRegistration
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<VoucherContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("VoucherConnectionString")));

			services.AddDbContext<DbContext>(options =>
	             options.UseSqlServer(configuration.GetConnectionString("VoucherConnectionString")));

			services.AddScoped<IVoucherRepository, VoucherRepository>();

			services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}

}
