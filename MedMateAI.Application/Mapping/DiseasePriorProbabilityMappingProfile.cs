using AutoMapper;
using MedMateAI.Application.DTOs.DiseasePriorProbabilities.Responses;
using MedMateAI.Domain.Entities;

namespace MedMateAI.Application.Mapping;

public sealed class DiseasePriorProbabilityMappingProfile : Profile
{
    public DiseasePriorProbabilityMappingProfile()
    {
        CreateMap<DiseasePriorProbability, DiseasePriorProbabilityResponse>();
    }
}
