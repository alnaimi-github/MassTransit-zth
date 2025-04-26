namespace HelloApi.Contracts;

//[EntityName("message-submitted")]
//[ExcludeFromTopology]
// [ConfigureConsumeTopology(false)]
public class Message
{
    public string Text { get; set; } = string.Empty;
}