using AutoMapper;
using Employee.Management.Application.DTOs.Auth;
using Employee.Management.Application.DTOs.Employee;
using Employee.Management.Domain.Entities;
using Employee.Management.Domain.Entities.ApplicationUser;

namespace Employee.Management.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EmployeeDetail, CreateEmployeeDto>().ReverseMap();

        CreateMap<EmployeeDetail, UpdateEmployeeDto>()
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();

        CreateMap<ApplicationUser, CreateUserRequestDto>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
            .ReverseMap();
    }
}
