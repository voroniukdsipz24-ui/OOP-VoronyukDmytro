public class SavingsAccount
{
    private const decimal MinimumBalance = 1000m;
    private readonly Account _account;

    public SavingsAccount(decimal initialBalance)
    {
        _account = new Account(initialBalance);
    }

    public decimal Balance => _account.Balance;

    public void Withdraw(decimal amount)
    {
        if (_account.Balance - amount < MinimumBalance)
        {
            throw new InvalidOperationException("Не можна зняти кошти, потрібно зберегти мінімальний баланс.");
        }

        _account.Withdraw(amount);
    }

    public void Deposit(decimal amount)
    {
        _account.Deposit(amount);
    }
}
