using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using Dapper.Contrib.Extensions;
using DBModels;
using ERPService;
using BusinessModels;

namespace Repositories
{
    public class CashRepository
    {
        public IDbConnection connection { get {return DataIO.connection; } }
        public Operation<CashTransaction> GetCashTransaction(int Id)
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
        Operation<CashTransaction> InsertCashTransaction(CashTransaction cashTransaction)
        {
            Operation<CashTransaction> op_item = new Operation<CashTransaction>();
            //if (item.ItemTypeId == 0)
            //{
            //    op_item.Fail = " Məhsulun Növü Daxil Edilməyib ";
            //    return op_item;
            //}
            //if (item.ItemCode.Length == 0)
            //{
            //    op_item.Fail = " Məhsulun Kodu Daxil Edilməyib ";
            //    return op_item;
            //}

            //if (item.ItemName.Length == 0)
            //{
            //    op_item.Fail = " Məhsulun Adı Daxil Edilməyib ";
            //    return op_item;
            //}

            IDbTransaction dbTransaction = null;
            try
            {
                connection.Open();
                dbTransaction = connection.BeginTransaction();
                string _ficheno = connection.ExecuteScalar<string>("EXECUTE SP_GetNewDocumentNumber 5", null, dbTransaction);
                cashTransaction.Ficheno = _ficheno;
                cashTransaction.Status = 1;
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

        Operation<CashTransaction> UpdateCashTransaction(CashTransaction cashTransaction)
        {
            Operation<CashTransaction> op_item = new Operation<CashTransaction>();
            //if (item.ItemTypeId == 0)
            //{
            //    op_item.Fail = " Məhsulun Növü Daxil Edilməyib ";
            //    return op_item;
            //}
            //if (item.ItemCode.Length == 0)
            //{
            //    op_item.Fail = " Məhsulun Kodu Daxil Edilməyib ";
            //    return op_item;
            //}

            //if (item.ItemName.Length == 0)
            //{
            //    op_item.Fail = " Məhsulun Adı Daxil Edilməyib ";
            //    return op_item;
            //}

            try
            {
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

        public Operation<List<CashTransactionView>> GetCashTransactionsView(DateTime dateBegin, DateTime dateEnd, int UserId)
        {
            Operation<List<CashTransactionView>> operation = new Operation<List<CashTransactionView>>();
            try
            {
                List<CashTransactionView> mainList = connection.Query<CashTransactionView>("SP_GetCashTransactionsView @DateBegin,@DateEnd", new { DateBegin = dateBegin, DateEnd = dateEnd }).ToList();
                User user = new UserRepository().GetUserById(UserId).Value;
                if (!user.BaseUser.IsAdmin)
                {
                    List<UserDataPermissionView> userDataPermissions = (new UserRepository().GetUserDataPermissionView(UserId)).Value;
                    userDataPermissions = userDataPermissions.Where(x => x.PermissionType == 1).ToList();
                    foreach (var item in mainList.Reverse<CashTransactionView>())
                    {
                        if (item.SourceCardTypeId == 4)
                        {
                            bool isPermitted = userDataPermissions.Any(x => x.PermissionId == item.SourceCardId);
                            if (!isPermitted) mainList.Remove(item);
                        }
                        if (item.DestCardTypeId == 4)
                        {
                            bool isPermitted = userDataPermissions.Any(x => x.PermissionId == item.DestCardId);
                            if (!isPermitted) mainList.Remove(item);
                        }
                    }
                }
               
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