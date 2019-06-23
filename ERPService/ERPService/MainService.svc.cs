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
        //SqlConnection _connection = null;
        IDbConnection connection
        {
            get
            {
                //if (_connection == null) _connection = new SqlConnection(Properties.Settings.Default.DefaultConnectionString);
                //return _connection;
                return new SqlConnection(Properties.Settings.Default.DefaultConnectionString);
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
        string GUIDOfCurrentIncomingRequest { get { return WebOperationContext.Current.IncomingRequest.Headers["GUID"]; } }
        int UserIdBySession
        {
            get
            {
                using (IDbConnection dbConnection = connection)
                {
                    LoginSession loginSession = dbConnection.Query<LoginSession>("select * from LoginSession WHERE Guid = @Guid",new {Guid = GUIDOfCurrentIncomingRequest }).FirstOrDefault();
                    return loginSession.UserId;
                }
            }
        }
        public Operation<List<WorkStateView>> GetWorkStates()
        {
            return new DapperRepo().GetAllT<WorkStateView>();
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
        public Operation<List<CurrencyByDateView>> GetCurrenciesByDate()
        {
            Operation<List<CurrencyByDateView>> operation = new Operation<List<CurrencyByDateView>>();
            try
            {
                operation.Value = connection.GetAll<CurrencyByDateView>().ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
        public Operation<List<CardTotalByIntervalView>> GetCardTotalsByInterval(string userId, string dateBegin, string dateEnd)
        {
            Operation<List<CardTotalByIntervalView>> operation = new Operation<List<CardTotalByIntervalView>>();
            try
            {
                using (IDbConnection connection = new SqlConnection(ERPService.Properties.Settings.Default.DefaultConnectionString))
                {
                    //@userId,@begDate,@endDate
                    List<CardTotalByIntervalView> mainList = connection.Query<CardTotalByIntervalView>("SP_GetCardTotalsByInterval", new { userId = Convert.ToInt32(userId), begDate = dateBegin.GetDateFromFormattedString(), endDate = dateEnd.GetDateFromFormattedString() },null,true,0,CommandType.StoredProcedure).ToList();
                    foreach (var accItem in mainList)
                    {
                        if (accItem.RemByBegDate > 0)
                        {
                            accItem.RemDebitByBegDate = accItem.RemByBegDate;
                        }
                        else
                        {
                            accItem.RemCreditByBegDate = Math.Abs(accItem.RemByBegDate);
                        }
                        if (accItem.RemByEndDate > 0)
                        {
                            accItem.RemDebitByEndDate = accItem.RemByEndDate;
                        }
                        else
                        {
                            accItem.RemCreditByEndDate = Math.Abs(accItem.RemByEndDate);
                        }
                    }
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
            if (currencyByDate.Rate <= 0)
            {
                return new Operation<CurrencyByDate>() { Fail = "Nisbət yanlış daxil edilmişdir" };
            }
            if (currencyByDate.CurrencyId == 0)
            {
                return new Operation<CurrencyByDate>() { Fail = "Məzənnə seçilməmişdir" };
            }
            CurrencyByDate existingCurrencyBD = connection.Query<CurrencyByDate>("select * from CurrencyByDate where Date = @Date and CurrencyId = @CurrencyId", new {currencyByDate.Date, currencyByDate.CurrencyId }).FirstOrDefault();
            if (existingCurrencyBD != null) currencyByDate.Id = existingCurrencyBD.Id; // Cunki varsa merge edecek, merge etmek ucun de movcud olanin Id-si lazim
            return new DapperRepo().Post(currencyByDate);
        }
        public Operation<ExchangeTransaction> PostExchangeTransaction(ExchangeTransaction exchangeTransaction)
        {
            if (exchangeTransaction.Id == 0)
            {
                exchangeTransaction.Ficheno = GetNewNumber(6);
            }
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
        public Operation<CurrencyByDate> GetCurrencyLastValue(string Id)
        {
            Operation<CurrencyByDate> operation = new Operation<CurrencyByDate>();
            if (Id == "1")
            {
                operation.Value = new CurrencyByDate { CurrencyId = 1, Id = 0, Date = DateTime.Now, Rate =1 };
                operation.Successful = true;
                return operation;
            }
            try
            {
                CurrencyByDate currencyByDate = connection.Query<CurrencyByDate>("select TOP(1) * from CurrencyByDate Where CurrencyId = "+ Id +" order by Date Desc").FirstOrDefault();
                operation.Value = currencyByDate;
                operation.Successful = currencyByDate != null;
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


        string GetNewNumber(byte DocumentTypeNumber, IDbTransaction dbTransaction = null)
        {
            IDbConnection dbConnection = connection;
            if (dbTransaction != null) dbConnection = dbTransaction.Connection;
            return dbConnection.ExecuteScalar<string>("SP_GetNewDocumentNumber", new { DocumentTypeNumber }, dbTransaction, 0, CommandType.StoredProcedure).ToString() ;
        }

        public Operation<FicheMasterView> GetFicheById(string Id)
        {
            Operation<FicheMasterView> op_fiche = new Operation<FicheMasterView>();
            try
            {
                FicheMasterView fiche = connection.Get<FicheMasterView>(Id);
                fiche.Lines = connection.Query<FicheLineView>("SELECT * FROM FicheLineView WHERE FicheId = " + Id).ToList();
                foreach (var line in fiche.Lines)
                {
                    line.LineServices = connection.Query<FicheLineServiceView>("SELECT * FROM FicheLineServiceView WHERE FicheLineId = " + line.Id.ToString()).ToList();
                }
                op_fiche.Value = fiche;
                op_fiche.Successful = true;
            }
            catch (Exception ex)
            {
                op_fiche.Fail = ex.Message;
            }
            return op_fiche;
        }

        public Operation<FicheMasterView> PostFiche(FicheMasterView fiche)
        {
            Operation<FicheMasterView> operation = new Operation<FicheMasterView>();
            IDbTransaction transaction = null;
            IDbConnection dbConnection = null;
            try
            {
                if (fiche == null)
                {
                    return new Operation<FicheMasterView>() { Fail = "Fiche is null !" };
                }
                if (fiche.Lines == null)
                {
                    return new Operation<FicheMasterView>() { Fail = "Fiche has no lines !" };
                }
                if (fiche.Lines.Count == 0)
                {
                    return new Operation<FicheMasterView>() { Fail = "Fiche has no lines !" };
                }

                int LineNumber = 0;

                fiche.Lines.ForEach(x=>x.LineTotalAcc = x.LineNetTotal + x.LineExpense);

                
                

                try
                {
                    dbConnection = connection;
                    dbConnection.Open();
                    transaction = dbConnection.BeginTransaction();
                }
                catch (Exception ex)
                {
                    return new Operation<FicheMasterView>() { Fail = ex.Message };
                }
                fiche.Lines.ForEach(x => x.LineTotalAcc = x.LineNetTotal + x.LineExpense + x.LineServices.Sum(y => y.LineNet));
                fiche.LinesTotal = fiche.Lines.Sum(x=>x.LineTotal);
                fiche.LineDiscountsTotal = fiche.Lines.Sum(x => x.LineDiscount);
                fiche.LinesNetTotal = fiche.Lines.Sum(x => x.LineNetTotal);
                fiche.FicheServicesNetTotal = fiche.Lines.Sum(x => x.LineServices.Sum(y=>y.LineNet));
                fiche.FicheNetTotal = fiche.LinesNetTotal + fiche.FicheServicesNetTotal - fiche.FicheDiscount;
                fiche.LineExpensesTotal = fiche.Lines.Sum(x => x.LineExpense);
                fiche.FicheTotalAcc = fiche.Lines.Sum(x=>x.LineTotalAcc);

                FicheMaster ficheMaster = fiche.GetEligibleOjbect<FicheMaster>();
                FicheMasterView existingFicheView = GetFicheById(fiche.Id.ToString()).Value;
                List<FicheLineView> oldLines = new List<FicheLineView>();
                List<FicheLineView> newLines = fiche.Lines;

                


                if (fiche.Id == 0)
                {
                    fiche.Ficheno = GetNewNumber(fiche.DocTypeId, transaction);
                    ficheMaster.Ficheno = fiche.Ficheno;
                    switch (fiche.DocTypeId)
                    {
                        case 1: ficheMaster.StatusId = 1; break;
                        case 2: ficheMaster.StatusId = 5; break;
                        case 3: ficheMaster.StatusId = 19; break;
                        case 4: ficheMaster.StatusId = 17; break;
                    }
                    dbConnection.Insert(ficheMaster, transaction);
                    
                }
                else
                {
                    oldLines = existingFicheView.Lines;
                    dbConnection.Update(ficheMaster,transaction);
                }

                fiche.Id = ficheMaster.Id;
                foreach (FicheLineView line in fiche.Lines)
                {
                    line.LineServices = line.LineServices.Where(x => x.Quantity > 0).ToList();
                    LineNumber++;
                    line.FicheId = fiche.Id;
                    line.LineNumber = LineNumber;
                    if (line.Note == null) line.Note = "";
                }


                CollectionChangesComparer<FicheLineView> ccf = new CollectionChangesComparer<FicheLineView>();
                ccf.KeyFieldName = "Id";
                ccf.SetInitialList(oldLines);
                ccf.SetFinalList(newLines);
                var deletes = ccf.GetDeletes();
                deletes.ForEach(x => dbConnection.Delete(x.LineServices.GetEligibleOjbect<List<FicheLineService>>(), transaction));
                dbConnection.Delete(deletes.GetEligibleOjbect<List<FicheLine>>(), transaction);
                var inserts = ccf.GetInserts();
                inserts.ForEach(x => x.FicheId = fiche.Id);
                foreach (var line in inserts)
                {
                    line.FicheId = fiche.Id;
                    FicheLine flmodel = line.GetEligibleOjbect<FicheLine>();
                    dbConnection.Insert(flmodel, transaction);
                    line.Id = flmodel.Id;
                    inserts.ForEach(x => x.LineServices = x.LineServices ?? new List<FicheLineServiceView>());
                    foreach (var serviceLine in line.LineServices)
                    {
                        serviceLine.FicheLineId = line.Id;
                    }
                    dbConnection.Insert(line.LineServices.GetEligibleOjbect<List<FicheLineService>>(), transaction);
                }
                var updates = ccf.GetUpdates();
                dbConnection.Update(updates.GetEligibleOjbect<List<FicheLine>>(), transaction);

                foreach (FicheLineView lineView in updates)
                {
                    List<FicheLineServiceView> oldServicesOfLine = fiche.Lines.Where(x => x.Id == lineView.Id).First().LineServices;
                    List<FicheLineServiceView> currentServicesOfLine = lineView.LineServices;
                    CollectionChangesComparer<FicheLineServiceView> ccs = new CollectionChangesComparer<FicheLineServiceView>();
                    ccs.KeyFieldName = "Id";
                    ccs.SetInitialList(oldServicesOfLine);
                    ccs.SetFinalList(currentServicesOfLine);
                    dbConnection.Delete(ccs.GetDeletes().GetEligibleOjbect<List<FicheLineService>>(), transaction);
                    dbConnection.Update(ccs.GetUpdates().GetEligibleOjbect<List<FicheLineService>>(), transaction);
                    List<FicheLineServiceView> srvInserts = ccs.GetInserts();
                    srvInserts.ForEach(x => x.FicheLineId = lineView.Id);
                    dbConnection.Insert(srvInserts.GetEligibleOjbect<List<FicheLineService>>(), transaction);
                }

                transaction.Commit();
                dbConnection.Close();
                operation.Value = fiche;
                operation.Successful = true;
            }
            catch (Exception gex)
            {
                operation.Fail = gex.Message;
                if (transaction == null)
                {
                    transaction.Rollback();
                    transaction.Dispose();
                }
                if (dbConnection == null)
                {
                    dbConnection.Dispose();
                }
            }
            return operation;

         
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

        public Operation<List<FicheMasterView>> GetFiches(string DocType, string dateBegin, string dateEnd)
        {
            Operation<List<FicheMasterView>> operation = new Operation<List<FicheMasterView>>();
            try
            {
                operation.Value = connection.Query<FicheMasterView>("SELECT * FROM FicheMasterView WHERE DocTypeId = @DocTypeId and CreatedDate between @dateBegin and @dateEnd ", new { DocTypeId = Convert.ToByte(DocType), dateBegin = dateBegin.GetDateFromFormattedString(), dateEnd = dateEnd.GetDateFromFormattedString() }).ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
        public Operation<List<FicheMasterView>> GetFichesByProcessId(string ProcessId)
        {
            Operation<List<FicheMasterView>> operation = new Operation<List<FicheMasterView>>();
            try
            {
                if (Convert.ToInt32(ProcessId) == 0)
                {
                    operation.Value = new List<FicheMasterView>();
                }
                else
                {
                    operation.Value = connection.Query<FicheMasterView>("SELECT * FROM FicheMasterView where ProcessId = " + Convert.ToInt32(ProcessId).ToString()).ToList();
                }
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }

        public Operation<List<WarehouseProcessDetailView>> GetWarehouseProcessDetails(string ProcessId)
        {
            Operation<List<WarehouseProcessDetailView>> operation = new Operation<List<WarehouseProcessDetailView>>();
            try
            {
                if (Convert.ToInt32(ProcessId) == 0)
                {
                    operation.Value = new List<WarehouseProcessDetailView>();
                }
                else
                {
                    operation.Value = connection.Query<WarehouseProcessDetailView>("SELECT * FROM WarehouseProcessDetailView where ProcessId = " + Convert.ToInt32(ProcessId).ToString()).ToList();
                }
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }

        public Operation<List<WarehouseProcessView>> GetWarehouseProcesses(string dateBegin, string dateEnd)
        {
            Operation<List<WarehouseProcessView>> operation = new Operation<List<WarehouseProcessView>>();
            try
            {
                operation.Value = connection.Query<WarehouseProcessView>(" select * from WarehouseProcessView where CreatedDate between @dateBegin and @dateEnd ", new { dateBegin = dateBegin.GetDateFromFormattedString(), dateEnd = dateEnd.GetDateFromFormattedString() }).ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }

        public Operation<List<WarehouseProcessView>> GetActiveWarehouseProcesses()
        {
            Operation<List<WarehouseProcessView>> operation = new Operation<List<WarehouseProcessView>>();
            try
            {
                operation.Value = connection.Query<WarehouseProcessView>(" select * from WarehouseProcessView where StatusId = 15 ").ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }

        public Operation<WarehouseProcessView> PostWarehouseProcess(WarehouseProcessView warehouseProcess)
        {
            Operation<WarehouseProcessView> operation = new Operation<WarehouseProcessView>();
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    IDbTransaction transaction = dbConnection.BeginTransaction();
                    warehouseProcess.StatusId = 14;
                    WarehouseProcess warehouseProcessModel = warehouseProcess.GetEligibleOjbect<WarehouseProcess>();
                    warehouseProcessModel.Id = Convert.ToInt32(GetNewNumber(7, transaction));
                    warehouseProcess.Id = warehouseProcessModel.Id;
                    dbConnection.Insert(warehouseProcessModel, transaction);
                    warehouseProcess.Fiches.ForEach(x => x.ProcessId = warehouseProcessModel.Id);
                    dbConnection.Insert(warehouseProcess.Fiches, transaction);
                    transaction.Commit();
                    dbConnection.Close();
                    operation.Value = warehouseProcess;
                    operation.Successful = true;
                }
               
            }
            catch (Exception gex)
            {
                operation.Fail = gex.Message;
            }
            return operation;
        }

        public Operation<WarehouseProcessView> WarehouseProcessBegin(int ProcessId)
        {
            Operation<WarehouseProcessView> operation = new Operation<WarehouseProcessView>();
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    IDbTransaction transaction = dbConnection.BeginTransaction();
                    var existingProc = dbConnection.Get<WarehouseProcessView>(ProcessId, transaction);
                    if (existingProc.StatusId != 14)
                    {
                        transaction.Commit();
                        dbConnection.Close();
                        return operation;
                    }
                    var lst = dbConnection.Query<int>(@"select Distinct ServiceItemId 
from FicheLineService where FicheLineId IN (select Id 
from FicheLine where FicheId IN (select FicheId 
from WarehouseProcessFiche where ProcessId = @ProcessId ))", new { ProcessId }, transaction);
                    foreach (int itemId in lst)
                    {
                        dbConnection.Insert(new WarehouseProcessDetail { ServiceItemId = itemId, ProcessId = ProcessId }, transaction);
                    }
                    WarehouseProcess warehouseProcess = dbConnection.Get<WarehouseProcess>(ProcessId, transaction);
                    warehouseProcess.StatusId = 15;
                    warehouseProcess.ProcessBeginDate = DateTime.Now;
                    dbConnection.Update(warehouseProcess, transaction);
                    transaction.Commit();
                    operation.Value = dbConnection.Get<WarehouseProcessView>(ProcessId);
                    dbConnection.Close();

                    operation.Successful = true;
                }
            }
            catch (Exception gex)
            {
                operation.Fail = gex.Message;
            }
            return operation;
        }

        public Operation<WarehouseProcessView> WarehouseProcessEnd(int ProcessId)
        {
            Operation<WarehouseProcessView> operation = new Operation<WarehouseProcessView>();
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    IDbTransaction transaction = dbConnection.BeginTransaction();
                    var existingProc = dbConnection.Get<WarehouseProcessView>(ProcessId, transaction);
                    if (existingProc.StatusId != 15)
                    {
                        transaction.Commit();
                        dbConnection.Close();
                        return operation;
                    }
                    WarehouseProcess warehouseProcess = dbConnection.Get<WarehouseProcess>(ProcessId, transaction);
                    warehouseProcess.StatusId = 16;
                    warehouseProcess.ProcessEndDate = DateTime.Now;
                    dbConnection.Update(warehouseProcess, transaction);
                    transaction.Commit();
                    operation.Value = dbConnection.Get<WarehouseProcessView>(ProcessId);
                    dbConnection.Close();
                    operation.Successful = true;
                }
            }
            catch (Exception gex)
            {
                operation.Fail = gex.Message;
            }
            return operation;
        }

        public Operation<WarehouseProcessDetail> CompleteWarehouseProcessDetail(int Id, int UserId)
        {
            
            Operation<WarehouseProcessDetail> operation = new Operation<WarehouseProcessDetail>();
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();

                    var existingDetail = dbConnection.Get<WarehouseProcessDetail>(Id);
                    if (!existingDetail.IsCompleted)
                    {
                        dbConnection.Execute("SP_CompleteWarehouseProcessDetail", new { Id = Id, UserId = UserId }, null, 20, CommandType.StoredProcedure);
                    }
                    operation.Value = dbConnection.Get<WarehouseProcessDetail>(Id);
                    operation.Successful = true;
                }
            }
            catch (Exception gex)
            {
                operation.Fail = gex.Message;
            }
            return operation;
        }

        public Operation<List<FicheReportLineViewLC>> GetFicheReportLines(string dateBegin, string dateEnd)
        {
            Operation<List<FicheReportLineViewLC>> operation = new Operation<List<FicheReportLineViewLC>>();
            try
            {
                operation.Value = connection.Query<FicheReportLineViewLC>(" select * from FicheReportLineViewLC where CreatedDate between @dateBegin and @dateEnd ", new { dateBegin = dateBegin.GetDateFromFormattedString(), dateEnd = dateEnd.GetDateFromFormattedString() }).ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
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

        public Operation<List<CashTransactionView>> GetCashTransactionsViewByFiche(string FicheId)
        {
            using (IDbConnection dbConnection = connection)
            {
                Operation<List<CashTransactionView>> operation = new Operation<List<CashTransactionView>>();
                if (FicheId == "0")
                {
                    operation.Successful = true;
                    operation.Value = new List<CashTransactionView>();
                    return operation;
                }
                try
                {
                    string query = @"SELECT * FROM CashTransactionView Where ConnectedFicheId = " + Convert.ToInt32(FicheId).ToString();
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

    public class LoginData
    {
        public string Login { get; set; }
        public string PassHash { get; set; }
    }
}
