using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("ItemPriceType")]
	public class ItemPriceType

    {
                    

    	[DataMember]
		public byte Id { get; set; }

    	[DataMember]
		public string PriceTypeName { get; set; }

    }
            }
            