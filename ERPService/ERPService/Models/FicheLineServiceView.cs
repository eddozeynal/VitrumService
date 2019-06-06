using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("FicheLineServiceView")]
	public class FicheLineServiceView
            
    {
                    

    	[DataMember]
		[Key]
		public int Id { get; set; }

    	[DataMember]
		public int FicheLineId { get; set; }

    	[DataMember]
		public int ServiceItemId { get; set; }

    	[DataMember]
		public decimal Quantity { get; set; }

    	[DataMember]
		public decimal ItemPrice { get; set; }

    	[DataMember]
		public decimal LinePrice { get; set; }

    	[DataMember]
		public decimal LineTotal { get; set; }

    	[DataMember]
		public decimal LineDiscount { get; set; }

    	[DataMember]
		public decimal LineNet { get; set; }

        [DataMember]
        public string ItemCode { get; set; }
        [DataMember]
        public string ItemName { get; set; }

    }
            }
            