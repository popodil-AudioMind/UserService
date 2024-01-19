using AuthService.Models.RabbitMQ;
using MassTransit;
using UserService.Data;
using UserService.Models;

namespace UserService.Services
{
    public class CreateConsumer : IConsumer<CreateMessage>
    {
        private ISqlUser _sqlUser;
        public CreateConsumer(ISqlUser sqlUser) 
        { 
            _sqlUser = sqlUser;
        }
        public Task Consume(ConsumeContext<CreateMessage> context)
        {
            _sqlUser.AddUser(new User()
            {
                id = Guid.Parse(context.Message.id),
                displayname = context.Message.displayName
            });
            return Task.CompletedTask;
        }
    }
}
