using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;
//using Z.Dapper.Plus;
using ERPService.Models;

namespace ERPService
{
    public class DapperRepo
    {
        System.Data.SqlClient.SqlConnection connection = null;
        IDbConnection dbConnection
        {
            get
            {
                if (connection == null) connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.DefaultConnectionString);
                return connection;
            }
        }

        public Operation<T> Post<T>(T data) where T : class, new()
        {
            Operation<T> operation = new Operation<T>();

            bool toUpdate = false;
            try
            {
                var prs = data.GetType().GetProperties();
                foreach (var pr in prs)
                {
                    var hasIsIdentity = Attribute.IsDefined(pr, typeof(KeyAttribute));
                    if (hasIsIdentity)
                    {
                        var val = pr.GetValue(data);
                        //int Id = 0;
                        if (int.TryParse(val.ToString(), out int Id))
                        {
                            if (Id !=0)
                                toUpdate = true;
                        }
                    }
                    
                }

                if (toUpdate)
                {
                    dbConnection.Update(data);
                }
                else
                {
                    dbConnection.Insert(data);
                }
                operation.Value = data;
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }

        public Operation<List<T>> GetAllT<T>() where T : class, new()
        {
            Operation<List<T>> operation = new Operation<List<T>>();
            try
            {
                operation.Value = dbConnection.GetAll<T>().ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
        public Operation<T> GetById<T>(string Id) where T : class, new()
        {
            Operation<T> operation = new Operation<T>();
            try
            {
                operation.Value = dbConnection.Get<T>(Convert.ToInt32(Id));
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
    }
}