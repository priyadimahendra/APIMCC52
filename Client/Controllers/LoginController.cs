using API.Model;
using API.ViewModel;
using Client.Base;
using Client.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepository repository;
        public LoginController(LoginRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Auth(LoginVM loginVM)
        {
            var jwtoken = await repository.Auth(loginVM);
            if (jwtoken == null)
            {
                return RedirectToAction("index");
            }
            HttpContext.Session.SetString("Jwt", jwtoken.Token); // nama Jwt itu harus sama dengan yang di appsetting.json
            return RedirectToAction("index","employee");
        }
    }
}
