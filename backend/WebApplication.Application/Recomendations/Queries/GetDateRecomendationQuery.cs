using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.Recomendations.Queries
{
    public class GetDateRecomendationQuery : IRequest<DateTime>
    {
        public string MeetingID;
    }
}
