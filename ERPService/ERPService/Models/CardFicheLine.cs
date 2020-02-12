using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ErpService.Models
{
    [Table("CardFicheLine")]
    [DataContract]
    public class CardFicheLine
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CardFicheId { get; set; }
        [DataMember]
        public int CardId { get; set; }
        [DataMember]
        public decimal Total { get; set; }
        [DataMember]
        public int CurrencyId { get; set; }
        [DataMember]
        public decimal CurrencyRate { get; set; }
        [DataMember]
        public decimal SignType { get; set; }
    }
}
