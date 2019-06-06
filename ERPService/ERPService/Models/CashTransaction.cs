using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("CashTransaction")]
	public class CashTransaction

    {
                    

    	[DataMember]
		[Key]
		public int Id { get; set; }

    	[DataMember]
		public byte CashTypeId { get; set; }

    	[DataMember]
		public string Ficheno { get; set; }

    	[DataMember]
		public int SourceCardId { get; set; }

    	[DataMember]
		public int DestCardId { get; set; }

    	[DataMember]
		public decimal Total { get; set; }

    	[DataMember]
		public byte CurrencyId { get; set; }

    	[DataMember]
		public decimal CurrencyRate { get; set; }

    	[DataMember]
		public byte StatusId { get; set; }

    	[DataMember]
		public string Note { get; set; }

    	[DataMember]
		public int CreatedBy { get; set; }

    	[DataMember]
		public DateTime CreatedDate { get; set; }

    	[DataMember]
		public int ConnectedFicheId { get; set; }

    }
            }
            