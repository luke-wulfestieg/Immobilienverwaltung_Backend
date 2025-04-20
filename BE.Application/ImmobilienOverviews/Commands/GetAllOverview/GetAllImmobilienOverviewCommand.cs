using BE.Application.ImmobilienOverviews.DTOs;
using MediatR;

namespace BE.Application.ImmobilienOverviews.Commands.GetAllOverview
{
    public class GetAllImmobilienOverviewCommand : IRequest<IEnumerable<ImmobilienOverviewDto>>
    {
    }
}
