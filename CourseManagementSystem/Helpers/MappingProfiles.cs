using AutoMapper;
using Course.DAL.Models;
using CourseManagementSystem.ViewModels;
namespace CourseManagementSystem.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<StudentViewModel, ApplicationUsers>()
                .ReverseMap();
            CreateMap<Courses, CourseViewModel>()
                .ReverseMap();
            CreateMap<ApplicationUsers, InstructorViewModel>()
                .ReverseMap();
        }
    }
}
