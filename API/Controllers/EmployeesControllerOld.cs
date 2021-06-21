using API.Model;
using API.Repository;
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
    public class EmployeesControllerOld : ControllerBase
    {
        private EmployeeRepository employeeRepository;

        public EmployeesControllerOld(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]//tambahan buat bedain getnya
        public ActionResult Get()
        {
             return Ok(employeeRepository.Get());// jadiin objeck status code, result, pesan.
        }

        [HttpGet("{nik}")]//tambahan buat bedain getnya
        public ActionResult Get(string nik)
        {
            var get = employeeRepository.Get(nik);
            if (get == null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = get, message = "Tidak Ditemukan"});
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result = get, message = "Data Ada" });
            }
        }
        //tambah trycatch

        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            var insert = employeeRepository.Insert(employee);
            if (insert == 1)
            {
                return Ok("Post Success");
            }
            else
            {
                return BadRequest("Fail To Insert");
            }
        }

        [HttpDelete]
        public ActionResult Delete(string nik)
        {
            var response = employeeRepository.Delete(nik);
            if (nik == null)
            {
                return BadRequest("Need NIK");
            }
            else
            {
                if (response == 1)
                {
                    return Ok("Delete Success");
                }
                else
                {
                    return Ok("Delete Success");
                }
            }
        }

        [HttpPut]
        public ActionResult Update(Employee employee, string nik)
        {
            var response = employeeRepository.Update(employee, nik);
            if (nik == null)
            {
                return BadRequest("Need NIK");
            }
            else
            {
                if (response == 1)
                {
                    return Ok("Update Success");
                }
                else
                {
                    return Ok("Update Success");
                }
            }
        }
    }
}
