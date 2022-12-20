using BLL.DTOs.Courses;
using BLL.DTOs.CourseUsers;
using DAL.Aggregates;

namespace BLL.Services;

public interface ICommon
{
    //User:
    bool IsUserIdValid(Guid userId);
    public User? GetUserByUsername(string? username);
    public User? GetUserById(string? id);

    //Course:
    public Guid? GetCourseIdByName(string name);
    public Course? GetCourseByName(string name);
    public Boolean CheckDuplicateCourseCode(CourseDTO course);
    public Boolean CheckDuplicateCourseName(CourseDTO course);    

    //CourseUser:
    public CourseUserDTO AssignLecturerToCourse(User lecturer, Course course);
    public CourseUserDTO ChangeLecturerOfCourse(User lecturer, Course course);
    public CourseUserDTO DeleteLecturerFromCourse(User lecturer, Course course);
    public IEnumerable<CourseUserDTO> DeleteAllStudentsFromCourse(Course course);
    public IEnumerable<CourseUserDTO> GetAllCourseRefOfUser(string? userid);
    public IEnumerable<User> GetStudentsInCourse(Guid? course_id);

    //Block:

    //MarkdownDocument:
    public IEnumerable<Guid?> DeleteAllMarkdownDocFromBlock(Guid? block_id);
}