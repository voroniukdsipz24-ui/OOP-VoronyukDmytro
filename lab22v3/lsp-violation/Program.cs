using System;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Account account = new Account(500);
        account.Deposit(200);
        account.Withdraw(100);  // Це працює коректно

        Console.WriteLine($"Баланс рахунку: {account.Balance}");

        SavingsAccount savingsAccount = new SavingsAccount(1500);
        savingsAccount.Deposit(200);

        // Порушення LSP: спроба зняти кошти, що призведе до помилки, якщо баланс менше мінімуму
        try
        {
            savingsAccount.Withdraw(750); // Це згенерує помилку, оскільки залишок буде менше мінімального
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine($"Баланс ощадного рахунку: {savingsAccount.Balance}");
    }
}
