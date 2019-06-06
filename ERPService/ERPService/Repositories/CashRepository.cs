using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using Dapper.Contrib.Extensions;
using ERPService;
using System.Data.SqlClient;

namespace ERPService
{
    public class CashRepository
    {
        //public IDbConnection connection { get {return DataIO.connection; } }
        public Operation<CashTransaction> GetCashTransaction(int Id)
        {
            using (IDbConnection connection = new SqlConnection(ERPService.Properties.Settings.Default.DefaultConnectionString))
            {
                Operation<CashTransaction> op_item = new Operation<CashTransaction>();
                CashTransaction item = null;
                try
                {
                    item = new CashTransaction();
                    item = connection.Get<CashTransaction>(Id);
                    op_item.Value = item;
                    op_item.Successful = (item != null);
                }
                catch (Exception ex)
                {
                    op_item.Fail = ex.Message;
                }
                return op_item;
            }
        }
        public Operation<CashTransaction> PostCashTransaction(CashTransaction cashTransaction)
        {
            if (cashTransaction == null)
            {
                return new Operation<CashTransaction>() { Fail = "cashTransaction is null !" };
            }
            
            Operation<CashTransaction> op_item = new Operation<CashTransaction>();

            if (cashTransaction.Id == 0)
            {
                op_item = InsertCashTransaction(cashTransaction);
            }
            else
            {
                op_item = UpdateCashTransaction(cashTransaction);
            }

            return op_item;
        }
        public Operation<CashTransaction> AcceptCashTransaction(int CashTransactionId, int UserId)
        {
            if (CashTransactionId == 0)
            {
                return new Operation<CashTransaction>() { Fail = "cashTransaction is null !" };
            }

            Operation<CashTransaction> op_item = GetCashTransaction(CashTransactionId);

            if (!op_item.Successful)
            {
                return new Operation<CashTransaction>() { Fail = "unable to retrieve cashTransaction" };
            }
            op_item.Value.Status = 2;
            return PostCashTransaction(op_item.Value);
        }
        Operation<CashTransaction> InsertCashTransaction(CashTransaction cashTransaction)
        {
            using (IDbConnection connection = new SqlConnection(ERPService.Properties.Settings.Default.DefaultConnectionString))
            {
                Operation<CashTransaction> op_item = new Operation<CashTransaction>();
                IDbTransaction dbTransaction = null;
                try
                {
                    connection.Open();
                    dbTransaction = connection.BeginTransaction();
                    string _ficheno = connection.ExecuteScalar<string>("EXECUTE SP_GetNewDocumentNumber 5", null, dbTransaction);
                    cashTransaction.Ficheno = _ficheno;
                    cashTransaction.Status = 1;
                    cashTransaction.TotalMEx = cashTransaction.Total * new ExchangeRepository().ExchangeRateByDate(cashTransaction.ExchangeId, cashTransaction.CreatedDate);
                    connection.Insert(cashTransaction, dbTransaction);

                    dbTransaction.Commit();
                    connection.Close();
                    op_item.Value = cashTransaction;
                    op_item.Successful = true;
                }
                catch (Exception ex)
                {
                    try
                    {
                        dbTransaction.Rollback();
                    }
                    catch
                    { }
                    op_item.Fail = ex.Message;
                }
                finally
                {

                    try
                    {
                        connection.Close();
                    }
                    catch
                    { }
                }

                return op_item;
            }
            
        }

        Operation<CashTransaction> UpdateCashTransaction(CashTransaction cashTransaction)
        {
            using (IDbConnection connection = new SqlConnection(ERPService.Properties.Settings.Default.DefaultConnectionString))
            {
                Operation<CashTransaction> op_item = new Operation<CashTransaction>();


                try
                {
                    cashTransaction.TotalMEx = cashTransaction.Total * new ExchangeRepository().ExchangeRateByDate(cashTransaction.ExchangeId, cashTransaction.CreatedDate);
                    connection.Update(cashTransaction);
                    op_item.Value = cashTransaction;
                    op_item.Successful = true;
                }
                catch (Exception ex)
                {
                    op_item.Fail = ex.Message;
                }
                finally
                {


                }
                return op_item;
            }
           
        }

        public Operation<List<CashTransactionView>> GetCashTransactionsView(DateTime dateBegin, DateTime dateEnd, int UserId)
        {
            

            using (IDbConnection connection = new SqlConnection(ERPService.Properties.Settings.Default.DefaultConnectionString))
            {
                Operation<List<CashTransactionView>> operation = new Operation<List<CashTransactionView>>();
                try
                {
                    List<CashTransactionView> mainList = connection.Query<CashTransactionView>("SP_GetCashTransactionsView", new { userId = UserId, begDate = dateBegin, endDate = dateEnd }, commandType: CommandType.StoredProcedure).ToList();

                    operation.Value = mainList;
                    operation.Successful = true;

                }
                catch (Exception ex)
                {
                    operation.Fail = ex.Message;
                }
                return operation;
            }
        }

        public Operation<List<CashTransactionView>> GetCashTransactionsViewByFiche(int FicheId)
        {
           
            using (IDbConnection connection = new SqlConnection(ERPService.Properties.Settings.Default.DefaultConnectionString))
            {
                Operation<List<CashTransactionView>> operation = new Operation<List<CashTransactionView>>();
                if (FicheId == 0)
                {
                    operation.Successful = true;
                    operation.Value = new List<CashTransactionView>();
                    return operation;
                }
                try
                {
                    string query = @"select Em.Name_ CashCategoryName , Ct.* from CashTransaction Ct 
left join EnumMaster Em on Ct.CashCategoryId = Em.Key_ and Em.TypeId = 10 
where Ct.ConnectedFicheId = " + FicheId.ToString();
                    List<CashTransactionView> mainList = connection.Query<CashTransactionView>(query).ToList();
                    operation.Value = mainList;
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
}