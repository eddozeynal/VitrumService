using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("WorkState")]
	public class WorkState

    {
                    

    	[DataMember]
		[Key]
		public int Id { get; set; }

    	[DataMember]
		public int ParentId { get; set; }

    	[DataMember]
		public byte JobId { get; set; }

    	[DataMember]
		public int PersonnelId { get; set; }

    }
            }
            