using ErrorOr;
using MediatR;
using MrClean.Core.Application.Feedback.Common;
namespace MrClean.Core.Application.Feedback.Queries.GetFeedback
{
    public record GetFeedbackQuery() : IRequest<ErrorOr<List<FeedbackResult>>>;
}
