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
    public class CardRepository
    {
        public IDbConnection connection { get { return DataIO.connection; } }
        public Operation<CardMaster> GetCardMaster(int Id)
        {
            Operation<CardMaster> op_item = new Operation<CardMaster>();
            CardMaster card = null;
            try
            {
                card = connection.Get<CardMaster>(Id);
                op_item.Value = card;
                op_item.Successful = (card != null);
            }
            catch (Exception ex)
            {
                op_item.Fail = ex.Message;
            }

            return op_item;
        }
        public Operation<CardMaster> PostCardMaster(CardMaster cardMaster)
        {
            if (cardMaster == null)
            {
                return new Operation<CardMaster>() { Fail = "item is null !" };
            }

            Operation<CardMaster> op_CardMaster = new Operation<CardMaster>();

            IDbTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                if (cardMaster.Id == 0)
                {
                    connection.Insert(cardMaster, transaction);
                }
                else
                {
                    connection.Update(cardMaster, transaction);
                }
                transaction.Commit();
                connection.Close();
                op_CardMaster.Value = cardMaster;
                op_CardMaster.Successful = true;
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch
                { }
                op_CardMaster.Fail = ex.Message;
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
            return op_CardMaster;
        }
        public Operation<List<CardMasterView>> GetAllCards()
        {
            Operation<List<CardMasterView>> r = new Operation<List<CardMasterView>>();
            try
            {
                r.Value = DataIO.GetAllOff<CardMasterView>();
                r.Successful = true;
            }
            catch (Exception ex)
            {
                r.Fail = ex.Message;
            }
            return r;
        }

        public Operation<List<CardMasterView>> GetAllCardsExt(DateTime date,int userId)
        {
            Operation<List<CardMasterView>> operation = new Operation<List<CardMasterView>>();
            try
            {
                List<CardMasterView> mainList = connection.Query<CardMasterView>("SP_GetCardsViewDebts @userId, @Date", new {userId = userId, Date = date }).ToList();
                User user = new UserRepository().GetUserById(userId).Value;
                if (!user.BaseUser.IsAdmin)
                {
                    List<UserDataPermissionView> userDataPermissions = (new UserRepository().GetUserDataPermissionView(userId)).Value;
                    userDataPermissions = userDataPermissions.Where(x => x.PermissionType == 1).ToList();
                    foreach (var item in mainList.Reverse<CardMasterView>())
                    {
                        if (item.CardType == 4)
                        {
                            bool isPermitted = userDataPermissions.Any(x => x.PermissionId == item.Id);
                            if (!isPermitted) mainList.Remove(item);
                        }
                        //    if (item.DestCardTypeId == 4)
                        //    {
                        //        bool isPermitted = userDataPermissions.Any(x => x.PermissionId == item.DestCardId);
                        //        if (!isPermitted) mainList.Remove(item);
                        //    }
                        //}
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

        public Operation<List<CardTransactionView>> GetCardTransactionViewByCardId(int CardId,DateTime begDate,DateTime endDate)
        {
            Operation<List<CardTransactionView>> operation = new Operation<List<CardTransactionView>>();
            try
            {
                List<CardTransactionView> mainList = connection.Query<CardTransactionView>("SELECT * FROM CardTransactionView WHERE (CardId = @CardId) AND (CreatedDate BETWEEN @begDate AND @endDate) order by CreatedDate", new { CardId = CardId, begDate = begDate , endDate = endDate }).ToList();
                //User user = new UserRepository().GetUserById(userId).Value;
                //if (!user.BaseUser.IsAdmin)
                //{
                //    List<UserDataPermissionView> userDataPermissions = (new UserRepository().GetUserDataPermissionView(userId)).Value;
                //    userDataPermissions = userDataPermissions.Where(x => x.PermissionType == 1).ToList();
                //    foreach (var item in mainList.Reverse<CardMasterView>())
                //    {
                //        if (item.CardType == 4)
                //        {
                //            bool isPermitted = userDataPermissions.Any(x => x.PermissionId == item.Id);
                //            if (!isPermitted) mainList.Remove(item);
                //        }
                //    }


                //}
                operation.Value = mainList;
                operation.Successful = true;

            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }

        public Operation<List<CardTotalByIntervalView>> GetGetCardTotalsByInterval(int userId, DateTime begDate, DateTime endDate)
        {
            Operation<List<CardTotalByIntervalView>> operation = new Operation<List<CardTotalByIntervalView>>();
            try
            {
                List<CardTotalByIntervalView> mainList = connection.Query<CardTotalByIntervalView>("SP_GetCardTotalsByInterval @userId,@begDate,@endDate", new {userId = userId, begDate = begDate, endDate = endDate }).ToList();
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