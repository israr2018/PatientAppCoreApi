using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace PatientCoreApi.Data
{
    public class UserDbContext:IdentityDbContext<Microsoft.AspNetCore.Identity.IdentityUser>

   {

        public UserDbContext(DbContextOptions<UserDbContext> options):base(options)
        {
            //public DbSet<IdentityUserContext> User { get; set; }
        } 

    }
}
