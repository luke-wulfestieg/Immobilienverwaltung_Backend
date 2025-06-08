using MediatR;

namespace BE.Application.ImmobilienOverviews.Commands.DeleteOverview
{
    public class DeleteImmobilienOverviewByIdCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
