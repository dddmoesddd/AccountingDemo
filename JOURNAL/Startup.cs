using AccountingJournal.Application.Features.EventsHandlers.Consumers;
using AccountingJurnal.Infrastrcture;
using FrameWork.Domain.Events.VoucherEvents;
using FrameWork.Infrastruture.MessageBrokerConfigorations.Rabbitmq.Voucher;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JOURNAL
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddInfrastructureServices(Configuration);
			services.AddScoped<AddVoucherConsumer>();
			services.AddEndpointsApiExplorer();
			try
			{
				var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
				{
					cfg.Host(new Uri(AddVoucherRabbitmqConfig.RabbitMqRootUriWithoutContainer), h =>
					{
						h.Username(AddVoucherRabbitmqConfig.UserName);
						h.Password(AddVoucherRabbitmqConfig.Password);
					});
					cfg.ReceiveEndpoint(AddVoucherRabbitmqConfig.AddVoucherQueue, ep =>
		{
			ep.PrefetchCount = 16;
			ep.UseMessageRetry(r => r.Interval(2, 100));
			ep.Consumer<AddVoucherConsumer>();
		});

				});

				bus.Start();
				//services.AddMassTransit(mt =>
				//{
				//	mt.AddConsumer<AddVoucherConsumer>();

				//	mt.UsingRabbitMq((context, cfg) =>
				//	{
				//		cfg.Host(new Uri(AddVoucherRabbitmqConfig.RabbitMqRootUriWithoutContainer), h =>
				//		{
				//			h.Username(AddVoucherRabbitmqConfig.UserName);
				//			h.Password(AddVoucherRabbitmqConfig.Password);
				//		});

				//		cfg.ReceiveEndpoint(AddVoucherRabbitmqConfig.AddVoucherQueue, ec =>
				//		{


				//			ec.UseMessageRetry(x => x.Interval(5, TimeSpan.FromSeconds(1)));
				//			ec.UseDelayedRedelivery(x => x.Incremental(5, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));

				//			ec.ConfigureConsumer<AddVoucherConsumer>(context);
				//		});
				//	});
				////	bus.Start(new TimeSpan(0,1,0));




				//});

	
			}
			catch (Exception ex)
			{

				var ss = ex;
			}

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "JOURNAL", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JOURNAL v1"));
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
