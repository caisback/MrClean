namespace MrClean.Presentation.Contracts.Feedback
{
    public record FeebackRequest(
        string Name,
        string Company,
        string Email,
        string Comments,
        int FixtureId,
        DateTime CreatedDate);
}
