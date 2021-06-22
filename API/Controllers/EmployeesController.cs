using API.Base;
using API.Model;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private EmployeeRepository repository;

        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var register = repository.RegisterRepo(registerVM);
            if (register > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = register, message = "Succes" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result = register, message = "NIK atau Email Sudah Ada" });
            }
        }
    }
}
