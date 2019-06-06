using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("PermissionMaster")]
	public class PermissionMaster

    {
                    

    	[DataMember]
		public int Id { get; set; }

    	[DataMember]
		public int ParentId { get; set; }

    	[DataMember]
		public string Name { get; set; }

    	[DataMember]
		public string KeyWord { get; set; }

    	[DataMember]
		public bool IsActive { get; set; }

    }
            }
            