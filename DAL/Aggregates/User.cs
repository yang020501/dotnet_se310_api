using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Aggregates;

[Table("users")]
public class User
{
    public User()
    {
        this.Courses = new HashSet<Course>();
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("username")]
    public string? Username { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("password")]
    public string? Password { get; set; }

    [Column("full_name")]
    public string? FullName { get; set; }

    [Column("role")]
    public string? Role { get; set; }

    [Column("avatar")]
    public string? Avatar { get; set; }

    public virtual ICollection<Course> Courses { get; set; }

}