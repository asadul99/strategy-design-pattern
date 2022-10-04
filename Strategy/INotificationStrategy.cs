using StrategyDesignPattern;

namespace Notify.Strategies;

public interface INotificationStrategy
{
    Task<bool> SendNotificationAsync(Message message);
    EnumNotificationMethod NotificationMethod { get; }
}