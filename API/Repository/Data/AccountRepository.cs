using API.Context;
using API.Model;
using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext context) : base(context)
        {
            this.myContext = context;
        }

        public int LoginNIK(LoginVM loginVM)
        {
            try
            {
                var find = myContext.Accounts.Find(loginVM.NIK);
                if (find.Password == loginVM.Password)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                
                return 2;
            }
        }

        public int LoginEmail(LoginVM loginVM, string emails)
        {
            try
            {
                var email = myContext.Employees.FirstOrDefault(e => e.Email == emails);
                loginVM.NIK = email.NIK;
                var findemail = myContext.Accounts.Find(loginVM.NIK);
                if (findemail.Password == loginVM.Password)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {

                return 2;
            }
        }
    }
}
