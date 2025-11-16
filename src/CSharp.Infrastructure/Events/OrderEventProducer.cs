using Confluent.Kafka;
using CSharp.Core.Models;
using Newtonsoft.Json;

namespace CSharp.Infrastructure.Events;

public class OrderEventProducer
{
    private readonly IProducer<Null, string> _producer;

    public OrderEventProducer(IProducer<Null, string> producer)
    {
        _producer = producer;
    }

    public async Task PublishOrderValidatedEvent(Order order)
    {
        var json = JsonConvert.SerializeObject(order);
        await _producer.ProduceAsync("orders.validated", new Message<Null,string>{Value = json});
    }
}