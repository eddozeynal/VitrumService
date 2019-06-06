using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("Warehouse")]
	public class Warehouse

    {
                    
        [ExplicitKey]
    	[DataMember]
		public int Number { get; set; }

    	[DataMember]
		public string WarehouseName { get; set; }

    	[DataMember]
		public bool IsActive { get; set; }

    	[DataMember]
		public int CreatedBy { get; set; }

    	[DataMember]
		public DateTime CreatedDate { get; set; }

    }
            }
            