using DEWebApi.Models;
using DoctorEaseWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorEaseWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<GenderModel> Genders { get; set; }
        public DbSet<RoleModel> Roles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
