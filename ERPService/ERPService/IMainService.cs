using ERPService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ERPService.ViewModels;
using ErpService.Models;

namespace ERPService
{
    [ServiceContract]
    public interface IMainService
    {

        //[OperationContract]
        //[WebInvoke(UriTemplate = "PostTest2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<TestClassView> PostTest2(TestClassView data);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "PostItem", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<Item> PostItem(Item item);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetItem/{Id}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<Item> GetItem(string Id);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetCardMaster/{Id}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<CardMaster> GetCardMaster(string Id);
        [OperationContract]
        [WebGet(UriTemplate = "GetWarehouses", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<Warehouse>> GetWarehouses();

        [OperationContract]
        [WebGet(UriTemplate = "GetItems", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<ItemView>> GetItems();

        [OperationContract]
        [WebGet(UriTemplate = "GetItemTypes", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<ItemType>> GetItemTypes();

        [OperationContract]
        [WebGet(UriTemplate = "GetLineCalcTypes", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<LineCalcType>> GetLineCalcTypes();

        [OperationContract]
        [WebGet(UriTemplate = "GetItemById/{id}", ResponseFormat = WebMessageFormat.Json)]
        Operation<Item> GetItemById(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "GetItemPrices/{ItemId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<ItemPrice>> GetItemPrices(string ItemId);

        [OperationContract]
        [WebGet(UriTemplate = "GetWarehouseProcessDetails/{ProcessId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<WarehouseProcessDetailView>> GetWarehouseProcessDetails(string ProcessId);

        [OperationContract]
        [WebGet(UriTemplate = "GetItemPricesByCard/{CardId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<ItemPrice>> GetItemPricesByCard(string CardId);

        [OperationContract]
        [WebGet(UriTemplate = "GetCashTypes", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<CashType>> GetCashTypes();

        [OperationContract]
        [WebGet(UriTemplate = "GetCurrenciesByDate", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<CurrencyByDateView>> GetCurrenciesByDate();
        //[OperationContract]
        //[WebGet(UriTemplate = "GetAllExchanges", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<ExchangeMaster>> GetAllExchanges();

        [OperationContract]
        [WebGet(UriTemplate = "GetFichesByProcessId/{ProcessId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<FicheMasterView>> GetFichesByProcessId(string ProcessId);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetEnums/{EnumNumber}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<EnumMaster>> GetEnums(string EnumNumber);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetDateTest", ResponseFormat = WebMessageFormat.Json)]
        //Operation<TestClass> GetDateTest();

        //[OperationContract]
        //[WebGet(UriTemplate = "GetParameters", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<Parameter>> GetParameters();

        //[OperationContract]
        //[WebGet(UriTemplate = "GetGridViewInfo/{GridViewId}/{UserId}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<GridViewInfo> GetGridViewInfo(string GridViewId, string UserId);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "PostCardMaster", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<CardMaster> PostCardMaster(CardMaster cardMaster);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetExchangesFromBankByDate/{Date_}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<ExchangeByDate>> GetExchangesFromBankByDate(string Date_);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetItemPriceForCards/{ItemId}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<ItemPriceForCard>> GetItemPriceForCards(string ItemId);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "PostItemPrices", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<bool> PostItemPrices(List<ItemPriceOperation> ItemPrices);

        [OperationContract]
        [WebInvoke(UriTemplate = "LoginUser", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<UserView> LoginUser(LoginData loginData);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostUserPermissions", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<string> PostUserPermissions(int UserId, List<PermissionDetail> Details);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostItemPrices", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<string> PostItemPrices(int ItemId, List<ItemPrice> Details);

        [OperationContract]
        [WebInvoke(UriTemplate = "DeleteObjectById", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<string> DeleteObjectById(string TableName,int Id);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostCardPermission", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<CardPermission> PostCardPermission(CardPermission cardPermission);

        [OperationContract]
        [WebGet(UriTemplate = "GetCards", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<CardView>> GetCards();

        [OperationContract]
        [WebGet(UriTemplate = "GetCardsTest", ResponseFormat = WebMessageFormat.Json)]
        List<CardView> GetCardsTest();


        [OperationContract]
        [WebGet(UriTemplate = "GetDocumentMasters", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<DocumentMaster>> GetDocumentMasters();

        [OperationContract]
        [WebGet(UriTemplate = "GetCurrencyLastValue/{Id}", ResponseFormat = WebMessageFormat.Json)]
        Operation<CurrencyByDate> GetCurrencyLastValue(string Id);
        //[OperationContract]
        //[WebGet(UriTemplate = "GetWarehouses", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<WarehouseMaster>> GetWarehouses();
        [OperationContract]
        [WebGet(UriTemplate = "GetCardsByUserId/{userId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<CardView>> GetCardsByUserId(string userId);
        //[OperationContract]
        //[WebGet(UriTemplate = "GetItemPricesList", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<ItemPrice>> GetItemPricesList();

        [OperationContract]
        [WebInvoke(UriTemplate = "PostFiche", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<FicheMasterView> PostFiche(FicheMasterView fiche);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostCardFiche", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<CardFiche> PostCardFiche(CardFiche cardFiche);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostFicheExpence", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<FicheExpence> PostFicheExpence(FicheExpence ficheExpence);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostWarehouseProcess", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<WarehouseProcessView> PostWarehouseProcess(WarehouseProcessView warehouseProcess);


        [OperationContract]
        [WebGet(UriTemplate = "GetFiches/{DocType}/{dateBegin}/{dateEnd}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<FicheMasterView>> GetFiches(string DocType, string dateBegin, string dateEnd);

        [OperationContract]
        [WebGet(UriTemplate = "GetWarehouseProcesses/{dateBegin}/{dateEnd}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<WarehouseProcessView>> GetWarehouseProcesses(string dateBegin, string dateEnd);

        [OperationContract]
        [WebGet(UriTemplate = "GetActiveWarehouseProcesses", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<WarehouseProcessView>> GetActiveWarehouseProcesses();

        [OperationContract]
        [WebGet(UriTemplate = "GetFicheReportLines/{dateBegin}/{dateEnd}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<FicheReportLineViewLC>> GetFicheReportLines(string dateBegin, string dateEnd);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetCardTransactionViewByCardId/{cardId}/{dateBegin}/{dateEnd}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<CardTransactionView>> GetCardTransactionViewByCardId(string cardId, string dateBegin, string dateEnd);

        [OperationContract]
        [WebGet(UriTemplate = "GetCardTotalsByInterval/{userId}/{dateBegin}/{dateEnd}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<CardTotalByIntervalView>> GetCardTotalsByInterval(string userId, string dateBegin, string dateEnd);

        [OperationContract]
        [WebGet(UriTemplate = "GetCardFicheLinesView/{dateBegin}/{dateEnd}/{userId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<CardFicheLineView>> GetCardFicheLinesView(string dateBegin, string dateEnd, string userId);
        //[OperationContract]
        //[WebGet(UriTemplate = "GetItemTotalsByInterval/{userId}/{dateBegin}/{dateEnd}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<ItemViewAcc>> GetItemTotalsByInterval(string userId, string dateBegin, string dateEnd);



        //[OperationContract]
        //[WebGet(UriTemplate = "GetPriceCalcTypes", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<PriceCalcType>> GetPriceCalcTypes();

        [OperationContract]
        [WebGet(UriTemplate = "GetFicheById/{Id}", ResponseFormat = WebMessageFormat.Json)]
        Operation<FicheMasterView> GetFicheById(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "GetCardFicheById/{Id}", ResponseFormat = WebMessageFormat.Json)]
        Operation<CardFiche> GetCardFicheById(string Id);


        [OperationContract]
        [WebGet(UriTemplate = "GetUserPermissions/{UserId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<PermissionMasterView>> GetUserPermissions(string UserId);

        [OperationContract]
        [WebGet(UriTemplate = "GetCardPermissions/{UserId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<CardPermission>> GetCardPermissions(string UserId);

        [OperationContract]
        [WebGet(UriTemplate = "GetUsers", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<User>> GetUsers();

        [OperationContract]
        [WebGet(UriTemplate = "GetDocumentStatusTypes", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<DocumentStatusType>> GetDocumentStatusTypes();

        [OperationContract]
        [WebGet(UriTemplate = "GetUserById/{Id}", ResponseFormat = WebMessageFormat.Json)]
        Operation<User> GetUserById(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "GetCardById/{Id}", ResponseFormat = WebMessageFormat.Json)]
        Operation<Card> GetCardById(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "GetCardTypes", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<CardType>> GetCardTypes();


        [OperationContract]
        [WebGet(UriTemplate = "GetCurrencies", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<Currency>> GetCurrencies();

        [OperationContract]
        [WebGet(UriTemplate = "GetUserIdBySession", ResponseFormat = WebMessageFormat.Json)]
        Operation<int> GetUserIdBySession();

        [OperationContract]
        [WebGet(UriTemplate = "GetUserBySession", ResponseFormat = WebMessageFormat.Json)]
        Operation<UserView> GetUserBySession();

        [OperationContract]
        [WebGet(UriTemplate = "GetWorkStates", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<WorkStateView>> GetWorkStates();

        [OperationContract]
        [WebInvoke(UriTemplate = "PostUser", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<User> PostUser(User user);

        [OperationContract]
        [WebInvoke(UriTemplate = "CompleteWarehouseProcessDetail", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<WarehouseProcessDetail> CompleteWarehouseProcessDetail(int Id, int UserId);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostItem", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<Item> PostItem(Item item);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostCard", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<Card> PostCard(Card card);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostCurrencyByDate", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<CurrencyByDate> PostCurrencyByDate(CurrencyByDate currencyByDate);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "ChangeStatusCardFiche", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<CardFiche> ChangeStatusCardFiche(int Id, byte Status);

        [OperationContract]
        [WebInvoke(UriTemplate = "WarehouseProcessBegin", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<WarehouseProcessView> WarehouseProcessBegin(int ProcessId);

        [OperationContract]
        [WebInvoke(UriTemplate = "WarehouseProcessEnd", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<WarehouseProcessView> WarehouseProcessEnd(int ProcessId);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostFicheMaster", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<FicheMaster> PostFicheMaster(FicheMaster ficheMaster);

      
        //[OperationContract]
        //[WebInvoke(UriTemplate = "PostCashTransaction", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<CashTransaction> PostCashTransaction(CashTransaction cashTransaction);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "AcceptCashTransaction", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<CashTransaction> AcceptCashTransaction(int CashTransactionId, int UserId);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetUserDataPermissionView/{UserId}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<UserDataPermissionView>> GetUserDataPermissionView(string UserId);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "PostDataPermission", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<DataPermission> PostDataPermission(DataPermission dataPermission);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "DeleteDataPermission", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<int> DeleteDataPermission(int dataPermissionId);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetCardTypes", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<CardType>> GetCardTypes();
    }
}
