# Strategy Design Pattern, no set strategy and no if/else no switch/case

This pattern falls under the category of behavioral pattern and as the name suggests, it allows clients to choose an algorithm from a set of algorithms at run time.

Most of the people uses a set strategy method like this
![Set strategy method](https://github.com/asadul99/strategy-design-pattern/blob/master/sc/set-strategy-method.PNG?raw=true)
But the strategy should resolve run time depending the strategy method

And some one uses if/else or switch/case for deciding the strategy that's violate `Open Close` principle.
![Set strategy method](https://github.com/asadul99/strategy-design-pattern/blob/master/sc/switch-case.PNG?raw=true)

Let's see the implementation of strategy pattern without set method and switch/case

Here, I have implemented to send notification like SMS,Email. Here is the message class
```c#
public class Message
{
    public string MessageBody { get; set; }
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public EnumNotificationMethod EnumNotificationMethod { get; set; }
}
```
and here is the strategy method
```c#
public enum EnumNotificationMethod
{
    None = 0,
    SMS = 1,
    Email = 2
}
```
So when the message says that the `EnumNotificationMethod` is `SMS` then the strategy pattern should take decision and send it vaia `SMS` and vice versa
Here is the strategy
```c#
public interface INotificationStrategy
{
    Task<bool> SendNotificationAsync(Message message);
    EnumNotificationMethod NotificationMethod { get; }
}
```
And here is the concrete strategy (Email)
```c#
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
```
and SMS 
```c#
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
```
Here is the notification context which will decide the notification method at run time
```c#
public interface INotificationContext
{
    INotificationStrategy GetNotificationStrategy(EnumNotificationMethod notificationMethod);
}
```
And here is the context implementation
```c#
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
```
As both Email and SMS notification implemented `INotificationStrategy`, so we need to decide the correct method by `NotificationMethod` passed in message body.
Register the dependencies

```c#
//register dependencies
builder.Services.AddTransient<INotificationStrategy, EmailNotificationStrategy>();
builder.Services.AddTransient<INotificationStrategy, SMSNotificationStrategy>();
builder.Services.AddTransient<INotificationContext, NotificationContext>();
```


When we call the api with this message payload
```json
{
  "messageBody": "message text here",
  "sender": "sender here",
  "receiver": "receiver here",
  "enumNotificationMethod": 1 //SMS
}
```
`SendNotificationAsync` method will decide the notification method and will call corresponding method
```c#
    private readonly INotificationContext _notificationContext;
    public NodificationController(INotificationContext notificationContext)
    {
        this._notificationContext = notificationContext;
    }

    [HttpPost(Name = "SendNotification")]
    public async Task<IActionResult> SendNotificationAsync([FromBody] Message message)
    {
        INotificationStrategy notificationStrategy = _notificationContext.GetNotificationStrategy(message.EnumNotificationMethod);
        return Ok(await notificationStrategy.SendNotificationAsync(message));
    }
```
So there is no set notification or switch/case.
If we want to implement new noticication method like `Slack` then 
1. Add a new Notification method in enum 
2. Create new file for handling slack implementaion.
3. Register the new implementation

Let's do
```c#
public enum EnumNotificationMethod
{
    None = 0,
    SMS = 1,
    Email = 2,
    Slack= 3
}
```
and 
```c#
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
```
```c#
builder.Services.AddTransient<INotificationStrategy, SlackNotificationStrategy>();
```
That's it.
