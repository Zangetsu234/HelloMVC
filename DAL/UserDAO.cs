using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL
{
    public class UserDAO
    {
        public void Write(string statement, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=HelloMVC;Integrated Security=SSPI;"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    command.ExecuteNonQuery();
                }
            }
        }
        public List<User> ReadUser(string statement, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=HelloMVC;Integrated Security=SSPI;"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SqlDataReader data = command.ExecuteReader();
                    List<User> users = new List<User>();
                    while (data.Read())
                    {
                        User user = new User();
                        user.ID = Convert.ToInt32(data["ID"]);
                        user.Email = data["Email"].ToString();
                        user.Password = data["Password"].ToString();
                        users.Add(user);
                    }
                    try
                    {
                        return users;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }
        public void CreateUser(User user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Password", user.Password)
            };
            Write("CreateUser", parameters);
        }
        public void UpdateUser(User user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", user.ID),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Password", user.Password)
            };
            Write("UpdateUser", parameters);
        }
        public User GetUserByEmail(string email)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", email)
            };
            return ReadUser("GetUserByEmail", parameters).SingleOrDefault();
        }
        public User GetUserByID(int ID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", ID)
            };
            return ReadUser("GetUserByID", parameters).SingleOrDefault();
        }
        public List<User> GetAllUsers()
        {
            return ReadUser("GetAllUsers", null);
        }
    }
}