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
            var login = repository.Login(loginVM);
            if (login == "1")
            {
                return Ok(new { status = HttpStatusCode.BadRequest, result = login, message = "nik / email tidak sesuai dengan data didatabase" });
            }
            else if (login == "0") 
            {
                return Ok(new { status = HttpStatusCode.BadRequest, result = login, message = "Password Salah" });
            }
            else if (login == "2")
            {
                return Ok(new { status = HttpStatusCode.BadRequest, result = login, message = "Masukan Password" });
            }
            else 
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Login Sukses", token = login });
            }
        }

        [HttpPost("ResetPassword")]
        public ActionResult ResetPassword(ForgotPasswordRequestVM forgotPasswordRequestVM)
        {
            var reset = repository.ResetPassword(forgotPasswordRequestVM);
            return Ok(reset);
        }

        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(ForgotPasswordRequestVM forgotPasswordRequestVM)
        {
            var result = repository.ChangePassword(forgotPasswordRequestVM);
            if (result == 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Sukses" });
            }
            else if (result == 1)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, result = result, message = "Wajib Masukan NIK atau Email" });
            }
            else if (result == 2)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, result = result, message = "Old Password dan New Password tidak ada" });
            }
            else if (result ==3)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, result = result, message = "Old Password tidak ada" });
            }
            else if (result == 4)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, result = result, message = "New Password tidak ada" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.BadRequest, result = result, message = "Password yang lama tidak cocok" });
            }
        }

        //test kirim ke Email
        [HttpPost("TestSendEmail")]
        public ActionResult TestSendEmail(ForgotPasswordRequestVM forgotPasswordRequestVM)
        {
            var acak = repository.SendEmailForgotPassword(forgotPasswordRequestVM);
            return Ok(acak);
        }
    }
}
