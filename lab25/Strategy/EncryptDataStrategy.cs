using System.Text;

namespace lab25.Strategy;

public class EncryptDataStrategy : IDataProcessorStrategy
{
    public string Process(string data)
    {
        var sb = new StringBuilder();
        foreach (var c in data)
            sb.Append((char)(c + 1));

        return $"Зашифровані дані: {sb}";
    }
}
