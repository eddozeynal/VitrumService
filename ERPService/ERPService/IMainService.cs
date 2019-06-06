using ERPService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ERPService.ViewModels;

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
        [WebGet(UriTemplate = "GetItemPricesByCard/{CardId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<ItemPrice>> GetItemPricesByCard(string CardId);

        [OperationContract]
        [WebGet(UriTemplate = "GetCashTypes", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<CashType>> GetCashTypes();
        //[OperationContract]
        //[WebGet(UriTemplate = "GetAllExchanges", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<ExchangeMaster>> GetAllExchanges();



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
        [WebGet(UriTemplate = "GetDocumentMasters", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<DocumentMaster>> GetDocumentMasters();

        //[OperationContract]
        //[WebGet(UriTemplate = "GetWarehouses", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<WarehouseMaster>> GetWarehouses();

        //[OperationContract]
        //[WebGet(UriTemplate = "GetItemPricesList", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<ItemPrice>> GetItemPricesList();

        //[OperationContract]
        //[WebInvoke(UriTemplate = "PostFiche", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<FicheMasterView> PostFiche(FicheMasterView fiche);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "PostFiche2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<FicheMasterView> PostFiche2(FicheMasterView fiche);



        //[OperationContract]
        //[WebGet(UriTemplate = "GetFiches/{DocType}/{dateBegin}/{dateEnd}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<FicheMasterView>> GetFiches(string DocType,string dateBegin, string dateEnd);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetCardTransactionViewByCardId/{cardId}/{dateBegin}/{dateEnd}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<CardTransactionView>> GetCardTransactionViewByCardId(string cardId, string dateBegin, string dateEnd);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetCardTotalsByInterval/{userId}/{dateBegin}/{dateEnd}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<CardTotalByIntervalView>> GetCardTotalsByInterval(string userId, string dateBegin, string dateEnd);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetItemTotalsByInterval/{userId}/{dateBegin}/{dateEnd}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<ItemViewAcc>> GetItemTotalsByInterval(string userId, string dateBegin, string dateEnd);



        //[OperationContract]
        //[WebGet(UriTemplate = "GetPriceCalcTypes", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<PriceCalcType>> GetPriceCalcTypes();

        //[OperationContract]
        //[WebGet(UriTemplate = "GetFicheById/{Id}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<FicheMasterView> GetFicheById(string Id);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetPermissionMasters", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<PermissionMaster>> GetPermissionMasters();
        [OperationContract]
        [WebInvoke(UriTemplate = "PostExchangeTransaction", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<ExchangeTransaction> PostExchangeTransaction(ExchangeTransaction exchangeTransaction);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostCashTransaction", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<CashTransaction> PostCashTransaction(CashTransaction cashTransaction);

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
        [WebInvoke(UriTemplate = "PostUser", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<User> PostUser(User user);

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
        //[WebInvoke(UriTemplate = "PostExchangeTransaction", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<ExchangeTransaction> PostExchangeTransaction(ExchangeTransaction exchangeTransaction);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostFicheMaster", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<FicheMaster> PostFicheMaster(FicheMaster ficheMaster);

        [OperationContract]
        [WebGet(UriTemplate = "GetCashTransactionById/{Id}", ResponseFormat = WebMessageFormat.Json)]
        Operation<CashTransaction> GetCashTransactionById(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "GetExchangeTransactionById/{Id}", ResponseFormat = WebMessageFormat.Json)]
        Operation<ExchangeTransaction> GetExchangeTransactionById(string Id);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "PostCashTransaction", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<CashTransaction> PostCashTransaction(CashTransaction cashTransaction);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "AcceptCashTransaction", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<CashTransaction> AcceptCashTransaction(int CashTransactionId, int UserId);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetUserDataPermissionView/{UserId}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<UserDataPermissionView>> GetUserDataPermissionView(string UserId);

        [OperationContract]
        [WebGet(UriTemplate = "GetCashTransactionsView/{dateBegin}/{dateEnd}/{userId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<CashTransactionView>> GetCashTransactionsView(string dateBegin, string dateEnd, string userId);

        [OperationContract]
        [WebGet(UriTemplate = "GetExchangeTransactionsView/{dateBegin}/{dateEnd}/{userId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<ExchangeTransactionView>> GetExchangeTransactionsView(string dateBegin, string dateEnd, string userId);
        //[OperationContract]
        //[WebInvoke(UriTemplate = "PostDataPermission", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<DataPermission> PostDataPermission(DataPermission dataPermission);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "DeleteDataPermission", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Operation<int> DeleteDataPermission(int dataPermissionId);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetCardTypes", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<CardType>> GetCardTypes();

        //[OperationContract]
        //[WebGet(UriTemplate = "GetCashTransactionsViewByFiche/{FicheId}", ResponseFormat = WebMessageFormat.Json)]
        //Operation<List<CashTransactionView>> GetCashTransactionsViewByFiche(string FicheId);
    }
}
