using MediatR;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Auth.Commands
{
    public record LoginUserCommand(string Email, string Lozinka) : IRequest<int?>;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, int?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<int?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var osoba = await _unitOfWork.Osobe.GetByEmailAndPasswordAsync(request.Email, request.Lozinka);

            return osoba?.idOsoba;
        }
    }
}
