using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("Currency")]
	public class Currency

    {
                    

    	[DataMember]
		[Key]
		public byte Id { get; set; }

    	[DataMember]
		public string CurrencyName { get; set; }

    }
            }
            