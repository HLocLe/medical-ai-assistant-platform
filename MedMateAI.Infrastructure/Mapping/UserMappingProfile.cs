using AutoMapper;
using MedMateAI.Domain.Entities;
using MedMateAI.Infrastructure.Identity;

namespace MedMateAI.Infrastructure.Mapping;

public sealed class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<ApplicationUser, User>()
            .ForMember(d => d.IdentityId, m => m.MapFrom(s => s.Id))
            .ForMember(d => d.Email, m => m.MapFrom(s => s.Email ?? string.Empty))
            .ForMember(d => d.Status, m => m.MapFrom(s => s.Status))
            .ForMember(d => d.IsDeleted, m => m.MapFrom(s => s.IsDeleted));
    }
}
