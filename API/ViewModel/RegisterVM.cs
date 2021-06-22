using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class RegisterVM
    {
        //Employee
        public string NIK { get; set; } //Account, Employee, Profiling
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }

        //Education
        public string Degree { get; set; }
        public string GPA { get; set; }

        //Account
        public string Password { get; set; }

        //Profiling
        public string UniversityId { get; set; }
        public string EducationId { get; set; }

    }
}
