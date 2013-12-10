using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class UserService
    {
        public UsersVM GetUsers()
        {
            UserDAO dao = new UserDAO();
            List<User> users = dao.GetAllUsers();
            UsersVM usersVM = new UsersVM();
            foreach(User user in users)
            {
                UserVM userVM = new UserVM();
                userVM.UserID = user.ID;
                userVM.Email = user.Email;
                usersVM.Users.Add(userVM);
            }
            return usersVM;
        }
        //public User ConvertUser(UserFM user)
        //{
        //    User userDAO = new User();
        //    userDAO.Email = user.Email;
            
        //}
        public bool CreateUser(UserFM userFM)
        {
            if (IsValidUser(userFM))
            {
                //email temp pass to user
                UserDAO dao = new UserDAO();
                User user = new User();
                user.Email = userFM.Email;
                user.Password = RandomPassword();
                dao.CreateUser(user);
                return true;
            }
            return false;
        }
        private string RandomPassword()
        {
            string password = "";
            Random rng = new Random();
            for (int i = 0; i < 8; i++)
            {
                int a = rng.Next(1, 4);
                switch(a)
                {
                    case 1://Uppercase
                        password = password + Convert.ToChar(rng.Next(65, 91));
                        break;
                    case 2://Number
                        password = password + Convert.ToChar(rng.Next(48, 58));
                        break;
                    case 3://Lowercase
                        password = password + Convert.ToChar(rng.Next(97, 123));
                        break;
                }
            }
            return password;
        }
        public bool IsValidUser(UserFM userFM)
        {
            UserDAO dao = new UserDAO();
            if(userFM.Email != null && userFM.Email.Length > 5 && dao.GetUserByEmail(userFM.Email) == null)
            {
                return true;
            }
            return false;
        }
        public UserFM GetUserFM(int ID)
        {           
            UserDAO dao = new UserDAO();
            User user = dao.GetUserByID(ID);
            UserFM userFM = new UserFM(user);
            return userFM;
        }
        public void UpdateUser(UserFM userFM)
        {
            UserDAO dao = new UserDAO();
            User user = dao.GetUserByID(userFM.ID);
            user.Email = userFM.Email;
            dao.UpdateUser(user);
        }
        //public bool IsValidPassword(PasswordFM passwordFM)
        //{
        //    UserDAO dao = new UserDAO();
        //    if (passwordFM.Password != null && passwordFM.Password.Length > 5 && dao.GetUserByEmail(passwordFM.Password) == null)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        public PasswordFM GetPasswordFM(int ID)
        {
            UserDAO dao = new UserDAO();
            User user = dao.GetUserByID(ID);
            PasswordFM passwordFM = new PasswordFM(user);
            return passwordFM;
        }
        public void UpdatePassword(PasswordFM passwordFM)
        {
            UserDAO dao = new UserDAO();
            User user = dao.GetUserByID(passwordFM.ID);
            user.Password = passwordFM.NewPassword;
            dao.UpdateUser(user);
        }
        public void DeleteUser(int ID)
        {
            UserDAO dao = new UserDAO();
            dao.DeleteUser(ID);
        }

        public bool VerifyPassword(PasswordFM passwordFM)
        {
            if(passwordFM.CurrentPassword == GetPasswordFM(passwordFM.ID).CurrentPassword)
            {
                return true;
            }
            return false;
        }
    }
}