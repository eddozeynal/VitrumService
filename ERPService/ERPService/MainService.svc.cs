using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Dapper;
using Dapper.Contrib.Extensions;
using ERPService;
using ERPService.Models;
using ERPService.ViewModels;

namespace ERPService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,InstanceContextMode = InstanceContextMode.Single)]
    public class MainService : IMainService
    {
        #region Global
        SqlConnection _connection = null;
        IDbConnection connection
        {
            get
            {
                if (_connection == null) _connection = new SqlConnection(Properties.Settings.Default.DefaultConnectionString);
                return _connection;
            }
        }

        public static List<T> GetAll<T>(IDbConnection connection, string condition)
        {
            string TableName = typeof(T).Name;
            string query = "SELECT * FROM " + TableName + " " + condition;
            return connection.Query<T>(query).ToList();
        }

        public Operation<string> DeleteObjectById(string TableName,  int Id)
        {
            Operation<string> operation = new Operation<string>();
            try
            {
                string query = " DELETE FROM "  + TableName + " WHERE Id = " + Id.ToString();
                operation.Value = connection.Execute(query).ToString();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
        #endregion

        #region Users/Login/Permission
        public Operation<UserView> LoginUser(LoginData loginData)//string Login, string PassHash)
        {
            Operation<UserView> operation = new Operation<UserView>();
            try
            {
                string openPassword = new RijndaelCrypt("D8903F7F075E4877A6C5F62EC68A2018").Decrypt(loginData.PassHash);
                User baseUser = connection.Query<User>(" select * From [User] where Login = @Login AND PassHash = @PassHash ", loginData).FirstOrDefault();
                if (baseUser == null)
                {
                    operation.Fail = "istifadəçi adı və ya şifrə yanlışdır";
                    return operation;
                }
                if (baseUser.IsActive == false)
                {
                    operation.Fail = "istifadəçi passivdir";
                    return operation;
                }
                LoginSession loginSession = new LoginSession();
                loginSession.CreatedDate = DateTime.Now;
                loginSession.UserId = baseUser.Id;
                loginSession.Guid = Guid.NewGuid().ToString();
                connection.Insert(loginSession);
                
                UserView user = baseUser.GetEligibleOjbect<UserView>();
                user.PermissionDetails = connection.Query<PermissionDetail>("SELECT * FROM PermissionDetail WHERE UserId = " + user.Id.ToString()).ToList();
                user.CardPermissions = connection.Query<CardPermission>("SELECT * FROM CardPermission WHERE UserId = " + user.Id.ToString()).ToList(); 
                user.LoginSession = loginSession;
                operation.Value = user;
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }

            return operation;
        }
        
        public Operation<List<PermissionMasterView>> GetUserPermissions(string UserId)
        {
            Operation<List<PermissionMasterView>> operation = new Operation<List<PermissionMasterView>>();
            try
            {
                operation.Value = connection.Query<PermissionMasterView>("SP_GetUserPermissions",new {UserId}, null,true,0,CommandType.StoredProcedure).ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
        public Operation<List<User>> GetUsers()
        {
            return new DapperRepo().GetAllT<User>();
        }
        public Operation<User> GetUserById(string Id)
        {
            return new DapperRepo().GetById<User>(Id);
        }
        public Operation<User> PostUser(User user)
        {
            return new DapperRepo().Post(user);
        }
        public Operation<string> PostUserPermissions(int UserId,List<PermissionDetail> Details)
        {
            Operation<string> operation = new Operation<string>();
            try
            {
                List<PermissionDetail> oldDetails = connection.Query<PermissionDetail>("SELECT * FROM PermissionDetail WHERE UserId = " + UserId.ToString()).ToList();
                CollectionChangesComparer<PermissionDetail> comparer = new CollectionChangesComparer<PermissionDetail>();
                comparer.KeyFieldName = "Id";
                comparer.SetInitialList(oldDetails);
                comparer.SetFinalList(Details);
                connection.Delete(comparer.GetDeletes());
                connection.Insert(comparer.GetInserts());
                connection.Update(comparer.GetUpdates());
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
           
        }
        public Operation<List<CardPermission>> GetCardPermissions(string UserId)
        {
            Operation<List<CardPermission>> operation = new Operation<List<CardPermission>>();
            try
            {
                operation.Value = connection.Query<CardPermission>("SP_GetCardPermissions", new { UserId }, null, true, 0, CommandType.StoredProcedure).ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
        public Operation<CardPermission> PostCardPermission(CardPermission cardPermission)
        {
            return new DapperRepo().Post(cardPermission);
        }

        #endregion

        #region Cards
        public Operation<Card> PostCard(Card card)
        {
            return new DapperRepo().Post(card);
        }
        public Operation<List<CardView>> GetCards()
        {
            return new DapperRepo().GetAllT<CardView>();
        }
        public Operation<List<CardType>> GetCardTypes()
        {
            Operation<List<CardType>> operation = new Operation<List<CardType>>();
            try
            {
                operation.Value = connection.GetAll<CardType>().ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
        public Operation<List<Currency>> GetCurrencies()
        {
            Operation<List<Currency>> operation = new Operation<List<Currency>>();
            try
            {
                operation.Value = connection.GetAll<Currency>().ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
        public Operation<Card> GetCardById(string Id)
        {
            return new DapperRepo().GetById<Card>(Id);
        }
        #endregion

        #region Items
        public Operation<List<ItemView>> GetItems()
        {
            return new DapperRepo().GetAllT<ItemView>();
        }
        public Operation<List<ItemType>> GetItemTypes()
        {
            return new DapperRepo().GetAllT<ItemType>();
        }
        public Operation<List<LineCalcType>> GetLineCalcTypes()
        {
            return new DapperRepo().GetAllT<LineCalcType>();
        }
        public Operation<Item> GetItemById(string Id)
        {
            return new DapperRepo().GetById<Item>(Id);
        }
        public Operation<Item> PostItem(Item item)
        {
            return new DapperRepo().Post(item);
        }
        public Operation<List<Warehouse>> GetWarehouses()
        {
            return new DapperRepo().GetAllT<Warehouse>();
        }
        public Operation<List<ItemPrice>> GetItemPrices(string ItemId)
        {
            Operation<List<ItemPrice>> operation = new Operation<List<ItemPrice>>();
            try
            {
                operation.Value = connection.Query<ItemPrice>("SP_GetItemPrices", new { ItemId }, null, true, 0, CommandType.StoredProcedure).ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
        public Operation<List<ItemPrice>> GetItemPricesByCard(string CardId)
        {
            Operation<List<ItemPrice>> operation = new Operation<List<ItemPrice>>();
            try
            {
                operation.Value = connection.Query<ItemPrice>("SP_GetItemPricesByCard", new { CardId }, null, true, 0, CommandType.StoredProcedure).ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
        public Operation<string> PostItemPrices(int ItemId, List<ItemPrice> Details)
        {
            Operation<string> operation = new Operation<string>();
            try
            {
                List<ItemPrice> oldDetails = GetItemPrices(ItemId.ToString()).Value;
                CollectionChangesComparer<ItemPrice> comparer = new CollectionChangesComparer<ItemPrice>();
                comparer.KeyFieldName = "Id";
                comparer.SetInitialList(oldDetails);
                comparer.SetFinalList(Details);
                connection.Delete(comparer.GetDeletes());
                connection.Insert(comparer.GetInserts());
                connection.Update(comparer.GetUpdates());
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;

        }
        #endregion

        #region Cash
        public Operation<CashTransaction> PostCashTransaction(CashTransaction cashTransaction)
        {
            if (cashTransaction.Id == 0)
            {
                cashTransaction.Ficheno = GetNewNumber(5);
            }
            return new DapperRepo().Post(cashTransaction);
        }
        public Operation<CurrencyByDate> PostCurrencyByDate(CurrencyByDate currencyByDate)
        {
            return new DapperRepo().Post(currencyByDate);
        }
        public Operation<ExchangeTransaction> PostExchangeTransaction(ExchangeTransaction exchangeTransaction)
        {
            return new DapperRepo().Post(exchangeTransaction);
        }
        public Operation<List<CashType>> GetCashTypes()
        {
            return new DapperRepo().GetAllT<CashType>();
        }
        public Operation<CashTransaction> GetCashTransactionById(string Id)
        {
            return new DapperRepo().GetById<CashTransaction>(Id);
        }
        public Operation<ExchangeTransaction> GetExchangeTransactionById(string Id)
        {
            return new DapperRepo().GetById<ExchangeTransaction>(Id);
        }
        public Operation<List<CashTransactionView>> GetCashTransactionsView(string dateBegin, string dateEnd, string userId)
        {
            Operation<List<CashTransactionView>> operation = new Operation<List<CashTransactionView>>();
            try
            {
                operation.Value = connection.Query<CashTransactionView>("SP_GetCashTransactionsView", new { begDate = dateBegin.GetDateFromFormattedString(), endDate = dateEnd.GetDateFromFormattedString(), userId = Convert.ToInt32(userId) }, null, true, 0, CommandType.StoredProcedure).ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
        public Operation<List<ExchangeTransactionView>> GetExchangeTransactionsView(string dateBegin, string dateEnd, string userId)
        {
            Operation<List<ExchangeTransactionView>> operation = new Operation<List<ExchangeTransactionView>>();
            try
            {
                operation.Value = connection.Query<ExchangeTransactionView>("SP_GetExchangeTransactionView", new { begDate = dateBegin.GetDateFromFormattedString(), endDate = dateEnd.GetDateFromFormattedString(), userId = Convert.ToInt32(userId) }, null, true, 0, CommandType.StoredProcedure).ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
        #endregion

        #region Fiches

        public Operation<List<DocumentStatusType>> GetDocumentStatusTypes()
        {
            return new DapperRepo().GetAllT<DocumentStatusType>();
        }

        public Operation<FicheMaster> PostFicheMaster(FicheMaster ficheMaster)
        {
            return new DapperRepo().Post(ficheMaster);
        }


        string GetNewNumber(byte DocumentTypeNumber)
        {
            return connection.ExecuteScalar<string>("SP_GetNewDocumentNumber", new { DocumentTypeNumber }, null, 0, CommandType.StoredProcedure).ToString() ;
        }

        #endregion

        //public Operation<List<Parameter>> GetParameters()
        //{
        //    return PrimaryRepository.GetParameters();
        //}

        //public Operation<Item> GetItem(string Id)
        //{
        //    string GUID = WebOperationContext.Current.IncomingRequest.Headers["GUID"];
        //    return ItemRepository.GetItem(Convert.ToInt32(Id));
        //}

        //public Operation<Item> PostItem(Item item)
        //{
        //    return ItemRepository.PostItem(item);
        //}

        //public Operation<TestClassView> PostTest2(TestClassView data)
        //{
        //    return new ERPService.Operation<ERPService.TestClassView>() { Value = data };
        //}
        //public Operation<CardMaster> PostCardMaster(CardMaster cardMaster)
        //{
        //    return CardRepository.PostCardMaster(cardMaster);
        //}

        //public Operation<List<CardMasterView>> GetAllCards()
        //{
        //    return CardRepository.GetAllCards();
        //}
        //public Operation<List<PriceCalcType>> GetPriceCalcTypes()
        //{
        //    return ItemRepository.GetPriceCalcTypes();
        //}
        //public Operation<List<ExchangeMaster>> GetAllExchanges()
        //{
        //    return ExchangeRepository.GetAllExchanges();
        //}

        //public Operation<List<ExchangeByDate>> GetExchangesFromBankByDate(string Date_)
        //{
        //    return ExchangeRepository.GetExchangesFromBankByDate(Convert.ToDateTime(Date_));
        //}

        //public Operation<List<CardType>> GetCardTypes()
        //{
        //    return CardRepository.GetCardTypes();
        //}

        //public Operation<List<ItemPriceForCard>> GetItemPriceForCards(string ItemId)
        //{
        //    return ItemRepository.GetItemPriceForCards(ItemId);
        //}

        //public Operation<bool> PostItemPrices(List<ItemPriceOperation> ItemPrices)
        //{
        //    return ItemRepository.PostItemPrices(ItemPrices);
        //}



        public Operation<List<DocumentMaster>> GetDocumentMasters()
        {
            return new DapperRepo().GetAllT<DocumentMaster>();
        }

        //public Operation<List<WarehouseMaster>> GetWarehouses()
        //{
        //    return PrimaryRepository.GetWarehouses();
        //}

        //public Operation<List<ItemPrice>> GetItemPricesList()
        //{
        //    return ItemRepository.GetItemPricesList();
        //}

        //public Operation<FicheMasterView> PostFiche(FicheMasterView fiche)
        //{
        //    return FicheRepository.PostFiche(fiche);
        //}

        //public Operation<List<FicheMasterView>> GetFiches(string DocType, string dateBegin, string dateEnd)
        //{
        //    return FicheRepository.GetFiches(Convert.ToByte(DocType), dateBegin.GetDateFromFormattedString(), dateEnd.GetDateFromFormattedString());
        //}

        //public Operation<List<CardTransactionView>> GetCardTransactionViewByCardId(string cardId, string dateBegin, string dateEnd)
        //{
        //    return CardRepository.GetCardTransactionViewByCardId(Convert.ToInt32(cardId), dateBegin.GetDateFromFormattedString(), dateEnd.GetDateFromFormattedString());
        //}
        //public Operation<List<CardTotalByIntervalView>> GetCardTotalsByInterval(string userId, string dateBegin, string dateEnd)
        //{
        //    return CardRepository.GetGetCardTotalsByInterval(Convert.ToInt32(userId), dateBegin.GetDateFromFormattedString(), dateEnd.GetDateFromFormattedString());
        //}
        //public Operation<FicheMasterView> GetFicheById(string Id)
        //{
        //    return FicheRepository.GetFicheById(Convert.ToInt32(Id));
        //}

        //public Operation<List<PermissionMaster>> GetPermissionMasters()
        //{
        //    return UserRepository.GetPermissionMasters();
        //}

        //public Operation<List<PermissionDetail>> GetPermissionDetailsByUserId(string UserId)
        //{
        //    return UserRepository.GetPermissionDetailsByUserId(Convert.ToInt32(UserId));
        //}

        //public Operation<CashTransaction> GetCashTransactionById(string Id)
        //{
        //    return CashRepository.GetCashTransaction(Convert.ToInt32(Id));
        //}

        //public Operation<CashTransaction> PostCashTransaction(CashTransaction CashTransaction)
        //{
        //    return CashRepository.PostCashTransaction(CashTransaction);
        //}

        //public Operation<List<UserDataPermissionView>> GetUserDataPermissionView(string UserId)
        //{
        //    return UserRepository.GetUserDataPermissionView(Convert.ToInt32(UserId));
        //}



        //Operation<CashTransaction> IMainService.GetCashTransactionById(string Id)
        //{
        //    throw new NotImplementedException();
        //}


        //public Operation<DataPermission> PostDataPermission(DataPermission dataPermission)
        //{
        //    return UserRepository.PostDataPermission(dataPermission);
        //}

        //public Operation<int> DeleteDataPermission(int dataPermissionId)
        //{
        //    return UserRepository.DeleteDataPermission(dataPermissionId);
        //}

        //public Operation<FicheMasterView> ChangeFicheStatus(string Id, string StatusId)
        //{
        //    return FicheRepository.ChangeFicheStatus(Convert.ToInt32(Id),Convert.ToByte(StatusId));
        //}

        //public Operation<FicheMasterView> PostFiche2(FicheMasterView fiche)
        //{
        //    return new Operation<FicheMasterView>() { Value = fiche };
        //}

        //public Operation<List<ItemViewAcc>> GetItemTotalsByInterval(string userId, string dateBegin, string dateEnd)
        //{
        //    return ItemRepository.GetItemTotalsByInterval(Convert.ToInt32(userId), dateBegin.GetDateFromFormattedString(), dateEnd.GetDateFromFormattedString());
        //}

        //public Operation<CashTransaction> AcceptCashTransaction(int CashTransactionId, int UserId)
        //{
        //    return CashRepository.AcceptCashTransaction(CashTransactionId, UserId);
        //}

        //public Operation<List<CashTransactionView>> GetCashTransactionsViewByFiche(string FicheId)
        //{
        //    return CashRepository.GetCashTransactionsViewByFiche(Convert.ToInt32(FicheId));
        //}
    }

    public class LoginData
    {
        public string Login { get; set; }
        public string PassHash { get; set; }
    }
}
