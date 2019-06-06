using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("DocumentStatusType")]
	public class DocumentStatusType

    {
                    

    	[DataMember]
		public byte Id { get; set; }

    	[DataMember]
		public byte DocMasterId { get; set; }

    	[DataMember]
		public string StatusName { get; set; }

    }
            }
            