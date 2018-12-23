using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DBModels;
using Repositories;
using BusinessModels;

namespace ERPService
{
    [ServiceContract]
    public interface IMainService
    {

        [OperationContract]
        [WebInvoke(UriTemplate = "PostTest2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string PostTest2(TestClass data);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostItem", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<Item> PostItem(Item item);

        [OperationContract]
        [WebGet(UriTemplate = "GetItem/{Id}", ResponseFormat = WebMessageFormat.Json)]
        Operation<Item> GetItem(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "GetCardMaster/{Id}", ResponseFormat = WebMessageFormat.Json)]
        Operation<CardMaster> GetCardMaster(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "GetAllItems", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<ItemView>> GetAllItems();

        [OperationContract]
        [WebGet(UriTemplate = "GetAllExchanges", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<ExchangeMaster>> GetAllExchanges();

        [OperationContract]
        [WebGet(UriTemplate = "GetAllCards", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<CardMasterView>> GetAllCards();

        [OperationContract]
        [WebGet(UriTemplate = "GetEnums/{EnumNumber}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<EnumMaster>> GetEnums(string EnumNumber);

        [OperationContract]
        [WebGet(UriTemplate = "GetDateTest", ResponseFormat = WebMessageFormat.Json)]
        Operation<TestClass> GetDateTest();

        [OperationContract]
        [WebGet(UriTemplate = "GetParameters", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<Parameter>> GetParameters();

        [OperationContract]
        [WebGet(UriTemplate = "GetGridViewInfo/{GridViewId}/{UserId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<GridViewInfo> GetGridViewInfo(string GridViewId, string UserId);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostCardMaster", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<CardMaster> PostCardMaster(CardMaster cardMaster);

        [OperationContract]
        [WebGet(UriTemplate = "GetExchangesFromBankByDate/{Date_}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<ExchangeByDate>> GetExchangesFromBankByDate(string Date_);

        [OperationContract]
        [WebGet(UriTemplate = "GetItemDefaultPricesView", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<ItemDefaultPrices>> GetItemDefaultPricesView();

        [OperationContract]
        [WebGet(UriTemplate = "GetItemPriceForCards/{ItemId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<ItemPriceForCard>> GetItemPriceForCards(string ItemId);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostItemPrices", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<bool> PostItemPrices(List<ItemPriceOperation> ItemPrices);

        [OperationContract]
        [WebInvoke(UriTemplate = "LoginUser", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<User> LoginUser(string UserName, string Password);

        [OperationContract]
        [WebGet(UriTemplate = "GetDocumentMasters", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<DocumentMaster>> GetDocumentMasters();

        [OperationContract]
        [WebGet(UriTemplate = "GetWarehouses", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<WarehouseMaster>> GetWarehouses();

        [OperationContract]
        [WebGet(UriTemplate = "GetItemPricesList", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<ItemPrice>> GetItemPricesList();

        [OperationContract]
        [WebInvoke(UriTemplate = "PostFiche", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<Fiche> PostFiche(Fiche fiche);


        [OperationContract]
        [WebGet(UriTemplate = "GetFiches/{DocType}/{dateBegin}/{dateEnd}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<FicheMasterView>> GetFiches(string DocType,string dateBegin, string dateEnd);

        [OperationContract]
        [WebGet(UriTemplate = "GetCardTransactionViewByCardId/{cardId}/{dateBegin}/{dateEnd}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<CardTransactionView>> GetCardTransactionViewByCardId(string cardId, string dateBegin, string dateEnd);

        [OperationContract]
        [WebGet(UriTemplate = "GetCardTotalsByInterval/{userId}/{dateBegin}/{dateEnd}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<CardTotalByIntervalView>> GetCardTotalsByInterval(string userId, string dateBegin, string dateEnd);

        [OperationContract]
        [WebGet(UriTemplate = "GetPriceCalcTypes", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<PriceCalcType>> GetPriceCalcTypes();

        [OperationContract]
        [WebGet(UriTemplate = "GetFicheById/{Id}", ResponseFormat = WebMessageFormat.Json)]
        Operation<Fiche> GetFicheById(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "GetFicheViewById/{Id}", ResponseFormat = WebMessageFormat.Json)]
        Operation<FicheView> GetFicheViewById(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "GetPermissionMasters", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<PermissionMaster>> GetPermissionMasters();

        [OperationContract]
        [WebGet(UriTemplate = "GetPermissionDetailsByUserId/{UserId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<PermissionDetail>> GetPermissionDetailsByUserId( string UserId);

        [OperationContract]
        [WebGet(UriTemplate = "GetUsers", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<BaseUser>> GetUsers();

        [OperationContract]
        [WebGet(UriTemplate = "GetUserById/{Id}", ResponseFormat = WebMessageFormat.Json)]
        Operation<User> GetUserById(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "ChangeFicheStatus/{Id}/{StatusId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<Fiche> ChangeFicheStatus(string Id, string StatusId);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostUser", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<User> PostUser(User user);

        [OperationContract]
        [WebGet(UriTemplate = "GetCashTransactionById/{Id}", ResponseFormat = WebMessageFormat.Json)]
        Operation<CashTransaction> GetCashTransactionById(string Id);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostCashTransaction", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<CashTransaction> PostCashTransaction(CashTransaction cashTransaction);

        [OperationContract]
        [WebGet(UriTemplate = "GetUserDataPermissionView/{UserId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<UserDataPermissionView>> GetUserDataPermissionView(string UserId);

        [OperationContract]
        [WebGet(UriTemplate = "GetCashTransactionsView/{dateBegin}/{dateEnd}/{userId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<CashTransactionView>> GetCashTransactionsView(string dateBegin, string dateEnd, string userId);

        [OperationContract]
        [WebGet(UriTemplate = "GetAllCardsExt/{date}/{userId}", ResponseFormat = WebMessageFormat.Json)]
        Operation<List<CardMasterView>> GetAllCardsExt(string date, string userId);

        [OperationContract]
        [WebInvoke(UriTemplate = "PostDataPermission", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<DataPermission> PostDataPermission(DataPermission dataPermission);

        [OperationContract]
        [WebInvoke(UriTemplate = "DeleteDataPermission", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Operation<int> DeleteDataPermission(int dataPermissionId);
    }
}
