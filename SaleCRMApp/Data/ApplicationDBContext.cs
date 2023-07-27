using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using SaleCRMApp.Models;

namespace SaleCRMApp.Data
{
    public class ApplicationDBContext : IdentityDbContext 
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SaleLeadEntity> SaleLead { get; set;}
    }
}
