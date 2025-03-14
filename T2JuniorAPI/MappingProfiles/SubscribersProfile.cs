﻿using AutoMapper;
using T2JuniorAPI.DTOs.Users;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class SubscribersProfile : Profile
    {
        public SubscribersProfile()
        {
            CreateMap<UserSubscribers, SubscriberProfileDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdSubscriber))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Subscriber.FirstName} {src.Subscriber.LastName}"));
        }
    }
}
