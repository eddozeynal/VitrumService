using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("WorkStateView")]
	public class WorkStateView

    {
                    

    	[DataMember]
		public int Id { get; set; }

    	[DataMember]
		public int ParentId { get; set; }

    	[DataMember]
		public byte JobId { get; set; }

    	[DataMember]
		public int PersonnelId { get; set; }

    	[DataMember]
		public string JobName { get; set; }

    	[DataMember]
		public string PersonnelName { get; set; }

    }
            }
            