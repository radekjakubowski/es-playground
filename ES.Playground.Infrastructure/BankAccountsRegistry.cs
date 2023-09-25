using ES.Playground.Domain;
using Microsoft.Extensions.Hosting;

namespace ES.Playground.Infrastructure
{
    public class BankAccountsRegistry
    {
        List<BankAccount> bankAccounts = new();

        public void AddBankAccount(BankAccount bankAccount)
        {
            bankAccounts.Add(bankAccount);
        }

        public BankAccount GetBankAccount(Guid bankAccountId) 
        {
            return bankAccounts.FirstOrDefault(b => b.Id == bankAccountId);
        }
    }
}
