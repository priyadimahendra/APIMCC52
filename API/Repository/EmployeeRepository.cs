using API.Context;
using API.Model;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext myContext;

        public EmployeeRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }


        public IEnumerable<Employee> Get()
        {
            var employee = myContext.Employees.ToList();
            return employee;
        }

        public Employee Get(string nik)
        {
            // bila tidak ada parameter maka akan eror untuk id/PK
            //var find = myContext.Employees.Find(nik);

            // bila parameter di kosongkan maka akan mengambil data yang pertama
            // untuk FOD itu bila udah dapat yang benar dicek maka langsung dikirim tanpa cek semuanya
            var find =  myContext.Employees.FirstOrDefault(e => e.NIK == nik);
            return find;


            // bila tidak ada parameternya maka method ini akan eror
            //var find = myContext.Employees.SingleOrDefault(c => c.NIK == nik);

            // parameter selain id/PK untuk SOD cek semua data.
        }

        public int Insert(Employee employee)
        {
            myContext.Employees.Add(employee);
            var insert = myContext.SaveChanges();
            return insert;//catatan 
        }

        public int Update(Employee employee, string nik)
        {
            var employees = myContext.Employees.Find(nik);
            myContext.Employees.Attach(employees);
            if (employee.FirstName != null)
            {
                employees.FirstName = employee.FirstName;
            }
            if (employee.LastName != null)
            {
                employees.LastName = employee.LastName;
            }
            if (employee.Email != null)
            {
                employees.Email = employee.Email;
            }
            if (employee.Salary != 0)
            {
                employees.Salary = employee.Salary;
            }
            if (employee.PhoneNumber != null)
            {
                employees.PhoneNumber = employee.PhoneNumber;
            }
            if (employee.BirthDate != null)
            {
                employees.BirthDate = employee.BirthDate;
            }
            myContext.Employees.Update(employees);
            var update = myContext.SaveChanges();
            return update;
        }

        public int Delete(string nik)
        {
            var find = myContext.Employees.Find(nik);
            myContext.Employees.Remove(find);
            var delete = myContext.SaveChanges();
            return delete;
        }
    }
}
