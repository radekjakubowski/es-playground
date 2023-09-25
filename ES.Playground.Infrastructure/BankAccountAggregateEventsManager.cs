using ES.Playground.Blocks;
using ES.Playground.Domain;
using static ES.Playground.Blocks.ESBlocks;

namespace ES.Playground.Infrastructure;

public class BankAccountAggregateEventsManager : IAggregateEventsManager<BankAccount, Guid>
{
    private readonly EventStore _eventStore;

    public BankAccountAggregateEventsManager(EventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public List<DomainEvent> GetDomainEvents(Guid aggregateId)
    {
        return _eventStore.GetDomainEvents(aggregateId);
    }

    public void StoreDomainEvent(Guid aggregateId, DomainEvent @event)
    {
        _eventStore.StoreEvent(aggregateId, @event);
    }
}
