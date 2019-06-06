using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ERPService.Models
{
    [DataContract]
    [Table("ExchangeTransactionView")]
    public class ExchangeTransactionView
    {
        [DataMember]
        [Key]
        public int Id { get; set; }

        [DataMember]
        public string Ficheno { get; set; }

        [DataMember]
        public int SourceCardId { get; set; }

        [DataMember]
        public int DestCardId { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public decimal CurrencyRate { get; set; }

        [DataMember]
        public string Note { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public bool SourceCardByPermission { get; set; }

        [DataMember]
        public string SourceCardNumber { get; set; }

        [DataMember]
        public string SourceCardName { get; set; }

        [DataMember]
        public bool DestCardByPermission { get; set; }

        [DataMember]
        public string DestCardNumber { get; set; }

        [DataMember]
        public string DestCardName { get; set; }

        [DataMember]
        public decimal SourceTotal { get; set; }

        [DataMember]
        public decimal DestTotal { get; set; }

        [DataMember]
        public string UserName { get; set; }
    }
}