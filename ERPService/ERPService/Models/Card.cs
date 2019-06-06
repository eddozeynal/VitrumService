using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("Card")]
	public class Card

    {
                    

    	[DataMember]
		[Key]
		public int Id { get; set; }

    	[DataMember]
		public byte CardTypeId { get; set; }

    	[DataMember]
		public string CardNumber { get; set; }

    	[DataMember]
		public string CardName { get; set; }

    	[DataMember]
		public decimal DebtLimit { get; set; }

    	[DataMember]
		public byte CurrencyId { get; set; }

    	[DataMember]
		public string TaxCode { get; set; }

    	[DataMember]
		public string LocationAddress { get; set; }

    	[DataMember]
		public string Phone1 { get; set; }

    	[DataMember]
		public string Phone2 { get; set; }

    	[DataMember]
		public string Phone3 { get; set; }

    	[DataMember]
		public string Country { get; set; }

    	[DataMember]
		public string City { get; set; }

    	[DataMember]
		public string Town { get; set; }

    	[DataMember]
		public string District { get; set; }

    	[DataMember]
		public float Latitude { get; set; }

    	[DataMember]
		public float Longitude { get; set; }

    	[DataMember]
		public bool IsActive { get; set; }

    	[DataMember]
		public int CreatedBy { get; set; }

    	[DataMember]
		public DateTime CreatedDate { get; set; }

    }
            }
            