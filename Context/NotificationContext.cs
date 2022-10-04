using Notify.Strategies;

namespace StrategyDesignPattern.Context;

public class NotificationContext : INotificationContext
{
    private readonly IEnumerable<INotificationStrategy> _notificationStrategies;
    public NotificationContext(IEnumerable<INotificationStrategy> notificationStrategies)
    {
        _notificationStrategies = notificationStrategies;
    }

    public INotificationStrategy GetNotificationStrategy(EnumNotificationMethod notificationMethod)
    {
        var notificationStrategy = _notificationStrategies.FirstOrDefault(t => t.NotificationMethod == notificationMethod);
        return notificationStrategy;
    }
}