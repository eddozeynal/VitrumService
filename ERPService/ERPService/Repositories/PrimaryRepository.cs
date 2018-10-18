using BusinessModels;
using Dapper;
using Dapper.Contrib.Extensions;
using DBModels;
using ERPService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace Repositories
{
    public class PrimaryRepository
    {
        public IDbConnection connection { get { return DataIO.connection; } }
        public GridViewInfo GetGridViewInfo(int GridViewId, int UserId)
        {
            GridViewInfo gvi = new GridViewInfo();
            gvi.GridViewMaster_ = DataIO.GetT<GridViewMaster>(GridViewId); //DataIO.GetAllOff<GridViewMaster>
            gvi.GridViewColumns_ = connection.Query<GridViewColumn>(" EXEC SP_GetGridViewColumns " + GridViewId.ToString() + ","+ UserId.ToString()).ToList();
            return gvi;
        }

        public Operation<List<Parameter>> GetParameters()
        {
            Operation<List<Parameter>> r = new Operation<List<Parameter>>();
            try
            {
                r.Value = DataIO.GetAllOff<Parameter>();
                r.Successful = true;
            }
            catch (Exception ex)
            {
                r.Fail = ex.Message;
            }
            return r;
        }

        public Operation<List<EnumMaster>> GetEnums(string EnumNumber)
        {
            Operation<List<EnumMaster>> r = new Operation<List<EnumMaster>>();
            try
            {
                r.Value = DataIO.GetAllOff<EnumMaster>(" TypeId = " + EnumNumber);
                r.Successful = true;
            }
            catch (Exception ex)
            {
                r.Fail = ex.Message;
            }
            return r;
        }

        public Operation<List<DocumentMaster>> GetDocumentMasters()
        {
            Operation<List<DocumentMaster>> r = new Operation<List<DocumentMaster>>();
            try
            {
                r.Value = DataIO.GetAllOff<DocumentMaster>();
                r.Successful = true;
            }
            catch (Exception ex)
            {
                r.Fail = ex.Message;
            }
            return r;
        }

        public Operation<List<WarehouseMaster>> GetWarehouses()
        {
            Operation<List<WarehouseMaster>> r = new Operation<List<WarehouseMaster>>();
            try
            {
                r.Value = DataIO.GetAllOff<WarehouseMaster>();
                r.Successful = true;
            }
            catch (Exception ex)
            {
                r.Fail = ex.Message;
            }
            return r;
        }

    }
}