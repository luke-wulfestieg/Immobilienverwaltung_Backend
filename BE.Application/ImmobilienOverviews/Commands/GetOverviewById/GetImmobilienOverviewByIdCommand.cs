using BE.Application.ImmobilienOverviews.DTOs;
using MediatR;

namespace BE.Application.ImmobilienOverviews.Commands.GetOverviewById
{
    public class GetImmobilienOverviewByIdCommand : IRequest<ImmobilienOverviewDto>
    {
        public int Id { get; }

        public GetImmobilienOverviewByIdCommand(int id)
        {
            Id = id;
        }
    }
}
