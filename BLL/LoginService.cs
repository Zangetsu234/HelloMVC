using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class LoginService
    {
        public LoginVM Login(LoginFM login)
        {
            LoginVM userVM = null;
            UserDAO dao = new UserDAO();
            User user = dao.GetUserByEmail(login.Email);
            if(user != null && user.Password == login.Password)
            {
                userVM = new LoginVM();
                userVM.Email = user.Email;
                userVM.ID = user.ID;
            }
            return userVM;
        }
    }
}