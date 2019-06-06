using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("DocumentMaster")]
	public class DocumentMaster

    {
                    

    	[DataMember]
		public byte Id { get; set; }

    	[DataMember]
		public string Name { get; set; }

    	[DataMember]
		public byte Lenth { get; set; }

    	[DataMember]
		public int Value { get; set; }

    	[DataMember]
		public bool Category1 { get; set; }

    	[DataMember]
		public bool Category2 { get; set; }

    	[DataMember]
		public bool Category3 { get; set; }

    	[DataMember]
		public bool Category4 { get; set; }

    	[DataMember]
		public string Prefix { get; set; }

    }
            }
            