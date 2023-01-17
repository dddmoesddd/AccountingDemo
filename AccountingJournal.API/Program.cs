using AccountingJournal.Application.Features.EventsHandlers.Consumers;
using AccountingJournal.Infrastrcture;
using FrameWork.Domain.Events.VoucherEvents;
using FrameWork.Infrastruture.MessageBrokerConfigorations.Rabbitmq.Voucher;
using MassTransit;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddScoped<AddVoucherConsumer>();
builder.Services.AddScoped<DeleteVoucherConsumerHandler>();
builder.Services.AddScoped<UpdateVocherConsumerHandler>();



builder.Services.AddMassTransit(
   config =>
   {
	   config.AddConsumer<AddVoucherConsumer>();
	   config.AddConsumer<DeleteVoucherConsumerHandler>();
	   config.AddConsumer<UpdateVocherConsumerHandler>();
	   config.UsingRabbitMq((ctx, cfg) =>
	   {
		   cfg.Host(new Uri(AddVoucherRabbitmqConfig.RabbitMqRootUriWithOutContainerRoot), h =>
		   {
			   h.Username(AddVoucherRabbitmqConfig.UserName);
			   h.Password(AddVoucherRabbitmqConfig.Password);
		   });
		   cfg.ReceiveEndpoint(AddVoucherRabbitmqConfig.AddVoucherQueue, ep =>
		   {
			   ep.PrefetchCount = 16;
			   ep.UseMessageRetry(r => r.Interval(2, 100));
			   ep.ConfigureConsumer<AddVoucherConsumer>(ctx);
		   });
		   cfg.ReceiveEndpoint(DeleteVoucherRabbitmqConfig.DeleteVoucherQueue, ep =>
		   {
			   ep.PrefetchCount = 16;
			   ep.UseMessageRetry(r => r.Interval(2, 100));
			   ep.ConfigureConsumer<DeleteVoucherConsumerHandler>(ctx);
		   });
		   cfg.ReceiveEndpoint(UpdateVoucherRabbitmqConfig.UpdateVoucherQueue, ep =>
		   {
			   ep.PrefetchCount = 16;
			   ep.UseMessageRetry(r => r.Interval(2, 100));
			   ep.ConfigureConsumer<UpdateVocherConsumerHandler>(ctx);
		   });


	   }
	   );
   });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
