using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("PermissionDetail")]
	public class PermissionDetail

    {
                    

    	[DataMember]
		[Key]
		public int Id { get; set; }

    	[DataMember]
		public int PermissionId { get; set; }

    	[DataMember]
		public int UserId { get; set; }

    	[DataMember]
		public int CreatedBy { get; set; }

    	[DataMember]
		public DateTime CreatedDate { get; set; }

    }
            }
            