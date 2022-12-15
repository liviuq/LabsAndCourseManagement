using AutoMapper;
using LabsAndCoursesManagement.API.DTOs;
using LabsAndCoursesManagement.Domain;

namespace LabsAndCoursesManagement.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateCourseDto, Course>();
            CreateMap<CreateGradeDto, Grade>();
            CreateMap<CreateStudentDto, Student>();
            CreateMap<CreateTeacherDto, Teacher>();
        }
    }
}
