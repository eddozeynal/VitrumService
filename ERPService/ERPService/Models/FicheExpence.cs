using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ERPService.Models
{
    [Table("FicheExpence")]
    [DataContract]
    public class FicheExpence
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int FicheId { get; set; }
        [DataMember]
        public int CardFicheLineId { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }
}