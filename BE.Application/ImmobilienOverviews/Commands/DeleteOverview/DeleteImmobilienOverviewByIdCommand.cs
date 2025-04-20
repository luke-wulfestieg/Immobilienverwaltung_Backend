using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BE.Application.ImmobilienOverviews.Commands.DeleteOverview
{
    public class DeleteImmobilienOverviewByIdCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
