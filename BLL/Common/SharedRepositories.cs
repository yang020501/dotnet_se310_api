using CQRSHandler;
using CQRSHandler.Abstractions;

using DAL;
using DAL.Configs;
using DAL.RepositoriesManager;
using Microsoft.Extensions.Configuration;


namespace BLL.Common;

public class SharedRepositories : ISharedRepositories
{
    private readonly IConfiguration _configuration;
    private readonly RepositoriesManager _repositoryManager;
    private readonly IDapperContext _dapperContext;
    

    public SharedRepositories(IConfiguration configuration)
    {
        _configuration = configuration;
        var dbconfig = new MSSQLDbConfig()
        {
            Server = _configuration["Database:Server"],
            Database = _configuration["Database:Database"],
            Username = _configuration["Database:Username"],
            Password = _configuration["Database:Password"]
        };

        _repositoryManager = new RepositoriesManager(new RepositoryContext(dbconfig));
        _dapperContext = new DapperContext(dbconfig.GetConnectionString());
    }

    public IRepositoriesManager RepositoriesManager { get => _repositoryManager; }

    public IDapperContext DapperContext => _dapperContext;

}