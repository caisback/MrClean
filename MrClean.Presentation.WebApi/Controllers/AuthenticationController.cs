using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using MrClean.Core.Application.Services.Authentication;
using MrClean.Presentation.Contracts.Authentication;

namespace MrClean.Presentation.WebApi.Controllers
{
    //[ApiController]
    [Route("auth")]
    public class AuthenticationController : ApiController //Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService) => _authenticationService = authenticationService;

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            ErrorOr<AuthenticationResult> authRegisterResult = _authenticationService.Register(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email, registerRequest.Password);

            return authRegisterResult.Match(
                authResult => Ok(MapAuthResult(authResult)), 
                errors => Problem(errors));

            //if (authRegisterResult.IsT0)
            //{
            //    AuthenticationResponse registerResponse = MapAuthRegisterResult(authRegisterResult);
            //    return Ok(registerResponse);
            //}

            //return Problem(statusCode: StatusCodes.Status409Conflict, title: "Email already exists.");

        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult authRegisterResult)
        {
            return new AuthenticationResponse(
                authRegisterResult.User.Id,
                authRegisterResult.User.FirstName,
                authRegisterResult.User.LastName,
                authRegisterResult.User.Email,
                authRegisterResult.Token
                );
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            ErrorOr<AuthenticationResult> authLoginResult = _authenticationService.Login(loginRequest.Email, loginRequest.Password);

            // Default error from Application:
            return authLoginResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));
        }

    }
}
