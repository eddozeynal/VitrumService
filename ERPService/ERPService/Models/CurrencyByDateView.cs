using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ERPService.Models
{
    [DataContract]
    [Table("CurrencyByDateView")]
    public class CurrencyByDateView
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public byte CurrencyId { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public decimal Rate { get; set; }
        [DataMember]
        public string CurrencyName { get; set; }
    }
}