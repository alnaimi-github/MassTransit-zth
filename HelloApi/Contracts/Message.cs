using MassTransit;

namespace HelloApi.Contracts;

//[EntityName("message-submitted")]
//[ExcludeFromTopology]
public class Message 
{
    public  string Text { get; set; } 
}