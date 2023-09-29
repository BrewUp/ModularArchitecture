﻿using BrewUp.Infrastructure.RabbitMq.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Infrastructure.RabbitMq;

public static class RabbitMqHelper
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, RabbitMqSettings rabbitMqSettings)
    {
        var serviceProvider = services.BuildServiceProvider();
        var repository = serviceProvider.GetRequiredService<IRepository>();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        var configuration = new RabbitMQConfiguration(rabbitMqSettings.Host, rabbitMqSettings.Username,
            rabbitMqSettings.Password, rabbitMqSettings.ExchangeCommandName, rabbitMqSettings.ExchangeEventName);
        var connectionFactory = new MufloneConnectionFactory(configuration, loggerFactory);

        services.AddMufloneTransportRabbitMQ(loggerFactory, configuration);
        
        //It's important to build the previous services registrations or IEventBus and IPurchaseOrderService will be null 
        //We need to improve this part. It's awful like it is right now
        serviceProvider = services.BuildServiceProvider();
        services.AddMufloneRabbitMQConsumers(new List<IConsumer>
        {
            new CreatePurchaseOrderConsumer(repository, connectionFactory, loggerFactory, serviceProvider.GetRequiredService<IEventBus>())
        });

        return services;
    }
}