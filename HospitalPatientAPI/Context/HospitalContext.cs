using Castle.Core.Resource;
using HospitalPatientAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalPatientAPI.Context
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
    }
}
