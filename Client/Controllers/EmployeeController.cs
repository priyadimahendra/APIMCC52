using API.Model;
using Client.Base;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class EmployeeController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeeController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ShowProfile()
        {
            var result = await repository.ShowProfile();
            return Json(result);
        }

        [HttpGet("Employee/ShowProfile/{nik}")]
        public async Task<JsonResult> ShowProfile(string nik)
        {
            var result = await repository.ShowProfile(nik);
            return Json(result);
        }
    }
}
