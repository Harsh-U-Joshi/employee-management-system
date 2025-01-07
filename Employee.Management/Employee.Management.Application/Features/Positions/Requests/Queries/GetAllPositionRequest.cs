using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Management.Application.Features.Positions.Requests.Queries
{
    public class GetAllPositionRequest : IRequest<BaseCommandResponse>
    {
    }
}
