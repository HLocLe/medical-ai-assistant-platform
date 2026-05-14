using AutoMapper;
using MedMateAI.Application.DTOs.Users.Responses;
using MedMateAI.Infrastructure.Identity;

namespace MedMateAI.Infrastructure.Mapping;

public sealed class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<ApplicationUser, ApplicationUserResponse>()
            .ForMember(d => d.Roles, o => o.Ignore());
    }
}
