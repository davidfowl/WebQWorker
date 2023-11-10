namespace Worker;

public class MessageListener(ILogger<MessageListener> logger)
{
    private Func<string, Task>? _onMessage;

    public IDisposable Subscribe(Func<string, Task> onMessage)
    {
        logger.LogInformation("Subscribing to messages");

        _onMessage += onMessage;

        return new DisposableAction(() =>
        {
            logger.LogInformation("Unsubscribing from messages");

            _onMessage -= onMessage;
        });
    }

    public Task WriteMessageAsync(string message)
    {
        if (_onMessage is { } m)
        {
            return m(message);
        }

        return Task.CompletedTask;
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
