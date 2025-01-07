using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Application.Features.Positions.Requests.Queries;
using MediatR;

namespace Employee.Management.Application.Features.Positions.Handlers.Queries
{
    public class GetAllPositionRequestHandler : IRequestHandler<GetAllPositionRequest, BaseCommandResponse>
    {
        private readonly IPositionRepository _positionRepository;

        public GetAllPositionRequestHandler(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<BaseCommandResponse> Handle(GetAllPositionRequest request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            response.Data = await _positionRepository.GetPositionDropDown();

            return response;
        }

    }
}
