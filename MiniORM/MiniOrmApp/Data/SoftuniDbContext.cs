using System;
using System.Collections.Generic;
using System.Text;
using MiniOrmApp.Data.Entities;
using MiniORM;

namespace MiniOrmApp.Data
{
    public class SoftuniDbContext : DbContext
    {
        public SoftuniDbContext(string connectionString) : base(connectionString)
        {
        }


        public DbSet<Employee> Employees { get; set; }

        public DbSet<Project> Projects{ get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<EmployeeProject> EmployeesProjects { get; set; }

    }
}
