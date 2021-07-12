using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class ShowProfile
    {
        //nama objek semua ini harus sama dengan yang ada di API ShowProfile

        public string NIK { get; set; } 

        public string RoleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }

        public string Degree { get; set; }
        public string GPA { get; set; }

        public string Name { get; set; }
    }
}
