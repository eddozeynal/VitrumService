using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("LineCalcType")]
	public class LineCalcType

    {
                    

    	[DataMember]
		public byte Id { get; set; }

    	[DataMember]
		public string Name { get; set; }

    }
            }
            