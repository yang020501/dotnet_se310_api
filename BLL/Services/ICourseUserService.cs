using BLL.DTOs.CourseUsers;
using BLL.DTOs.Users;
using DAL.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ICourseUserService
    {
        public CourseUserRespone AddUserToCourse(CourseUserRequest request);
        public CourseUserRespone RemoveUserFromCourse(CourseUserRequest request);
        public CourseUserRespone MoveUserToAnotherCourse(CourseUserRequest request);
        public CourseUserDTO AssignLecturerToCourse(User lecturer, Course course);
        public CourseUserDTO ChangeLecturerOfCourse(User lecturer, Course course);
        public CourseUserDTO DeleteLecturerFromCourse(User lecturer, Course course);
        public IEnumerable<CourseUserDTO> DeleteAllStudentsFromCourse(Course course);
        public IEnumerable<CourseUserDTO> GetAllCourseRefOfUser(string? userid);
        public AddStudentToCourseResponse AddStudentsToCourse(AddStudentToCourseRequest request);
        public RemoveStudentFromCourseResponse RemoveStudentsFromCourse(RemoveStudentFromCourseRequest request);
    }
}
