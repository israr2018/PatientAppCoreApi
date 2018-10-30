using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientCoreApi.Models;

namespace PatientCoreApi.Data
{
    public class PatientDbContext:DbContext
    {
        public PatientDbContext()
        {
        }

        public PatientDbContext(DbContextOptions<PatientDbContext> options):base(options)
        {

        }
        public DbSet<Patient> Patients { get; set; }

    }
}
