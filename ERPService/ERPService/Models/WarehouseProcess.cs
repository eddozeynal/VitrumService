using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("WarehouseProcess")]
	public class WarehouseProcess

    {
                    
        [ExplicitKey]
    	[DataMember]
		public int Id { get; set; }

    	[DataMember]
		public byte StatusId { get; set; }

    	[DataMember]
		public int CreatedBy { get; set; }

    	[DataMember]
		public DateTime CreatedDate { get; set; }

    	[DataMember]
		public DateTime? ProcessBeginDate { get; set; }

    	[DataMember]
		public DateTime? ProcessEndDate { get; set; }

    }
            }
            