using AutoMapper;
using MedMateAI.Application.DTOs.ClinicalQuestions.Responses;
using MedMateAI.Domain.Entities;

namespace MedMateAI.Application.Mapping;

public sealed class ClinicalQuestionMappingProfile : Profile
{
    public ClinicalQuestionMappingProfile()
    {
        CreateMap<ClinicalQuestion, ClinicalQuestionResponse>();
    }
}
