using API.Base;
using API.Model;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [EnableCors("AllowOrigin")]
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
            if (register == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, result = register, message = "succes" });
            }
            else if (register == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = register, message = "nik sudah ada" });
            }
            else if (register == 3)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = register, message = "email sudah ada" });
            }
            else if (register == 4)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = register, message = "nik dan email sudah ada" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = register, message = "Gagal menyimpan, data harus diisi semua" });
            }
        }

        //[Authorize(Roles = "Employee")]
        [HttpGet("ShowProfile")]
        public ActionResult ShowProfile()
        {
            var result = repository.ShowProfile();
            if (result == null)
            {
                return Ok(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("ShowProfile/{nik}")]
        public ActionResult ShowProfile(string nik)
        {
            var result = repository.ShowProfile(nik);
            if (result == null)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, result = result, message = "Data tidak Ada" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result = result});
            }
        }
    }
}
