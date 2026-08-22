using MediatR;

namespace MuzickiFestivali.API.Features.Users.Commands
{
    public record LogoutUserCommand() : IRequest;

    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
    {
        public Task Handle(LogoutUserCommand request, CancellationToken cancellationToken) =>
            Task.CompletedTask;
    }
}
