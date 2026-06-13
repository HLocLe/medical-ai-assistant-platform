using AutoMapper;
using MedMateAI.Application.DTOs.IcdChapters.Responses;
using MedMateAI.Domain.Entities;

namespace MedMateAI.Application.Mapping;

public sealed class IcdChapterMappingProfile : Profile
{
    public IcdChapterMappingProfile()
    {
        CreateMap<IcdChapter, IcdChapterResponse>();
    }
}
