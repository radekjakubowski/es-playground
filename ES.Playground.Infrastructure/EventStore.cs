using Microsoft.Extensions.Hosting;
using static ES.Playground.Blocks.ESBlocks;

namespace ES.Playground.Infrastructure
{
    public class EventStore
    {
        Dictionary<Guid, List<DomainEvent>> domainEvents = new();

        public void StoreEvent(Guid aggregateId, DomainEvent domainEvent)
        {
            domainEvents.TryGetValue(aggregateId, out List<DomainEvent> eventsList);

            if (eventsList == null)
            {
                domainEvents[aggregateId] = new List<DomainEvent>() { domainEvent };
                return;
            }

            domainEvents[aggregateId].Add(domainEvent);
        }

        public List<DomainEvent> GetDomainEvents(Guid aggregateId)
        {
            return domainEvents[aggregateId];
        }
    }
}
