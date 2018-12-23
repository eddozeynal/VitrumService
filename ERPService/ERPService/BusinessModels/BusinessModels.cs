using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DBModels;
using Dapper.Contrib.Extensions;

namespace BusinessModels
{

    [DataContract]
    public class CardMasterView : CardMaster
    {
        [DataMember]
        public string CardTypeName { get; set; }
        [DataMember]
        public string ExchangeName { get; set; }
        [DataMember]
        public decimal Debt { get; set; }
    }

    [DataContract]
    public class CardTotalByIntervalView : CardMaster
    {
        [DataMember]
        public string CardTypeName { get; set; }
        [DataMember]
        public string ExchangeName { get; set; }
        [DataMember]
        public decimal RemByBegDate { get; set; }
        [DataMember]
        public decimal TotalInput { get; set; }
        [DataMember]
        public decimal TotalOutput { get; set; }
        [DataMember]
        public decimal RemByEndDate { get; set; }

    }

    [DataContract]
    public class ItemView : Item
    {
        [DataMember]
        public string ItemTypeName { get; set; }
    }

    [DataContract]
    [Table("FicheMasterView")]
    public class FicheMasterView : FicheMaster
    {
        [DataMember]
        public string ExchangeName { get; set; }
        [DataMember]
        public decimal Total { get; set; }
        [DataMember]
        public decimal WeightTotal { get; set; }
        [DataMember]
        public string CardNumber { get; set; }
        [DataMember]
        public string CardName { get; set; }
        [DataMember]
        public int LineCount { get; set; }
        [DataMember]
        public bool WorksCompleted { get; set; }
        [DataMember]
        public string StatusName { get; set; }
    }

    [DataContract]
    public class GridViewInfo
    {
        [DataMember]
        public GridViewMaster GridViewMaster_ { get; set; }
        [DataMember]
        public List<GridViewColumn> GridViewColumns_ { get; set; }
    }
    [DataContract]
    public class Fiche
    {
        [DataMember]
        public FicheMaster FicheMaster { get; set; }
        [DataMember]
        public List<FicheLine> FicheLines { get; set; }
    }

    [DataContract]
    public class FicheView
    {
        [DataMember]
        public FicheMasterView FicheMaster { get; set; }
        [DataMember]
        public List<FicheLineView> FicheLines { get; set; }

        

    }
    [Table("FicheLineView")]
    public class FicheLineView : FicheLine
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemTypeName { get; set; }
    }

    [DataContract]
    public class ItemPriceForCard
    {
        [DataMember]
        public int CardId { get; set; }
        [DataMember]
        public string CardNumber { get; set; }
        [DataMember]
        public string CardName { get; set; }
        [DataMember]
        public decimal? Price { get; set; }
        [DataMember]
        public bool IsSpecial { get; set; }


    }

    [DataContract]
    public class ItemPriceOperation : ItemPrice
    {
        [DataMember]
        public byte OperationType { get; set; }
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public BaseUser BaseUser { get; set; }

        [DataMember]
        public List<PermissionDetail> PermissionDetails { get; set; }

        [DataMember]
        public LoginSession LoginSession { get; set; }
    }

    [DataContract]
    public class UserDataPermissionView : DataPermission
    {
        [DataMember]
        public string PermissionTypeName { get; set; }
        [DataMember]
        public string PermissionCode { get; set; }
        [DataMember]
        public string PermissionName { get; set; }
    }

    [DataContract]
    public class CashTransactionView : CashTransaction
    {
        [DataMember]
        public string SourceCardNumber { get; set; }
        [DataMember]
        public string SourceCardName { get; set; }
        [DataMember]
        public string DestCardNumber { get; set; }
        [DataMember]
        public string DestCardName { get; set; }
        [DataMember]
        public string ExchangeName { get; set; }
        [DataMember]
        public string TransactionName { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string StatusName { get; set; }
        [DataMember]
        public int SourceCardTypeId { get; set; }
        [DataMember]
        public int DestCardTypeId { get; set; }

    }

}
