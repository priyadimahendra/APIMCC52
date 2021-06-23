using API.Context;
using API.Model;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext context) : base(context)
        {
            this.context = context;
        }

        public int RegisterRepo(RegisterVM registerVM)
        {
            try
            {
                var ceknik = context.Employees.Find(registerVM.NIK);
                var cekemail = context.Employees.FirstOrDefault(e => e.Email == registerVM.Email);
                if (ceknik == null && cekemail == null) // gak ada yang sama
                {
                    Employee employee = new Employee();
                    employee.NIK = registerVM.NIK;
                    employee.FirstName = registerVM.FirstName;
                    employee.LastName = registerVM.LastName;
                    employee.Email = registerVM.Email;
                    employee.Salary = registerVM.Salary;
                    employee.PhoneNumber = registerVM.PhoneNumber;
                    employee.BirthDate = registerVM.BirthDate;


                    Account account = new Account();
                    account.NIK = registerVM.NIK;
                    account.Password = registerVM.Password;


                    Education education = new Education();
                    education.Id = registerVM.EducationId;
                    education.Degree = registerVM.Degree;
                    education.GPA = registerVM.GPA;
                    education.UniversityId = registerVM.UniversityId;


                    //Profiling profiling = new Profiling();
                    //profiling.NIK = registerVM.NIK;
                    //profiling.EducationId = registerVM.EducationId;
                    //context.Profilings.Add(profiling);
                    context.Employees.Add(employee);
                    context.Accounts.Add(account);
                    context.Educations.Add(education);
                    var insert = context.SaveChanges();
                    //return insert;
                    return 1;
                }
                else if (ceknik != null && cekemail == null) // nik sama
                {
                    return 2;
                }
                else if (ceknik == null && cekemail != null) // email sama
                {
                    return 3;
                }
                else //sama semua
                {
                    return 4;
                }
            }
            catch (Exception)
            {
                return 0; // data ada yang kurang
            }
            
        }
    }
}
