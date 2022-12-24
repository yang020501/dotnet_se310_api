using DAL.Aggregates;
using Json.Net;
using Microsoft.EntityFrameworkCore;

namespace DAL.Configs;

public class MSSQLDbConfig : IConfig
{
    public MSSQLDbConfig(string json)
    {
        var data = JsonNet.Deserialize<dynamic>(json);
        Server = data.Server;
        Username = data.Username;
        Password = data.Password;
        Database = data.Database;
    }

    public MSSQLDbConfig()
    {
        Server = "sql8004.site4now.net";
        Username = "db_a92015_phungthanhtu4_admin";
        Password = "Phungthanhtu!1";
        Database = "db_a92015_phungthanhtu4";
    }

    public string Server { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Database { get; set; }

    public string GetConnectionString()
    {
        return $@"
            Server={Server};
            User Id={Username};
            Database={Database};
            Password={Password}
        ";
    }

    public DbContextOptionsBuilder UseDBMS(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        return optionsBuilder.UseSqlServer(GetConnectionString());
    }

    public void OnModelCreating(ModelBuilder modelBuilder)
    {
        var userHasTask = new User { Username = "sample4", Password = BCrypt.Net.BCrypt.HashPassword("sampass4"), Email = "sample4@sample.sample", FullName = "Sample User Four", Id = Guid.NewGuid() };
        var admin = new User { Username = "sudo", Password = BCrypt.Net.BCrypt.HashPassword("sudo"), Email = "admin@pro.org", FullName = "Super User Admin", Role="admin", Id = Guid.NewGuid() };

        modelBuilder.Entity<User>().HasData(admin, userHasTask);

        modelBuilder.Entity<Course>();

        modelBuilder.Entity<CourseUser>().HasKey(k => new { k.CourseId, k.UserId });

        modelBuilder.Entity<Block>();

        modelBuilder.Entity<MarkdownDocument>();
        
    }
}