using AccountingVoucher.Domain.DomianService;
using FrameWork.Application.Behaviours;
using FrameWork.Infrastruture.MessageBrokerConfigorations.Rabbitmq.Voucher;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AccountingVoucher.Application
{
	public static class ApplicationServiceRegistration
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddMediatR(Assembly.GetExecutingAssembly());
	
			services.AddMassTransit(x =>
			{
				
				x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
				{
					config.Host(new Uri(AddVoucherRabbitmqConfig.RabbitMqRootUriWithOutContainerRoot), h =>
					{
						h.Username(AddVoucherRabbitmqConfig.UserName);
						h.Password(AddVoucherRabbitmqConfig.Password);
					});
				}));
			});
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionalCommandHandler<,>));
		    services.AddScoped<IVoucherBalancDomainService,VoucherBalanceCheck>();
			return services;
		}
	}
}

//public const string ProjectaQues = "pa-queue";
//public const string RabbitMqRootUri = "amqp://guest:guest@rabbitmq:5672";
//public const string RabbitMqUri = "rabbitmq://localhost/pa-queue";
//public const string UserName = "guest";
//public const string Password = "guest";
//public const string NotificationServiceQueue = "notification.service";