namespace MrClean.Core.Application.Feedback.Common
{
    public record FeedbackResult(
        string id,
        string Name,
        string Company,
        string Email,
        string Comments,
        int FixtureId,
        DateTime CreatedDate,
        bool IsDeleted);
}
