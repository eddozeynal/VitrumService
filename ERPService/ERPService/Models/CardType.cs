using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("CardType")]
	public class CardType

    {
                    

    	[DataMember]
		public byte Id { get; set; }

    	[DataMember]
		public string CardTypeName { get; set; }

    	[DataMember]
		public bool ByPermission { get; set; }

    	[DataMember]
		public bool RemainingCalculated { get; set; }

    	[DataMember]
		public bool TransactionCalculated { get; set; }

    }
            }
            