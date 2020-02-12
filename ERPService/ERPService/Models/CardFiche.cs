
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ErpService.Models
{
    [Table("CardFiche")]
    [DataContract]
    public class CardFiche
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public byte CashTypeId { get; set; }
        [DataMember]
        public string Ficheno { get; set; }
        [DataMember]
        public byte StatusId { get; set; }
        [DataMember]
        public string Note { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        [Write(false)]
        public List<CardFicheLine> Lines { get; set; }
    }
}
