using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("ItemType")]
	public class ItemType

    {
                    

    	[DataMember]
		public byte Id { get; set; }

    	[DataMember]
		public string ItemTypeName { get; set; }

    }
            }
            