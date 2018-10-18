using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using DBModels;
using BusinessModels;
using Repositories;

namespace ERPService
{
    public static class DataIO
    {
        public static string ConnectionString = "Data Source = PROGRAMMER10; Database = VITRUMDB; User Id = AppUser; Password = AppUser;";
        public static IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        public static List<T> GetAllOff<T>() where T : class, new()
        {
            DBL.DBTools dbt = new DBL.DBTools(ConnectionString);
            DBL.DBOperation oper = dbt.GetDataTable(" SELECT * FROM " + typeof(T).Name, CommandType.Text);
            DataTable dt = oper.ConvertTo<DataTable>();
            return dt.ConvertDataTableToList<T>();
        }
        public static List<T> GetAllOff<T>(string Condition) where T : class, new()
        {
            DBL.DBTools dbt = new DBL.DBTools(ConnectionString);
            DBL.DBOperation oper = dbt.GetDataTable(" SELECT * FROM " + typeof(T).Name + " WHERE 0=0 AND " + Condition, CommandType.Text);
            DataTable dt = oper.ConvertTo<DataTable>();
            return dt.ConvertDataTableToList<T>();
        }
        public static List<T> GetAllOff<T>(IDbTransaction transaction) where T : class, new()
        {
            DBL.DBTools dbt = new DBL.DBTools(ConnectionString);
            DBL.DBOperation oper = dbt.GetDataTable(" SELECT * FROM " + typeof(T).Name, CommandType.Text);
            DataTable dt = oper.ConvertTo<DataTable>();
            return dt.ConvertDataTableToList<T>();
        }
        public static List<T> GetAllOff<T>(string Condition,IDbTransaction transaction) where T : class, new()
        {
            DBL.DBTools dbt = new DBL.DBTools(ConnectionString);
            DBL.DBOperation oper = dbt.GetDataTable(" SELECT * FROM " + typeof(T).Name + " WHERE 0=0 AND " + Condition, CommandType.Text);
            DataTable dt = oper.ConvertTo<DataTable>();
            return dt.ConvertDataTableToList<T>();
        }
        public static Operation<List<T>> Query<T>(string sqlQuery) where T : class, new()
        {
            Operation<List<T>> op_ = new Operation<List<T>>();
            try
            {
                List<T> lst = connection.Query<T>(sqlQuery).ToList();
                op_.Value = lst;
                op_.Successful = true;
            }
            catch (Exception ex)
            {
                op_.Fail = ex.Message;
            }
            return op_;
        }
        public static Operation<List<T>> Query<T>(string sqlQuery,IDbTransaction transaction) where T : class, new()
        {
            Operation<List<T>> op_ = new Operation<List<T>>();
            try
            {
                List<T> lst = connection.Query<T>(sqlQuery,transaction).ToList();
                op_.Value = lst;
                op_.Successful = true;
            }
            catch (Exception ex)
            {
                op_.Fail = ex.Message;
            }
            return op_;
        }
        public static T GetT<T>(object Id) where T : class, new ()
        {
            return connection.Get<T>(Id);
        }
        public static List<T> ConvertDataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        private static ICryptor _Cryptor = null;

        public static ICryptor Cryptor
        {
            get
            {
                if (_Cryptor == null) _Cryptor = new AesCryptoProvider();
                return _Cryptor;
            }
            
        }

    }
}