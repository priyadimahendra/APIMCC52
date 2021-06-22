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
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        private AccountRepository repository;
        public AccountController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var login = repository.LoginNIK(loginVM);
            if (login == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, result = login, message = "Login Sukses" });
            }
            else if (login == 0) 
            {
                return Ok(new { status = HttpStatusCode.OK, result = login, message = "Password Salah" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result = login, message = "nik tidak sesuai dengan data didatabase" });
            }
        }

        [HttpPost("Login/{email}")]
        public ActionResult Login(LoginVM loginVM, string email)
        {
            var login = repository.LoginEmail(loginVM, email);
            if (login == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, result = login, message = "Login Sukses" });
            }
            else if (login == 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = login, message = "Password Salah" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result = login, message = "Email tidak sesuai dengan data didatabase" });
            }
        }
    }
}
