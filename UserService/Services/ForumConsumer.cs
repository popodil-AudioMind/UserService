using Audiomind.RabbitMQ.Moddels;
using MassTransit;
using UserService.Data;

namespace Audiomind.RabbitMQ
{
    public class DeleteConsumer : IConsumer<DeleteMessage>
    {
        private ISqlUser _sqlUser;
        public DeleteConsumer(ISqlUser sqlUser) 
        { 
            _sqlUser = sqlUser;
        }
        public Task Consume(ConsumeContext<DeleteMessage> context)
        {
            _sqlUser.DeleteUserById(context.Message.id);
            return Task.CompletedTask;
        }
    }
}
