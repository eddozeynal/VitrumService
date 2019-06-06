using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
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
		public int CreatedBy { get; set; }

    	[DataMember]
		public DateTime CreatedDate { get; set; }

    }
            }
            