using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

namespace ERPService.Repositories
{
    public class LogRepository
    {
        public void Log(byte logDataType,int logDataId,object data)
        {
            using (IDbConnection connection = new SqlConnection(ERPService.Properties.Settings.Default.DefaultConnectionString))
            {
                try
                {
                    IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;

                    string Guid = woc.Headers["Guid"];

                    int userIdOnGuid = connection.Query<int>("SELECT UserId FROM LoginSession  WHERE Guid = '"+ Guid + "'").First();

                    DataLog dataLog = new DataLog();
                    dataLog.DoneBy = userIdOnGuid;
                    dataLog.LogDataId = logDataId;
                    dataLog.LogDataType = logDataType;
                    dataLog.LogDate = DateTime.Now;
                    dataLog.LogData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    connection.Insert(dataLog);
                }
                catch {                

                }
            }
        }
    }
}