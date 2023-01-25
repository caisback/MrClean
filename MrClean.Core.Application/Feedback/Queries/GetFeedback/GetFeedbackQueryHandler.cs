using ErrorOr;
using MediatR;
using MrClean.Core.Application.Common.Interfaces.Persistence;
using MrClean.Core.Application.Feedback.Common;

namespace MrClean.Core.Application.Feedback.Queries.GetFeedback
{
    public class GetFeedbackQueryHandler : IRequestHandler<GetFeedbackQuery, ErrorOr<List<FeedbackResult>>>
    {
        private readonly ICosmosClientService _cosmosClientService;

        public GetFeedbackQueryHandler(ICosmosClientService cosmosClientService) => _cosmosClientService = cosmosClientService;

        public async Task<ErrorOr<List<FeedbackResult>>> Handle(GetFeedbackQuery request, CancellationToken cancellationToken)
        {
            var feedbacks = await _cosmosClientService.GetItemsAsync<FeedbackResult>("feedbacks", sqlQueryText: "select * from c where c.IsDeleted = false");
            return feedbacks;
        }
    }
}
