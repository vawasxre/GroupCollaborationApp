using GroupCollabApp.Areas.Identity.Data;
using GroupCollabApp.Models;
using GroupCollabApp.Views.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GroupCollabApp.Data;

public class GroupCollabAppAuthDbContext : IdentityDbContext<AppUser>
{
    public GroupCollabAppAuthDbContext(DbContextOptions<GroupCollabAppAuthDbContext> options)
        : base(options)
    {
    }

    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<CalendarEvent> CalendarEvents { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
       
       
    }
    
    
}
