using ES.Playground.Domain;

namespace ES.Playground.Infrastructure
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly BankAccountsRegistry _bankAccountsRegistry;

        public BankAccountRepository(BankAccountsRegistry bankAccountsRegistry)
        {
            _bankAccountsRegistry = bankAccountsRegistry;
        }

        public void AddBankAccount(BankAccount bankAccount)
        {
            _bankAccountsRegistry.AddBankAccount(bankAccount);
        }

        public BankAccount GetBankAccount(Guid bankAccountId)
        {
            return _bankAccountsRegistry.GetBankAccount(bankAccountId);
        }
    }
}
