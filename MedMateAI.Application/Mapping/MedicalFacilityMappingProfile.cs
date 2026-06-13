using AutoMapper;
using MedMateAI.Application.DTOs.MedicalFacilities.Responses;
using MedMateAI.Domain.Entities;

namespace MedMateAI.Application.Mapping;

public sealed class MedicalFacilityMappingProfile : Profile
{
    public MedicalFacilityMappingProfile()
    {
        CreateMap<MedicalFacility, MedicalFacilityResponse>()
            .ForMember(
                dest => dest.Departments,
                opt => opt.MapFrom(src => src.FacilityDepartments
                    .Where(facilityDepartment =>
                        !facilityDepartment.IsDeleted
                        && facilityDepartment.Department != null
                        && !facilityDepartment.Department.IsDeleted)
                    .OrderBy(facilityDepartment =>
                        (facilityDepartment.Department!.DepartmentName ?? string.Empty).ToLowerInvariant())
                    .Select(facilityDepartment => new MedicalFacilityDepartmentResponse
                    {
                        DepartmentId = facilityDepartment.DepartmentId,
                        DepartmentName = facilityDepartment.Department!.DepartmentName,
                        Description = facilityDepartment.Department.Description,
                    })
                    .ToList()));
    }
}
