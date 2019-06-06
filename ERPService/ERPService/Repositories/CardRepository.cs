
using Dapper;
using Dapper.Contrib.Extensions;
using ERPService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ERPService
{
    public class CardRepository
    {
        public Operation<CardMaster> GetCardMaster(int Id)
        {
            Operation<CardMaster> op_item = new Operation<CardMaster>();
            CardMaster card = null;
            using (IDbConnection connection = new SqlConnection(Properties.Settings.Default.DefaultConnectionString))
            {
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
            }
            return op_item;
        }
        public Operation<CardMaster> PostCardMaster(CardMaster cardMaster)
        {
            if (cardMaster == null)
            {
                return new Operation<CardMaster>() { Fail = "Card is null !" };
            }

            Operation<CardMaster> op_CardMaster = new Operation<CardMaster>();
            using (IDbConnection connection = new SqlConnection(ERPService.Properties.Settings.Default.DefaultConnectionString))
            {
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
            }
            new Repositories.LogRepository().Log(20, cardMaster.Id, cardMaster);
            return op_CardMaster;
        }
        public Operation<List<CardMasterView>> GetAllCards()
        {
            Operation<List<CardMasterView>> r = new Operation<List<CardMasterView>>();
            try
            {
                using (IDbConnection connection = new SqlConnection(Properties.Settings.Default.DefaultConnectionString))
                {
                    r.Value = connection.GetAll<CardMasterView>().ToList();
                    r.Successful = true;
                }
            }
            catch (Exception ex)
            {
                r.Fail = ex.Message;
            }
            return r;
        }
        public Operation<List<CardTransactionView>> GetCardTransactionViewByCardId(int CardId,DateTime begDate,DateTime endDate)
        {
            Operation<List<CardTransactionView>> operation = new Operation<List<CardTransactionView>>();
            try
            {
                using (IDbConnection connection = new SqlConnection(ERPService.Properties.Settings.Default.DefaultConnectionString))
                {
                    List<CardTransactionView> mainList = connection.Query<CardTransactionView>("SELECT * FROM CardTransactionView WHERE (CardId = @CardId) AND (CreatedDate BETWEEN @begDate AND @endDate) order by CreatedDate", new { CardId = CardId, begDate = begDate, endDate = endDate }).ToList();

                    operation.Value = mainList;
                    operation.Successful = true;
                }
                   

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
                using (IDbConnection connection = new SqlConnection(ERPService.Properties.Settings.Default.DefaultConnectionString))
                {
                    List<CardTotalByIntervalView> mainList = connection.Query<CardTotalByIntervalView>("SP_GetCardTotalsByInterval @userId,@begDate,@endDate", new { userId = userId, begDate = begDate, endDate = endDate }).ToList();
                    operation.Value = mainList;
                    operation.Successful = true;
                }
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }

        public Operation<List<CardType>> GetCardTypes()
        {
            Operation<List<CardType>> operation = new Operation<List<CardType>>();
            try
            {
                using (IDbConnection connection = new SqlConnection(ERPService.Properties.Settings.Default.DefaultConnectionString))
                {
                    List<CardType> mainList = connection.GetAll<CardType>().ToList();
                    operation.Value = mainList;
                    operation.Successful = true;
                }
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
    }
}