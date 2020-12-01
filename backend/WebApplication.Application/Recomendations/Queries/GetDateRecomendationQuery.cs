using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.Recomendations.Queries
{
    public class GetDateRecomendationQuery : IRequest<IEnumerable<DateTime>>
    {
        public string MeetingID;
    }
}
