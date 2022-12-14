using DAL.Aggregates;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.RepositoriesManager;

public interface IRepositoriesManager : IDisposable
{
    IGenericRepository<User> UserRepository { get; }
    IGenericRepository<Course> CourseRepository { get; }
    IGenericRepository<CourseUser> CourseUserRepository { get; }
    IGenericRepository<Block> BlockRepository { get; }
    IGenericRepository<MarkdownDocument> MarkdownDocumentRepository { get; }
 
    DbContext Context { get; }

    public void Saves();

    public void Dispose(bool disposing);
}