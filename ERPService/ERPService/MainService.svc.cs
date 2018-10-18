using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DBModels;
using BusinessModels;
using Repositories;

namespace ERPService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,InstanceContextMode = InstanceContextMode.Single)]
    public class MainService : IMainService
    {
        ItemRepository _ItemRepository = null;
        CardRepository _CardRepository = null;
        ExchangeRepository _ExchangeRepository = null;
        FicheRepository _FicheRepository = null;
        PrimaryRepository _PrimaryRepository = null;
        UserRepository _UserRepository = null;
        CashRepository _CashRepository = null;

        ItemRepository ItemRepository
        {
            get
            {
                if (_ItemRepository == null) _ItemRepository = new ItemRepository();
                return _ItemRepository;
            } 
        }
        CashRepository CashRepository
        {
            get
            {
                if (_CashRepository == null) _CashRepository = new CashRepository();
                return _CashRepository;
            }
        }

        UserRepository UserRepository
        {
            get
            {
                if (_UserRepository == null) _UserRepository = new UserRepository();
                return _UserRepository;
            }
        }

        CardRepository CardRepository
        {
            get
            {
                if (_CardRepository == null) _CardRepository = new CardRepository();
                return _CardRepository;
            }
        }

        ExchangeRepository ExchangeRepository
        {
            get
            {
                if (_ExchangeRepository == null) _ExchangeRepository = new ExchangeRepository();
                return _ExchangeRepository;
            }
        }

        FicheRepository FicheRepository
        {
            get
            {
                if (_FicheRepository == null) _FicheRepository = new FicheRepository();
                return _FicheRepository;
            }
        }

        PrimaryRepository PrimaryRepository
        {
            get
            {
                if (_PrimaryRepository == null) _PrimaryRepository = new PrimaryRepository();
                return _PrimaryRepository;
            }
        }

        public Operation<List<VW_Item>> GetAllItems()
        {
            return ItemRepository.GetAllItems();
        }

        public Operation<List<Parameter>> GetParameters()
        {
            return PrimaryRepository.GetParameters();
        }

        public Operation<CardMaster> GetCardMaster(string Id)
        {
            return CardRepository.GetCardMaster(Convert.ToInt32(Id));
        }

        public Operation<TestClass> GetDateTest()
        {
            Operation<TestClass> tc = new Operation<TestClass>();
            tc.Value = new TestClass() { Name = "Salam", Date_ = DateTime.Now };
            return tc;
        }

        public Operation<List<EnumMaster>> GetEnums(string EnumNumber)
        {
            return PrimaryRepository.GetEnums(EnumNumber);
        }

        public Operation<GridViewInfo> GetGridViewInfo(string GridViewId,string UserId)
        {
            Operation<GridViewInfo> r = new Operation<GridViewInfo>();
            try
            {
                r.Value = PrimaryRepository.GetGridViewInfo(Convert.ToInt32(GridViewId), Convert.ToInt32(UserId));
                r.Successful = true;
            }
            catch (Exception ex)
            {
                r.Fail = ex.Message;
            }
            return r;
        }

        public Operation<Item> GetItem(string Id)
        {
            return ItemRepository.GetItem(Convert.ToInt32(Id));
        }

        public Operation<Item> PostItem(Item item)
        {
            return ItemRepository.PostItem(item);
        }

        public string PostTest2(TestClass data)
        {
            return data.Name;
        }
        public Operation<CardMaster> PostCardMaster(CardMaster cardMaster)
        {
            return CardRepository.PostCardMaster(cardMaster);
        }

        public Operation<List<VW_CardMaster>> GetAllCards()
        {
            return CardRepository.GetAllCards();
        }
        public Operation<List<PriceCalcType>> GetPriceCalcTypes()
        {
            return ItemRepository.GetPriceCalcTypes();
        }
        public Operation<List<ExchangeMaster>> GetAllExchanges()
        {
            return ExchangeRepository.GetAllExchanges();
        }

        public Operation<List<ExchangeByDate>> GetExchangesFromBankByDate(string Date_)
        {
            return ExchangeRepository.GetExchangesFromBankByDate(Convert.ToDateTime(Date_));
        }

        public Operation<List<VW_ItemPricesDefault>> GetItemDefaultPricesView()
        {
            return ItemRepository.GetItemDefaultPricesView();
        }

        public Operation<List<ItemPriceForCard>> GetItemPriceForCards(string ItemId)
        {
            return ItemRepository.GetItemPriceForCards(ItemId);
        }

        public Operation<bool> PostItemPrices(List<ItemPriceOperation> ItemPrices)
        {
            return ItemRepository.PostItemPrices(ItemPrices);
        }

        public Operation<User> LoginUser(string UserName, string Password)
        {
            return UserRepository.LoginUser(UserName, Password);
        }

        public Operation<List<DocumentMaster>> GetDocumentMasters()
        {
            return PrimaryRepository.GetDocumentMasters();
        }

        public Operation<List<WarehouseMaster>> GetWarehouses()
        {
            return PrimaryRepository.GetWarehouses();
        }

        public Operation<List<ItemPrice>> GetItemPricesList()
        {
            return ItemRepository.GetItemPricesList();
        }

        public Operation<Fiche> PostFiche(Fiche fiche)
        {
            return FicheRepository.PostFiche(fiche);
        }

        public Operation<List<VW_FicheMaster>> GetFiches(string DocType, string dateBegin, string dateEnd)
        {
            return FicheRepository.GetFiches(Convert.ToByte(DocType), dateBegin.GetDateFromFormattedString(), dateEnd.GetDateFromFormattedString());
        }

        public Operation<Fiche> GetFicheById(string Id)
        {
            return FicheRepository.GetFicheById(Convert.ToInt32(Id));
        }

        public Operation<List<PermissionMaster>> GetPermissionMasters()
        {
            return UserRepository.GetPermissionMasters();
        }

        public Operation<List<PermissionDetail>> GetPermissionDetailsByUserId(string UserId)
        {
            return UserRepository.GetPermissionDetailsByUserId(Convert.ToInt32(UserId));
        }
        public Operation<List<BaseUser>> GetUsers()
        {
            return UserRepository.GetUsers();
        }
        public Operation<User> GetUserById(string Id)
        {
            return UserRepository.GetUserById(Convert.ToInt32(Id));
        }

        public Operation<User> PostUser(User user)
        {
            return UserRepository.PostUser(user);
        }
        public Operation<CashTransaction> GetCashTransactionById(string Id)
        {
            return CashRepository.GetCashTransaction(Convert.ToInt32(Id));
        }

        public Operation<CashTransaction> PostCashTransaction(CashTransaction CashTransaction)
        {
            return CashRepository.PostCashTransaction(CashTransaction);
        }

        public Operation<List<UserDataPermissionView>> GetUserDataPermissionView(string UserId)
        {
            return UserRepository.GetUserDataPermissionView(Convert.ToInt32(UserId));
        }

        public Operation<List<CashTransactionView>> GetCashTransactionsView(string dateBegin, string dateEnd, string userId)
        {
           
            return CashRepository.GetCashTransactionsView(dateBegin.GetDateFromFormattedString(), dateEnd.GetDateFromFormattedString(),Convert.ToInt32(userId));
        }

        public Operation<List<VW_CardMaster>> GetAllCardsExt(string date, string userId)
        {
            return CardRepository.GetAllCardsExt(date.GetDateFromFormattedString(), Convert.ToInt32(userId));
        }
        public Tuple<User, CardMaster> testTuple()
        {
            User user = UserRepository.GetUserById(1).Value;
            CardMaster card = CardRepository.GetCardMaster(1).Value;
            Tuple<User, CardMaster> tp = new Tuple<User, CardMaster>(user, card);
            return new Tuple<User, CardMaster>(user, card);
        }
        public Tuple<bool, CardMaster, string> testMulTuple()
        {
            User user = UserRepository.GetUserById(1).Value;
            CardMaster card = CardRepository.GetCardMaster(1).Value;
            Tuple < User, CardMaster, Fiche > tp = new Tuple<User, CardMaster, Fiche>(user, card, new Fiche());
            return new Tuple<bool, CardMaster, string>(true, card, "");
            //Exception ex = new Exception();
            //return new Tuple<bool, CardMaster, string>(false, null, ex.Message);
        }
      
    }
}
