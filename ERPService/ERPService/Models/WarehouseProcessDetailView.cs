using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("WarehouseProcessDetailView")]
	public class WarehouseProcessDetailView

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
        [DataMember]
        public string ItemCode { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string CompletedByUserName { get; set; }
    }
}
            