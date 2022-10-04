using Microsoft.AspNetCore.Mvc;

using Notify.Strategies;

using StrategyDesignPattern.Context;

namespace StrategyDesignPattern.Controllers;

[ApiController]
[Route("[controller]")]
public class NodificationController : ControllerBase
{
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
}