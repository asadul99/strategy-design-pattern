using Notify.Strategies;

namespace StrategyDesignPattern.ConcreteStrategies;

public class EmailNotificationStrategy : INotificationStrategy
{
    //initiate notification method
    public EnumNotificationMethod NotificationMethod => EnumNotificationMethod.Email;

    /// <summary>
    /// Send email notification
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task<bool> SendNotificationAsync(Message message)
    {
        Console.WriteLine(message);
        //write email sending logic here
        return Task.FromResult(true);
    }
}
