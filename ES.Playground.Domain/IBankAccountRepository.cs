namespace ES.Playground.Domain
{
    public interface IBankAccountRepository
    {
        void AddBankAccount(BankAccount bankAccount);
        BankAccount GetBankAccount(Guid bankAccountId);
    }
}
