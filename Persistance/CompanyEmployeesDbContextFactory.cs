using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class CompanyEmployeesDbContextFactory : IDesignTimeDbContextFactory<CompanyEmployeesDbContext>
    {
        public CompanyEmployeesDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CompanyEmployeesDbContext>();
            optionsBuilder.UseSqlite(args.Length != 0 ? args[0] : Path.Combine(Environment.CurrentDirectory, "CompanyAnalitics.db"));
            return new CompanyEmployeesDbContext(optionsBuilder.Options);
        }
    }
}
