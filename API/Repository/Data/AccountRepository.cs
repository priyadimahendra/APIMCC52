using API.Context;
using API.Model;
using API.ViewModel;
using API.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext context) : base(context)
        {
            this.myContext = context;
        }

        public string Login(LoginVM loginVM)
        {
            try
            {
                if (loginVM.NIK != null && loginVM.Password != null)// pakai nik
                {
                    var find = myContext.Accounts.Find(loginVM.NIK);
                    var checkPassword = Bcrypt.ValidatePassword(loginVM.Password, find.Password);
                    if (checkPassword == true)
                    {
                        var result = Authentication(loginVM.NIK);
                        return result;
                    }
                    else
                    {
                        return "0";
                    }
                }
                else if (loginVM.Email != null && loginVM.Password != null)// pakai email
                {
                    var email = myContext.Employees.FirstOrDefault(e => e.Email == loginVM.Email);
                    loginVM.NIK = email.NIK;
                    var findemail = myContext.Accounts.Find(loginVM.NIK);
                    var checkPassword = Bcrypt.ValidatePassword(loginVM.Password, findemail.Password);
                    if (checkPassword == true)
                    {
                        var result = Authentication(loginVM.NIK);
                        return result;
                    }
                    else
                    {
                        return "0";
                    }
                }
                else// gak ada password atau gak ada semua
                {
                    return "2";
                }
            }
            catch (Exception)
            {
                return "1";
            }
        }

        public string ResetPassword(ForgotPasswordRequestVM forgotPasswordRequestVM)
        {
            var checkAccount = myContext.Employees.FirstOrDefault(e => e.Email == forgotPasswordRequestVM.Email);
            if (checkAccount == null) return "Email tidak Terdaftar";
            
            var nikAccount = checkAccount.NIK;
            var account = myContext.Accounts.Find(nikAccount);
            string passwordBaru = Guid.NewGuid().ToString();
            var passwordBaruHash = Bcrypt.HashPassword(passwordBaru); //bcryp password
            account.Password = passwordBaruHash;
            myContext.SaveChanges();

            var reset = Email.SendEmailResetPassword(forgotPasswordRequestVM.Email,passwordBaru, checkAccount.FirstName); //kirim keemail

            return reset;
        }

        public int ChangePassword(ForgotPasswordRequestVM forgotPasswordRequestVM)
        {
            if (forgotPasswordRequestVM.NIK == null && forgotPasswordRequestVM.Email == null) return 1;// NIK dan Email tidak ada

            if (forgotPasswordRequestVM.NIK != null && forgotPasswordRequestVM.Email == null) //pake NIK
            {
                var account = myContext.Accounts.Find(forgotPasswordRequestVM.NIK);
                if (forgotPasswordRequestVM.NewPassword == null && forgotPasswordRequestVM.OldPassword == null) return 2; //NewPassword & OldPassword gak ada
                if (forgotPasswordRequestVM.NewPassword != null && forgotPasswordRequestVM.OldPassword == null) return 3; //OldPassword gak ada
                if (forgotPasswordRequestVM.NewPassword == null && forgotPasswordRequestVM.OldPassword == null) return 4; //NewPassword gak ada

                var checkPassword = Bcrypt.ValidatePassword(forgotPasswordRequestVM.OldPassword, account.Password);

                if (!checkPassword) 
                {
                    return 5;//OldPassword salah
                }
                else
                {
                    var newPasswordHash = Bcrypt.HashPassword(forgotPasswordRequestVM.NewPassword);
                    account.Password = newPasswordHash;

                    myContext.Entry(account).State = EntityState.Modified;
                    myContext.SaveChanges();
                    return 0;
                }
            }
            else //pake Email
            {
                var employee = myContext.Employees.FirstOrDefault(e => e.Email == forgotPasswordRequestVM.Email);
                var account = myContext.Accounts.Find(employee.NIK);
                if (forgotPasswordRequestVM.NewPassword == null && forgotPasswordRequestVM.OldPassword == null) return 2; //NewPassword & OldPassword gak ada
                if (forgotPasswordRequestVM.NewPassword != null && forgotPasswordRequestVM.OldPassword == null) return 3; //OldPassword gak ada
                if (forgotPasswordRequestVM.NewPassword == null && forgotPasswordRequestVM.OldPassword == null) return 4; //NewPassword gak ada

                var checkPassword = Bcrypt.ValidatePassword(forgotPasswordRequestVM.OldPassword, account.Password);

                if (!checkPassword)
                {
                    return 5;//OldPassword salah
                }
                else
                {
                    var newPasswordHash = Bcrypt.HashPassword(forgotPasswordRequestVM.NewPassword);
                    account.Password = newPasswordHash;

                    myContext.Entry(account).State = EntityState.Modified;
                    myContext.SaveChanges();
                    return 0;
                }
            }
        }

        private string Authentication(string nik)
        {
            //create claims details based on the user information
            var employee = myContext.Employees.Find(nik);
            List<AccountRole> accountRoles = myContext.AccountRoles.Where(a => a.NIK == nik).ToList();
            var accountRoleAdmin = myContext.AccountRoles.FirstOrDefault(e => e.NIK == nik && e.RoleId == "02");

            if (accountRoleAdmin != null)
            {
                var role = myContext.Roles.Find("02");
                var role2 = myContext.Roles.Find("01");
                var claims = new[] {
                new Claim("email", employee.Email),
                new Claim("role", role.RoleName),
                new Claim("role", role2.RoleName)
                };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf"));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken("API", "Test", claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    var result = new JwtSecurityTokenHandler().WriteToken(token);
                    return result;
            }
            else
            {
                var role = myContext.Roles.Find("01");
                var claims = new[] {
                new Claim("email", employee.Email),
                new Claim("role", role.RoleName)
                };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf"));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken("API", "Test", claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    var result = new JwtSecurityTokenHandler().WriteToken(token);
                    return result;
            }
        }

        // Test Kirim Email
        public string SendEmailForgotPassword(ForgotPasswordRequestVM forgotPasswordRequestVM)
        {
            try
            {
                var fromAddress = new MailAddress("	akunt5634@gmail.com");
                var passwordFrom = "akunt3$t";
                var toAddress = new MailAddress(forgotPasswordRequestVM.Email);
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, passwordFrom)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "gg",
                    Body = "dsfs",
                    IsBodyHtml = true,
                }) smtp.Send(message);

                // guid
                string guid = Guid.NewGuid().ToString();
                return guid;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
