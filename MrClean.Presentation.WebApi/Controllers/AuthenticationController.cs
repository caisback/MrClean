using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MrClean.Core.Application.Authentication.Commands.Register;
using MrClean.Core.Application.Authentication.Common;
using MrClean.Core.Application.Authentication.Queries.Login;
//using MrClean.Core.Application.Services.Authentication;
using MrClean.Presentation.Contracts.Authentication;

namespace MrClean.Presentation.WebApi.Controllers
{
    //[ApiController]
    [Route("auth")]
    public class AuthenticationController : ApiController //Controller
    {
        //private readonly IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;


        //public AuthenticationController(IAuthenticationService authenticationService) => _authenticationService = authenticationService;
        public AuthenticationController(IMediator mediator) => _mediator = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            //ErrorOr<AuthenticationResult> authRegisterResult = _authenticationService.Register(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email, registerRequest.Password);

            var command = new RegisterCommand(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email, registerRequest.Password);
            ErrorOr<AuthenticationResult> authRegisterResult = await _mediator.Send(command);

            return authRegisterResult.Match(
                authResult => Ok(MapAuthResult(authResult)), 
                errors => Problem(errors));
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
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            //ErrorOr<AuthenticationResult> authLoginResult = _authenticationService.Login(loginRequest.Email, loginRequest.Password);

            var query = new LoginQuery(loginRequest.Email, loginRequest.Password);
            ErrorOr<AuthenticationResult> authLoginResult = await _mediator.Send(query);

            return authLoginResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));
        }

    }
}
