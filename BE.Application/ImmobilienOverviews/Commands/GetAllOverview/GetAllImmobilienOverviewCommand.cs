using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Application.ImmobilienOverviews.DTOs;
using MediatR;

namespace BE.Application.ImmobilienOverviews.Commands.GetAllOverview
{
    public class GetAllImmobilienOverviewCommand : IRequest<IEnumerable<ImmobilienOverviewDto>>
    {
    }
}
