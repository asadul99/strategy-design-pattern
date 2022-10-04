using Notify.Strategies;

namespace StrategyDesignPattern.ConcreteStrategies;

public class SMSNotificationStrategy : INotificationStrategy
{
    public EnumNotificationMethod NotificationMethod => EnumNotificationMethod.SMS;
    public Task<bool> SendNotificationAsync(Message message)
    {
        Console.WriteLine(message);
        //write sms sending logic here
        return Task.FromResult(true);
    }
}
