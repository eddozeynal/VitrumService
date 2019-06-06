using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("CurrencyByDate")]
	public class CurrencyByDate

    {
                    

    	[DataMember]
		[Key]
		public int Id { get; set; }

    	[DataMember]
		public byte CurrencyId { get; set; }

    	[DataMember]
		public DateTime Date { get; set; }

    	[DataMember]
		public decimal Rate { get; set; }

    }
            }
            