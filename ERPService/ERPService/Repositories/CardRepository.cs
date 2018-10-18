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
        public Operation<List<VW_CardMaster>> GetAllCards()
        {
            Operation<List<VW_CardMaster>> r = new Operation<List<VW_CardMaster>>();
            try
            {
                r.Value = DataIO.GetAllOff<VW_CardMaster>();
                r.Successful = true;
            }
            catch (Exception ex)
            {
                r.Fail = ex.Message;
            }
            return r;
        }

        public Operation<List<VW_CardMaster>> GetAllCardsExt(DateTime date,int userId)
        {
            Operation<List<VW_CardMaster>> operation = new Operation<List<VW_CardMaster>>();
            try
            {
                List<VW_CardMaster> mainList = connection.Query<VW_CardMaster>("SP_GetCardsViewDebts @Date", new { Date = date }).ToList();
                User user = new UserRepository().GetUserById(userId).Value;
                if (!user.BaseUser.IsAdmin)
                {
                    List<UserDataPermissionView> userDataPermissions = (new UserRepository().GetUserDataPermissionView(userId)).Value;
                    userDataPermissions = userDataPermissions.Where(x => x.PermissionType == 1).ToList();
                    foreach (var item in mainList.Reverse<VW_CardMaster>())
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
    }
}