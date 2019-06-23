using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("WarehouseProcessDetail")]
	public class WarehouseProcessDetail

    {
                    

    	[DataMember]
		[Key]
		public int Id { get; set; }

    	[DataMember]
		public int ProcessId { get; set; }

    	[DataMember]
		public int ServiceItemId { get; set; }

    	[DataMember]
		public bool IsCompleted { get; set; }

    	[DataMember]
		public int? CompletedBy { get; set; }

    	[DataMember]
		public DateTime? CompletedDate { get; set; }

    }
            }
            