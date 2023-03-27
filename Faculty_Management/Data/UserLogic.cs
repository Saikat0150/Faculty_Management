using Faculty_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Faculty_Management.Data
{
    public class UserLogic : SqlQueries
    {
        public static int SignUpUser(int user_id, string name, string e_mail, string pass, int user_type)
        {
            User data = new User
            {
                UserId = user_id,
                Name=name,
                Email = e_mail,
                Password = pass,
                UserType = 1 //user_type
            };

            string sql = @"INSERT INTO [Users](UserId, Name, Email, Password, UserType) VALUES(@UserId, @Name, @Email, @Password, @UserType);";

            return SqlQueries.CreateUser(sql, data);
        }


        public static List<User> SignInUser(int user_id, string name, string e_mail, string pass, int user_type)
        {
            User data = new User
            {
                UserId = user_id,
                Name = name,
                Email = e_mail,
                Password = pass,
                UserType = user_type
            };

            string sql = @"SELECT * FROM [Users] WHERE(Email = @Email AND Password = @Password);";

            return SqlQueries.CheckUser<User>(sql, data);
        }

    }
}