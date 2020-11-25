using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WebApplication.Application.Groups.Commands;
using WebApplication.Application.Groups.Models;
using WebApplication.Application.Invitations.Commands;
using WebApplication.Application.Invitations.Models;
using WebApplication.Application.Meals.Models;
using WebApplication.Application.Meetings.Commands;
using WebApplication.Application.Meetings.Models;
using WebApplication.Application.Movies.Models;
using WebApplication.Application.Users.Commands;
using WebApplication.Application.Users.Models;
using WebApplication.Mongo.Models;

namespace WebApplication.Application
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserDO, UserDto>();
            CreateMap<UserDO, UserNameDto>();
            CreateMap<UserDO, UserDO>();
            CreateMap<UpdateUserCommand, UserDO>();

            CreateMap<MovieDto, MovieDO>();
            CreateMap<MoviePreferenceDto, MoviePreferenceDO>();
            CreateMap<MoviePreferenceDO, MoviePreferenceDto>();

            CreateMap<MealPreferenceDto, MealPreferenceDO>();
            CreateMap<MealPreferenceDO, MealPreferenceDto>();
            CreateMap<MealDto, MealDO>();

            CreateMap<GroupDO, GroupDto>();
            CreateMap<GroupDO, GroupDO>();
            CreateMap<CreateGroupCommand, GroupDO>();
            CreateMap<UpdateGroupCommand, GroupDO>();

            CreateMap<MeetingDO, MeetingDto>();
            CreateMap<MeetingDO, MeetingDO>();
            CreateMap<CreateMeetingCommand, MeetingDO>();
            CreateMap<UpdateMeetingCommand, MeetingDO>();

            CreateMap<InvitationDO, InvitationDto>();
            CreateMap<InviteCommand, InvitationDO>();
        }
    }
}
