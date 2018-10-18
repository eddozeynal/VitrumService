using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPService
{
    public static class DBBasicGate
    {
        public static List<T> GetAll<T>(IDbConnection connection)
        {
            string TableName = typeof(T).Name;
            string query = "SELECT * FROM " + TableName;
            return connection.Query<T>(query).ToList();
        }
        public static List<T> GetAll<T>(IDbConnection connection, string condition)
        {
            string TableName = typeof(T).Name;
            string query = "SELECT * FROM " + TableName + " " + condition;
            return connection.Query<T>(query).ToList();
        }
    }
}
