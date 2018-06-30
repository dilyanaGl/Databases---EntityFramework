using System;
using System.Linq;
using MiniOrmApp.Data;
using MiniOrmApp.Data.Entities;

namespace MiniOrmApp
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string connectionString = "Server =.;Database=MiniORM;Integrated security=True";

            var context = new SoftuniDbContext(connectionString);

            context.Employees.Add(new Employee
            {
                FirstName = "Gosho",
                LastName = "Petrov",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true
            });

            var employee = context.Employees.Last();

            employee.FirstName = "Modified";

            context.SaveChanges();




        }
    }
}

