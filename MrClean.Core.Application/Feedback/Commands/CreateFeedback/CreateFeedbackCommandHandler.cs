using MediatR;
using MrClean.Core.Application.Common.Interfaces.Persistence;

namespace MrClean.Core.Application.Feedback.Commands.CreateFeedback
{
    public class CreateFeedbackCommandHandler : AsyncRequestHandler<CreateFeedbackCommand>
    {
        private readonly ICosmosClientService _cosmosClientService;
        public CreateFeedbackCommandHandler(ICosmosClientService cismosClientService) => _cosmosClientService = cismosClientService;

        protected async override Task Handle(CreateFeedbackCommand command, CancellationToken cancellationToken)
        {
            var feedback = new Domain.Entities.Feedback
            {
                Name = command.Name,
                Email = command.Email,
                Company = command.Company,
                Comments = command.Comments,
                FixtureId = command.FixtureId,
                CreatedDate = command.CreatedDate
            };
            await _cosmosClientService.CreateItemAsync("feedbacks", feedback,"/id", feedback.Id);
        }
    }
}
