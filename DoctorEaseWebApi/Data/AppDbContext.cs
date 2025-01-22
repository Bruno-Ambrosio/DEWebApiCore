using DEWebApi.Models;
using DoctorEaseWebApi.Enum;
using DoctorEaseWebApi.Models;
using DoctorEaseWebApi.Services.Password;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DoctorEaseWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<GenderModel> Genders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
