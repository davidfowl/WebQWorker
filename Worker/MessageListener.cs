using System.Threading.Channels;

namespace Worker;

public class MessageListener(ILogger<MessageListener> logger)
{
    private readonly List<ChannelWriter<string>> _subscribers = [];
    private readonly SemaphoreSlim _lock = new(1);

    public async Task SubscribeAsync(Channel<string> channel)
    {
        logger.LogInformation("Subscribing to messages");

        await _lock.WaitAsync();
        try
        {
            _subscribers.Add(channel.Writer);
        }
        finally
        {
            _lock.Release();
        }

        async Task WaitForCompletion()
        {
            await channel.Reader.Completion;

            logger.LogInformation("Unsubscribing from messages");

            await _lock.WaitAsync();
            try
            {
                _subscribers.Remove(channel.Writer);
            }
            finally
            {
                _lock.Release();
            }
        }

        // When the channel closes, remove it from the list of subscriptions
        _ = WaitForCompletion();
    }

    public async Task WriteMessageAsync(string message)
    {
        await _lock.WaitAsync();
        try
        {
            foreach (var w in _subscribers)
            {
                await w.WriteAsync(message);
            }
        }
        finally
        {
            _lock.Release();
        }
    }

    private class DisposableAction(Action a) : IDisposable
    {
        private Action? _action = a;

        public void Dispose()
        {
            Interlocked.Exchange(ref _action, null)?.Invoke();
        }
    }
}
