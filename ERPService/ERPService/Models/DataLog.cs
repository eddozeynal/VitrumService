using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("DataLog")]
	public class DataLog

    {
                    

    	[DataMember]
		[Key]
		public int Id { get; set; }

    	[DataMember]
		public DateTime LogDate { get; set; }

    	[DataMember]
		public byte LogDataType { get; set; }

    	[DataMember]
		public int LogDataId { get; set; }

    	[DataMember]
		public int DoneBy { get; set; }

    	[DataMember]
		public string Data { get; set; }

    }
            }
            