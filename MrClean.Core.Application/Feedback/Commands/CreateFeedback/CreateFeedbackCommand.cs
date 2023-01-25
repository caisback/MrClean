using MediatR;

namespace MrClean.Core.Application.Feedback.Commands.CreateFeedback
{
    public record CreateFeedbackCommand(
        string Name,
        string Company,
        string Email,
        string Comments,
        int FixtureId,
        DateTime CreatedDate) : IRequest;
}
