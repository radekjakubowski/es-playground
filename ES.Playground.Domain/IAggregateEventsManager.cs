using static ES.Playground.Blocks.ESBlocks;

namespace ES.Playground.Domain
{
    public interface IAggregateEventsManager<TAgrregateType, TAggregateId> where TAgrregateType : AggregateRoot<TAggregateId>
    {
        List<DomainEvent> GetDomainEvents(TAggregateId aggregateId);
        void StoreDomainEvent(TAggregateId aggregateId, DomainEvent @event);
    }
}
