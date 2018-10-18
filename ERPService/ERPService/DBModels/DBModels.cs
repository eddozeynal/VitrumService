using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace DBModels
{
    [DataContract]
    [Table("BaseUser")]
    public class BaseUser
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Name_ { get; set; }
        [DataMember]
        public string PassHash { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public bool IsAdmin { get; set; }
    }

    [DataContract]
    [Table("CardMaster")]
    public class CardMaster
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public byte CardType { get; set; }
        [DataMember]
        public string CardNumber { get; set; }
        [DataMember]
        public string CardName { get; set; }
        [DataMember]
        public decimal DebtLimit { get; set; }
        [DataMember]
        public byte ExchangeId { get; set; }
        [DataMember]
        public string TaxCode { get; set; }
        [DataMember]
        public string LocationAddress { get; set; }
        [DataMember]
        public string Phone1 { get; set; }
        [DataMember]
        public string Phone2 { get; set; }
        [DataMember]
        public string Phone3 { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Town { get; set; }
        [DataMember]
        public string District { get; set; }
        [DataMember]
        public float Latitude { get; set; }
        [DataMember]
        public float Longitude { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    [Table("CashTransaction")]
    public class CashTransaction
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public byte TransactionType { get; set; }
        [DataMember]
        public string Ficheno { get; set; }
        [DataMember]
        public int SourceCardId { get; set; }
        [DataMember]
        public int DestCardId { get; set; }
        [DataMember]
        public decimal Total { get; set; }
        [DataMember]
        public int ExchangeId { get; set; }
        [DataMember]
        public byte Status { get; set; }
        [DataMember]
        public string Note { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    [Table("DocumentMaster")]
    public class DocumentMaster
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name_ { get; set; }
        [DataMember]
        public byte Lenth_ { get; set; }
        [DataMember]
        public int Value_ { get; set; }
        [DataMember]
        public bool Category1 { get; set; }
        [DataMember]
        public bool Category2 { get; set; }
        [DataMember]
        public bool Category3 { get; set; }
        [DataMember]
        public bool Category4 { get; set; }
        [DataMember]
        public byte CardType { get; set; }
        [DataMember]
        public byte PriceType { get; set; }
    }

    [DataContract]
    [Table("DataPermission")]
    public class DataPermission
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public byte SourceType { get; set; }
        [DataMember]
        public int SourceId { get; set; }
        [DataMember]
        public byte PermissionType { get; set; }
        [DataMember]
        public int PermissionId { get; set; }
        [DataMember]
        public bool Level1 { get; set; }
        [DataMember]
        public bool Level2 { get; set; }
        [DataMember]
        public bool Level3 { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    [Table("EnumMaster")]
    public class EnumMaster
    {
        [DataMember]
        public byte TypeId { get; set; }
        [DataMember]
        public string TypeDefinition { get; set; }
        [DataMember]
        public byte Key_ { get; set; }
        [DataMember]
        public string Name_ { get; set; }
        [DataMember]
        public bool AllowsUpdate { get; set; }
    }

    [DataContract]
    [Table("ExchangeByDate")]
    public class ExchangeByDate
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public byte ExchangeId { get; set; }
        [DataMember]
        public DateTime Date_ { get; set; }
        [DataMember]
        public decimal ExchangeRate { get; set; }
    }

    [DataContract]
    [Table("ExchangeMaster")]
    public class ExchangeMaster
    {
        [DataMember]
        [Key]
        public byte Id { get; set; }
        [DataMember]
        public string Name_ { get; set; }
        [DataMember]
        public bool IsMainExchange { get; set; }
        [DataMember]
        public bool IsSecondaryExchange { get; set; }
    }

    [DataContract]
    [Table("FicheLine")]
    public class FicheLine
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public int FicheId { get; set; }
        [DataMember]
        public byte ItemTypeId { get; set; }
        [DataMember]
        public int ItemId { get; set; }
        [DataMember]
        public int LineNumber { get; set; }
        [DataMember]
        public string Note { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public decimal ShippedAmount { get; set; }
        [DataMember]
        public decimal Length_ { get; set; }
        [DataMember]
        public decimal Width_ { get; set; }
        [DataMember]
        public decimal Height_ { get; set; }
        [DataMember]
        public decimal Weight_ { get; set; }
        [DataMember]
        public decimal ItemPrice { get; set; }
        [DataMember]
        public decimal LinePrice { get; set; }
        [DataMember]
        public decimal LineTotal { get; set; }
        [DataMember]
        public decimal LineNetTotal { get; set; }
    }

    [DataContract]
    [Table("FicheMaster")]
    public class FicheMaster
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public byte DocTypeId { get; set; }
        [DataMember]
        public string Ficheno { get; set; }
        [DataMember]
        public string SourceDocument { get; set; }
        [DataMember]
        public int CardId { get; set; }
        [DataMember]
        public int SourceWarehouse { get; set; }
        [DataMember]
        public int DestinationWarehouse { get; set; }
        [DataMember]
        public byte ExchangeId { get; set; }
        [DataMember]
        public string Note1 { get; set; }
        [DataMember]
        public string Note2 { get; set; }
        [DataMember]
        public string Note3 { get; set; }
        [DataMember]
        public string Note4 { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public byte Status_ { get; set; }
        [DataMember]
        public int SourceFicheId { get; set; }
    }

    [DataContract]
    [Table("GridViewColumn")]
    public class GridViewColumn
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public int VIewId { get; set; }
        [DataMember]
        public string FieldName { get; set; }
        [DataMember]
        public string Caption { get; set; }
        [DataMember]
        public int VisibleIndex { get; set; }
        [DataMember]
        public bool Editable { get; set; }
        [DataMember]
        public bool ReadOnly_ { get; set; }
        [DataMember]
        public int Width_ { get; set; }
        [DataMember]
        public byte FilterPopupMode { get; set; }
        [DataMember]
        public byte SummaryType { get; set; }
        [DataMember]
        public byte HorzintalAlign { get; set; }
        [DataMember]
        public bool ShowInCustomization { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public string DisplayFormat { get; set; }
        [DataMember]
        public int PermissionId { get; set; }
    }

    [DataContract]
    [Table("GridViewMaster")]
    public class GridViewMaster
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name_ { get; set; }
        [DataMember]
        public bool ShowFooter { get; set; }
        [DataMember]
        public bool ShowGroupPanel { get; set; }
        [DataMember]
        public bool ShowSearchPanel { get; set; }
    }

    [DataContract]
    [Table("Item")]
    public class Item
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public byte ItemTypeId { get; set; }
        [DataMember]
        public string ItemCode { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string Group1 { get; set; }
        [DataMember]
        public string Group2 { get; set; }
        [DataMember]
        public string Group3 { get; set; }
        [DataMember]
        public string Group4 { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public decimal Length_ { get; set; }
        [DataMember]
        public decimal Width_ { get; set; }
        [DataMember]
        public decimal Height_ { get; set; }
        [DataMember]
        public decimal Weight_ { get; set; }
        [DataMember]
        public byte PriceCalcTypeId { get; set; }
        [DataMember]
        public string ShortcutKey { get; set; }
    }

    [DataContract]
    [Table("ItemPrice")]
    public class ItemPrice
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public byte PriceTypeId { get; set; }
        [DataMember]
        public int ItemId { get; set; }
        [DataMember]
        public int CardId { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public byte ExchangeId { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    [Table("LoginSession")]
    public class LoginSession
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public string Guid { get; set; }
    }

    [DataContract]
    [Table("Parameter")]
    public class Parameter
    {
        [DataMember]
        public string ParameterKey { get; set; }
        [DataMember]
        public string ParameterDescription { get; set; }
        [DataMember]
        public string ParameterValue { get; set; }
        [DataMember]
        public bool CanAdminChange { get; set; }
        [DataMember]
        public bool CanUserChange { get; set; }
    }

    [DataContract]
    [Table("PermissionDetail")]
    public class PermissionDetail
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public int PermissionId { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    [Table("PermissionMaster")]
    public class PermissionMaster
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ParentId { get; set; }
        [DataMember]
        public string Name_ { get; set; }
        [DataMember]
        public string KeyWord { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
    }

    [DataContract]
    [Table("PriceCalcType")]
    public class PriceCalcType
    {
        [DataMember]
        public byte Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

    

    [DataContract]
    [Table("VW_ItemPricesDefault")]
    public class VW_ItemPricesDefault
    {
        [DataMember]
        public int ItemId { get; set; }
        [DataMember]
        public string ItemTypeName { get; set; }
        [DataMember]
        public string ItemCode { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public decimal PurchasePrice { get; set; }
        [DataMember]
        public decimal SalePrice { get; set; }
    }

    [DataContract]
    [Table("WarehouseMaster")]
    public class WarehouseMaster
    {
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public string Name_ { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }






}
