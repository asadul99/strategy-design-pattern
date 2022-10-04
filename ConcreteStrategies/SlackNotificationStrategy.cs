using Notify.Strategies;

namespace StrategyDesignPattern.ConcreteStrategies;

public class SlackNotificationStrategy : INotificationStrategy
{
    public EnumNotificationMethod NotificationMethod => EnumNotificationMethod.Slack;
    public Task<bool> SendNotificationAsync(Message message)
    {
        Console.WriteLine(message);
        //write slack sending logic here
        return Task.FromResult(true);
    }
}
