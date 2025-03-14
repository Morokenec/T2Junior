﻿using AutoMapper;
using T2JuniorAPI.DTOs.Users;

namespace T2JuniorAPI.MappingProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<ApplicationUser, UserProfileDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.SubscibersCount, opt => opt.MapFrom(src => src.SubscribersAsUser.Count(s => s.IdUser == src.Id)))
                .ForMember(dest => dest.SubscriptionsCount, opt => opt.MapFrom(src => src.SubscribersAsSubscriber.Count(s => s.IdSubscriber == src.Id)))
                .ForMember(dest => dest.ClubsCount, opt => opt.MapFrom(src => src.ClubUsers.Count))
                .ForMember(dest => dest.PostAndOrganization, opt => opt.MapFrom(src => $"{src.Post} {src.Organization.Name}"))
                .ForMember(dest => dest.AvatarPath, opt => opt.MapFrom(src => src.UserAvatars
                .Where(ua => !ua.IsDelete)
                .OrderByDescending(ua => ua.CreationDate)
                .FirstOrDefault().Media.Path));

            CreateMap<RegisterUserDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<UpdateUserDto, ApplicationUser>();
        }
    }
}
