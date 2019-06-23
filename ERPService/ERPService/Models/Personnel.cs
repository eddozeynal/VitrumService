using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("Personnel")]
	public class Personnel

    {
                    

    	[DataMember]
		[Key]
		public int Id { get; set; }

    	[DataMember]
		public string PersonnelName { get; set; }

    	[DataMember]
		public DateTime WorkBeginDate { get; set; }

    	[DataMember]
		public bool IsActive { get; set; }

    }
            }
            