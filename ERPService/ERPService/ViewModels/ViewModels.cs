using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;
using ERPService.Models;

namespace ERPService.ViewModels
{
   

    //[DataContract]
    //[Table("CardTransactionView")]
    //public class CardTransactionView 
    //{
    //    [DataMember]
    //    public string DocTypeName { get; set; }
    //    [DataMember]
    //    public string Ficheno { get; set; }
    //    [DataMember]
    //    public int CardId { get; set; }
    //    [DataMember]
    //    public decimal Total { get; set; }
    //    [DataMember]
    //    public DateTime CreatedDate { get; set; }
    //    [DataMember]
    //    public decimal TotalWithSign { get; set; }
    //}


    // Inherited Clases

    //[DataContract]
    //[Table("CardMasterView")]
    //public class CardMasterView : CardMaster
    //{
    //    [DataMember]
    //    public string CardTypeName { get; set; }
    //    [DataMember]
    //    public string ExchangeName { get; set; }
    //    [DataMember]
    //    public decimal Debt { get; set; }
    //    [DataMember]
    //    public bool ByPermission { get; set; }
    //}

    //[DataContract]
    //public class CardTotalByIntervalView : CardMaster
    //{
    //    [DataMember]
    //    public string CardTypeName { get; set; }
    //    [DataMember]
    //    public string ExchangeName { get; set; }
    //    [DataMember]
    //    public decimal RemByBegDate { get; set; }
    //    [DataMember]
    //    public decimal TotalInput { get; set; }
    //    [DataMember]
    //    public decimal TotalOutput { get; set; }
    //    [DataMember]
    //    public decimal RemByEndDate { get; set; }

    //}

    //[DataContract]
    //public class ItemView : Item
    //{
    //    [DataMember]
    //    public string ItemTypeName { get; set; }
    //    [DataMember]
    //    public decimal DefaultSalePrice { get; set; }
    //    [DataMember]
    //    public decimal DefaultPurchasePrice { get; set; }
    //}

    //[DataContract]
    //public class ItemViewAcc : ItemView
    //{
    //    [DataMember]
    //    public decimal BegAmount { get; set; }
    //    [DataMember]
    //    public decimal InputAmount { get; set; }
    //    [DataMember]
    //    public decimal OutputAmount { get; set; }
    //    [DataMember]
    //    public decimal EndAmount { get; set; }
    //}

    //[DataContract]
    //[Table("FicheMasterView")]
    //public class FicheMasterView : FicheMaster
    //{
    //    [DataMember]
    //    public string ExchangeName { get; set; }
    //    [DataMember]
    //    public decimal Total { get; set; }
    //    [DataMember]
    //    public decimal WeightTotal { get; set; }
    //    [DataMember]
    //    public string CardNumber { get; set; }
    //    [DataMember]
    //    public string CardName { get; set; }
    //    [DataMember]
    //    public int LineCount { get; set; }
    //    [DataMember]
    //    public bool WorksCompleted { get; set; }
    //    [DataMember]
    //    public string StatusName { get; set; }
    //    [DataMember]
    //    public List<FicheLineView> FicheLines { get; set; }
    //}

    //[DataContract]
    //public class GridViewInfo : GridViewMaster
    //{
    //    [DataMember]
    //    public List<GridViewColumn> GridViewColumns { get; set; }
    //}

    //[Table("FicheLineView")]
    //public class FicheLineView : FicheLine
    //{
    //    [DataMember]
    //    public string ItemCode { get; set; }
    //    [DataMember]
    //    public string ItemName { get; set; }
    //    [DataMember]
    //    public string ItemTypeName { get; set; }
    //    [DataMember]
    //    public decimal ServiceLineTotalSum { get; set; }
    //    [DataMember]
    //    public decimal ServiceLineNetTotalSum { get; set; }
    //    [DataMember]
    //    public decimal LineFinalSum { get; set; }
    //    [DataMember]
    //    public List<FicheLineServiceView> Services { get; set; }
    //}

    //[DataContract]
    //public class ItemPriceForCard
    //{
    //    [DataMember]
    //    public int CardId { get; set; }
    //    [DataMember]
    //    public string CardNumber { get; set; }
    //    [DataMember]
    //    public string CardName { get; set; }
    //    [DataMember]
    //    public decimal? Price { get; set; }
    //    [DataMember]
    //    public bool IsSpecial { get; set; }


    //}

    //[DataContract]
    //public class ItemPriceOperation : ItemPrice
    //{
    //    [DataMember]
    //    public byte OperationType { get; set; }
    //}

    [DataContract]
    public class UserView : User
    {
        [DataMember]
        public List<PermissionDetail> PermissionDetails { get; set; }

        [DataMember]
        public LoginSession LoginSession { get; set; }

        [DataMember]
        public List<CardPermission> CardPermissions { get; set; }
    }

    //[DataContract]
    //public class UserDataPermissionView : CardPermission
    //{
    //    [DataMember]
    //    public string PermissionTypeName { get; set; }
    //    [DataMember]
    //    public string PermissionCode { get; set; }
    //    [DataMember]
    //    public string PermissionName { get; set; }
    //}

    //[DataContract]
    //public class CashTransactionView : CashTransaction
    //{
    //    [DataMember]
    //    public string SourceCardNumber { get; set; }
    //    [DataMember]
    //    public string SourceCardName { get; set; }
    //    [DataMember]
    //    public string DestCardNumber { get; set; }
    //    [DataMember]
    //    public string DestCardName { get; set; }
    //    [DataMember]
    //    public string ExchangeName { get; set; }
    //    [DataMember]
    //    public string UserName { get; set; }
    //    [DataMember]
    //    public string StatusName { get; set; }
    //    [DataMember]
    //    public int SourceCardTypeId { get; set; }
    //    [DataMember]
    //    public int DestCardTypeId { get; set; }
    //    [DataMember]
    //    public string ConnectedInvoice { get; set; }
    //    [DataMember]
    //    public string CashCategoryName { get; set; }

    //}

    //[DataContract]
    //public class FicheLineServiceView : FicheLineService
    //{
    //    [DataMember]
    //    public string ItemCode { get; set; }
    //    [DataMember]
    //    public string ItemName { get; set; }
    //}

}
