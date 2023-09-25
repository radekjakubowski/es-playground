namespace ES.Playground.Blocks
{
    public static class ESBlocks
    {
        public abstract class AggregateRoot<TId>
        {
            private readonly List<DomainEvent> _events = new List<DomainEvent>();

            public TId Id { get; protected set; }

            public IEnumerable<DomainEvent> Events => _events;

            // Clear the list of events after they have been handled or persisted
            public void ClearEvents()
            {
                _events.Clear();
            }

            protected void AddEvent(DomainEvent domainEvent)
            {
                _events.Add(domainEvent);
            }

            public abstract void Apply(DomainEvent domainEvent);
        }

        // Define a base class for domain events
        public class DomainEvent
        {
            public DomainEvent(DateTime occurredAt, decimal? amount)
            {
                OccurredAt = occurredAt;
                Type = GetType().FullName;
                Amount = amount;
            }

            public DateTime OccurredAt { get; }
            public string Type { get; } 
            public decimal? Amount { get; }
        }
    }
}