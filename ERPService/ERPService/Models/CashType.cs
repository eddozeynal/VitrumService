using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("CashType")]
	public class CashType

    {
                    

    	[DataMember]
		public byte Id { get; set; }

    	[DataMember]
		public string CashTypeName { get; set; }

    }
            }
            