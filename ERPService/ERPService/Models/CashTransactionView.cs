using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("CashTransactionView")]
	public class CashTransactionView

    {
                    

    	[DataMember]
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
		public byte CurrecyId { get; set; }

    	[DataMember]
		public decimal CurrecyRate { get; set; }

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

    	[DataMember]
		public string CashTypeName { get; set; }

    	[DataMember]
		public string CurrencyName { get; set; }

    	[DataMember]
		public string UserName { get; set; }

    	[DataMember]
		public string StatusName { get; set; }

    	[DataMember]
		public bool SourceCardByPermission { get; set; }

    	[DataMember]
		public string SourceCardNumber { get; set; }

    	[DataMember]
		public string SourceCardName { get; set; }

    	[DataMember]
		public bool DestCardByPermission { get; set; }

    	[DataMember]
		public string DestCardNumber { get; set; }

    	[DataMember]
		public string DestCardName { get; set; }

    	[DataMember]
		public string ConnectedInvoice { get; set; }

        [DataMember]
        [Write(false)]
        public decimal DmTotal { get; set; }
    }
            }
            