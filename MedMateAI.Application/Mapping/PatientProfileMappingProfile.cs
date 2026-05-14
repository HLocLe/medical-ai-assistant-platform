using AutoMapper;
using MedMateAI.Application.DTOs.PatientProfiles.Responses;
using MedMateAI.Domain.Entities;

namespace MedMateAI.Application.Mapping;

public sealed class PatientProfileMappingProfile : Profile
{
    public PatientProfileMappingProfile()
    {
        CreateMap<PatientProfile, PatientProfileResponse>();
    }
}
