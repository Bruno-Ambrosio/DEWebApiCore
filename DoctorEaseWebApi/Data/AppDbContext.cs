using DoctorEaseWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorEaseWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
