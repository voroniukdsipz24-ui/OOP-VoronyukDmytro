using System;

namespace lab25.Observer;

public class DataPublisher
{
    public event Action<string>? DataProcessed;

    public void PublishDataProcessed(string data)
    {
        DataProcessed?.Invoke(data);
    }
}
