using Notify.Strategies;
namespace StrategyDesignPattern.Context;
public interface INotificationContext
{
    INotificationStrategy GetNotificationStrategy(EnumNotificationMethod notificationMethod);
}