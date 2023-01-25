using MediatR;
using Microsoft.AspNetCore.Mvc;
using MrClean.Core.Application.Feedback.Commands.CreateFeedback;
using MrClean.Core.Application.Feedback.Queries.GetFeedback;
using MrClean.Presentation.Contracts.Feedback;

namespace MrClean.Presentation.WebApi.Controllers
{
    [Route("feedback")]
    public class FeedbackController : ApiController
    {
        private readonly IMediator _mediator;

        public FeedbackController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateFeedback(FeebackRequest feebackRequest)
        {
            var command = new CreateFeedbackCommand(
                feebackRequest.Name,
                feebackRequest.Company,
                feebackRequest.Email,
                feebackRequest.Comments,
                feebackRequest.FixtureId,
                feebackRequest.CreatedDate);
            await _mediator.Send(command);

            return Ok(command);
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedbacks()
        {
            var feedbacks = await _mediator.Send(new GetFeedbackQuery());
            return Ok(feedbacks);
        }
    }
}
