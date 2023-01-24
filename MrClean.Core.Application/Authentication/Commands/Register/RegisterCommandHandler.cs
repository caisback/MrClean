using ErrorOr;
using MediatR;
using MrClean.Core.Application.Authentication.Common;
using MrClean.Core.Application.Common.Interfaces.Authentication;
using MrClean.Core.Application.Common.Interfaces.Persistence;
using MrClean.Core.Domain.Entities;

namespace MrClean.Core.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            // Check if user already exists
            if (_userRepository.GetByEmail(command.Email) is not null)
            {
                //throw new Exception("User with given email already exists.");
                return Domain.Common.Errors.Errors.UserErrors.DuplicateEmail;
            }

            // Create user (generate unique ID) & Persist to DB
            var user = new User { FirstName = command.FirstName, LastName = command.LastName, Email = command.Email, Password = command.Password };

            _userRepository.Add(user);

            // Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
