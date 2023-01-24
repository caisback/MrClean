using ErrorOr;
using MrClean.Core.Application.Authentication.Common;
using MrClean.Core.Application.Common.Interfaces.Authentication;
using MrClean.Core.Application.Common.Interfaces.Persistence;
using MrClean.Core.Domain.Common.Errors;
using MrClean.Core.Domain.Entities;


namespace MrClean.Core.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;   
        }
        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            // Validate, should be existing
            if(_userRepository.GetByEmail(email) is not User user)
            {
                //throw new Exception("User with given email does not exist.");
                return Errors.AuthenticationErrors.InvalidCredentials;
            }

            // Validate, password should be correct
            if(user.Password != password) 
            {
                //throw new Exception("Invalid password.");
                return Errors.AuthenticationErrors.InvalidCredentials;

            }

            // Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            // Check if user already exists
            if(_userRepository.GetByEmail(email) is not null)
            {
                //throw new Exception("User with given email already exists.");
                return Errors.UserErrors.DuplicateEmail;
            }

            // Create user (generate unique ID) & Persist to DB
            var user = new User { FirstName = firstName, LastName = lastName, Email = email, Password = password };

            _userRepository.Add(user);

            // Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
