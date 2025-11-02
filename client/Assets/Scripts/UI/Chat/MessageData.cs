public class MessageData
{
    public string CharaterName { get; set; }
    public string Message { get; private set; }

    public MessageData(string CharaterName, string Message)
    {
        this.CharaterName = CharaterName;
        this.Message = Message;
    }
}
