using AutoMapper;
using BLL.Common;
using BLL.DTOs.Courses;
using BLL.DTOs.CourseUsers;
using BLL.Exceptions;
using DAL.Aggregates;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;

namespace BLL.Services;

public class Common : ICommon
{
    private readonly ISharedRepositories _sharedRepositories;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<Course> _courseRepository;
    private readonly IGenericRepository<CourseUser> _courseUserRepository;
    private readonly IGenericRepository<Block> _blockRepository;
    private readonly IGenericRepository<MarkdownDocument> _markdownDocumentRepository;

    public Common(ISharedRepositories sharedRepositories, IMapper mapper, IConfiguration configuration)
    {
        _mapper = mapper;
        _configuration = configuration;
        _sharedRepositories = sharedRepositories;
        _userRepository = _sharedRepositories.RepositoriesManager.UserRepository;
        _courseRepository = _sharedRepositories.RepositoriesManager.CourseRepository;
        _courseUserRepository = _sharedRepositories.RepositoriesManager.CourseUserRepository;
        _blockRepository = _sharedRepositories.RepositoriesManager.BlockRepository;
        _markdownDocumentRepository = _sharedRepositories.RepositoriesManager.MarkdownDocumentRepository;
    }

    public bool IsUserIdValid(Guid userId)
    {
        if (_userRepository.GetById(userId) is not null) return true;
        return false;
    }
    public User? GetUserByUsername(string? username)
    {
        try
        {
            if (!_userRepository.Get(user => user.Username == username, null, 1).Any()) return null;
            return _userRepository.Get(user => user.Username == username, null, 1).First();
        }
        catch (Exception)
        {
            throw new ResourceNotFoundException("username is not found");
        }
    }
    public User? GetUserById(string? id)
    {
        try
        {
            if (!_userRepository.Get(user => user.Id.ToString() == id).Any())
            {
                throw new ResourceNotFoundException("UserId is invalid");
            }
            return _userRepository.Get(user => user.Id.ToString() == id).FirstOrDefault();
        }
        catch (Exception e)
        {
            throw new ResourceConflictException(e.Message);
        }
    }

    public bool CheckDuplicateCourseCode(CourseDTO course)
    {
        try
        {
            if (_courseRepository.Get(c => c.Coursecode == course.Coursecode && c.Id != course.Id).Any())
            {
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            throw new ResourceNotFoundException(e.Message);
        }
    }

    public bool CheckDuplicateCourseName(CourseDTO course)
    {
        try
        {
            if (_courseRepository.Get(c => c.Coursename == course.Coursename && c.Id != course.Id).Any())
            {
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            throw new ResourceNotFoundException(e.Message);
        }
    }

    public Course? GetCourseByName(string? name)
    {
        if (!_courseRepository.Get(c => c.Coursename == name, null, 1).Any()) return null;
        return _courseRepository.Get(c => c.Coursename == name, null, 1).First();
    }

    public Guid? GetCourseIdByName(string? name)
    {
        if (!_courseRepository.Get(course => course.Coursename == name, null, 1).Any()) return null;
        Course found = _courseRepository.Get(course => course.Coursename == name, null, 1).First();

        return found.Id;
    }

    public CourseUserDTO AssignLecturerToCourse(User lecturer, Course course)
    {
        try
        {
            var course_user = new CourseUser();
            course_user.UserId = lecturer.Id;
            course_user.CourseId = course.Id;
            if (!_courseUserRepository.Get(cu => cu.UserId == course_user.UserId && cu.CourseId == course_user.CourseId).Any())
            {
                _courseUserRepository.Insert(course_user);
                _sharedRepositories.RepositoriesManager.Saves();
                CourseUserDTO dto = _mapper.Map<CourseUserDTO>(course_user);

                return dto;
            }

            throw new ResourceConflictException("There is another of ref of this course and lecturer existed");
        }
        catch (Exception e)
        {
            throw new ResourceConflictException(e.Message);
        }
    }

    public CourseUserDTO ChangeLecturerOfCourse(User lecturer, Course course)
    {
        try
        {
            CourseUser old_course_user = new CourseUser();
            old_course_user.UserId = course.LecturerId;
            old_course_user.CourseId = course.Id;
            if (_courseUserRepository.Get(cu => cu.UserId == old_course_user.UserId && cu.CourseId == old_course_user.CourseId).Any())
            {
                _courseUserRepository.Delete(old_course_user);
            }

            CourseUser new_course_user = new CourseUser();
            new_course_user.UserId = lecturer.Id;
            new_course_user.CourseId = course.Id;
            _courseUserRepository.Insert(new_course_user);
            _sharedRepositories.RepositoriesManager.Saves();
            CourseUserDTO dto = _mapper.Map<CourseUserDTO>(new_course_user);

            return dto;
        }
        catch (Exception e)
        {
            throw new ResourceConflictException(e.Message);
        }
    }

    public CourseUserDTO DeleteLecturerFromCourse(User lecturer, Course course)
    {
        try
        {
            CourseUser course_user = new CourseUser();
            course_user.UserId = lecturer.Id;
            course_user.CourseId = course.Id;

            if (_courseUserRepository.Get(cu => cu.UserId == course_user.UserId && cu.CourseId == course_user.CourseId).Any())
            {
                _courseUserRepository.Delete(course_user);
                _sharedRepositories.RepositoriesManager.Saves();
                CourseUserDTO dto = _mapper.Map<CourseUserDTO>(course_user);

                return dto;
            }

            throw new ResourceNotFoundException("There is no ref of this course and user existed");
        }
        catch (Exception e)
        {
            throw new ResourceConflictException(e.Message);
        }
    }

    public IEnumerable<CourseUserDTO> DeleteAllStudentsFromCourse(Course course)
    {
        try
        {
            List<CourseUser> ref_list = _courseUserRepository.Get(c => c.CourseId == course.Id).ToList();
            if (ref_list.Any())
            {
                foreach (CourseUser user in ref_list)
                {
                    User student = _userRepository.GetById(user.UserId);
                    if (student != null && student.Role == "student")
                    {
                        _courseUserRepository.Delete(user);
                    }
                }

                _sharedRepositories.RepositoriesManager.Saves();
                return _mapper.Map<List<CourseUserDTO>>(ref_list);
            }

            throw new ResourceNotFoundException();

        }
        catch (Exception e)
        {
            throw new ResourceConflictException(e.Message);
        }
    }

    public IEnumerable<CourseUserDTO> GetAllCourseRefOfUser(string? userid)
    {
        try
        {
            List<CourseUser> list = _courseUserRepository.Get(cu => cu.UserId.ToString() == userid).ToList();
            return _mapper.Map<List<CourseUserDTO>>(list);
        }
        catch (Exception e)
        {
            throw new ResourceNotFoundException(e.Message);
        }
    }

    public IEnumerable<Guid?> DeleteAllMarkdownDocFromBlock(Guid? block_id)
    {
        try
        {
            List<MarkdownDocument> delete_list = _markdownDocumentRepository.Get(doc => doc.BlockId == block_id).ToList();
            List<Guid?> response = new List<Guid?>();
            if(delete_list != null)
            {
                foreach (MarkdownDocument markdownDocument in delete_list)
                {
                    _markdownDocumentRepository.Delete(markdownDocument);
                    response.Add(markdownDocument.Id);
                }
                _sharedRepositories.RepositoriesManager.Saves();
                return response;
            }

            return null;
        }
        catch (Exception e)
        {
            throw new ResourceNotFoundException(e.Message);
        }
    }
}