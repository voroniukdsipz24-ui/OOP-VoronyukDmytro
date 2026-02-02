public class SavingsAccount : Account
{
    private const decimal MinimumBalance = 1000m;

    public SavingsAccount(decimal initialBalance) : base(initialBalance)
    {
    }

    public override void Withdraw(decimal amount)
    {
        if (Balance - amount < MinimumBalance)
        {
            throw new InvalidOperationException("Не можна зняти кошти, потрібно зберегти мінімальний баланс.");
        }

        base.Withdraw(amount);
    }
}
