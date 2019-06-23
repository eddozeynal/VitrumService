using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ERPService.ViewModels
{
    [DataContract]
    public class CardTotalByIntervalView 
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public byte CardTypeId { get; set; }
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
        [DataMember]
        public decimal RemDebitByBegDate { get; set; }
        [DataMember]
        public decimal RemCreditByBegDate { get; set; }
        [DataMember]
        public decimal RemDebitByEndDate { get; set; }
        [DataMember]
        public decimal RemCreditByEndDate { get; set; }
    }
}