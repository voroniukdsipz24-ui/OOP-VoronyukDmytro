namespace Domain
{
    public enum TransactionType
    {
        Deposit,       // Вхід
        Withdrawal,    // Вихід (готівка)
        TransferIn,    // Вхід (переказ)
        TransferOut    // Вихід (переказ)
    }
}
