using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ES.Playground.Blocks.ESBlocks;

namespace ES.Playground.Domain
{
    public static class Events
    {
        public class AccountCreatedEvent : DomainEvent
        {
            public AccountCreatedEvent() : base(DateTime.UtcNow, null)
            {
            }
        }

        public class AccountWithdrawalEvent : DomainEvent
        {
            public AccountWithdrawalEvent(decimal amount) : base(DateTime.UtcNow, amount)
            {
            }
        }

        public class AccountIncomeEvent : DomainEvent
        {
            public AccountIncomeEvent(decimal amount) : base(DateTime.UtcNow, amount)
            {
            }
        }
    }
}
