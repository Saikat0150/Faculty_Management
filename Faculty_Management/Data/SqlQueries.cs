using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Faculty_Management.Data
{
    public class SqlQueries
    {
        public static string GetConnectionString(string connectionName = "Options")
        {
            //var connectionString = "Data Source=tcp:facultyinformationsystemdbserver.database.windows.net,1433;Initial Catalog=FacultyInformationSystem_db;User Id=KetanRaul@facultyinformationsystemdbserver;Password=Rajkunwar@1";
            var connectionString = "Server=DESKTOP-JSBRBHQ\\SQLEXPRESS;Initial Catalog=FacultyBassetti;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
            return connectionString;
        }
        public static int CreateUser<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);
            }
        }

        public static List<T> CheckUser<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql, data).ToList();
            }
        }

    }
}