using AutoMapper;
using Employee.Management.MVC.Models.Employee;
using System.Reflection;

namespace Employee.Management.MVC.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDetails, UpdateEmployeeViewModel>()
                 .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToDateTime(TimeOnly.MinValue)));

            CreateMap<UpdateEmployeeViewModel, UpdateEmployee>()
                 .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateOfBirth)));
        }
    }
}
