using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("PersonnelJobMaster")]
	public class PersonnelJobMaster

    {
                    

    	[DataMember]
		public byte Id { get; set; }

    	[DataMember]
		public string JobName { get; set; }

    }
            }
            