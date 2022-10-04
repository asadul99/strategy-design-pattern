namespace StrategyDesignPattern;

public class Message
{
    public string MessageBody { get; set; }
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public EnumNotificationMethod EnumNotificationMethod { get; set; }
}