using ErrorOr;
using MediatR;
using MrClean.Core.Application.Authentication.Common;
using MrClean.Core.Application.Common.Interfaces.Authentication;
using MrClean.Core.Application.Common.Interfaces.Persistence;
using MrClean.Core.Domain.Entities;
using MrClean.Core.Domain.Common.Errors;

namespace MrClean.Core.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // Validate, should be existing
            if (_userRepository.GetByEmail(query.Email) is not User user)
            {
                //throw new Exception("User with given email does not exist.");
                return Errors.AuthenticationErrors.InvalidCredentials;
            }

            // Validate, password should be correct
            if (user.Password != query.Password)
            {
                //throw new Exception("Invalid password.");
                return Errors.AuthenticationErrors.InvalidCredentials;

            }

            // Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
