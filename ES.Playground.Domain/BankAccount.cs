using static ES.Playground.Blocks.ESBlocks;
using static ES.Playground.Domain.Events;

namespace ES.Playground.Domain
{
    public class BankAccount : AggregateRoot<Guid>
    {
        private readonly IAggregateEventsManager<BankAccount, Guid> _aggregateEventsManager;

        public BankAccount(IAggregateEventsManager<BankAccount, Guid> aggregateEventsManager)
        {
            _aggregateEventsManager = aggregateEventsManager;
        }

        public decimal Amount { get; private set; }

        public override void Apply(DomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case AccountCreatedEvent e:
                    AddEvent(e);
                    InitiateBankAccount();
                    break;
                case AccountWithdrawalEvent e:
                    AddEvent(e);
                    WithdrawAmount((decimal)e.Amount!);
                    break;
                case AccountIncomeEvent e:
                    AddEvent(e);
                    IncomeAmount((decimal)e.Amount!);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"{domainEvent.GetType().Name} is not a valid event type for {nameof(BankAccount)}");
            }
        }

        public void InitiateBankAccount()
        {
            this.Amount = 0;
        }

        public static BankAccount CreateNewBankAccount(IAggregateEventsManager<BankAccount, Guid> aggregateEventsManager)
        {
            return new BankAccount(aggregateEventsManager) { Id = Guid.NewGuid() };
        }

        public void WithdrawAmount(decimal amount)
        {
            this.Amount -= amount;
        }

        public void IncomeAmount(decimal amount)
        {
            this.Amount += amount;
        }

        public void ApplyAllDomainEvents()
        {
            var events = _aggregateEventsManager.GetDomainEvents(this.Id);

            foreach (var @event in events)
            {
                Apply(@event);
            }
        }

        public void StoreDomainEvent(DomainEvent @event)
        {
            _aggregateEventsManager.StoreDomainEvent(this.Id, @event);
        }
    }
}
