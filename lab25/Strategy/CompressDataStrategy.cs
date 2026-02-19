namespace lab25.Strategy;

public class CompressDataStrategy : IDataProcessorStrategy
{
    public string Process(string data)
    {
        return $"Стиснені дані: {data.Length}_символів";
    }
}
