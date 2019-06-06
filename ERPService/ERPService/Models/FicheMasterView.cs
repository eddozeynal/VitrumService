using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("FicheMasterView")]
	public class FicheMasterView

    {
                    

    	[DataMember]
		public int Id { get; set; }

    	[DataMember]
		public byte DocTypeId { get; set; }

    	[DataMember]
		public string Ficheno { get; set; }

    	[DataMember]
		public string SourceDocument { get; set; }

    	[DataMember]
		public int CardId { get; set; }

    	[DataMember]
		public int SourceWarehouse { get; set; }

    	[DataMember]
		public int DestinationWarehouse { get; set; }

    	[DataMember]
		public byte CurrencyId { get; set; }

    	[DataMember]
		public decimal CurrencyRate { get; set; }

    	[DataMember]
		public string Note1 { get; set; }

    	[DataMember]
		public string Note2 { get; set; }

    	[DataMember]
		public string Note3 { get; set; }

    	[DataMember]
		public string Note4 { get; set; }

    	[DataMember]
		public int CreatedBy { get; set; }

    	[DataMember]
		public DateTime CreatedDate { get; set; }

    	[DataMember]
		public byte StatusId { get; set; }

    	[DataMember]
		public int SourceFicheId { get; set; }

    	[DataMember]
		public decimal LinesTotal { get; set; }

    	[DataMember]
		public decimal LineDiscountsTotal { get; set; }

    	[DataMember]
		public decimal LinesNetTotal { get; set; }

    	[DataMember]
		public decimal FicheDiscount { get; set; }

    	[DataMember]
		public decimal FicheServicesNetTotal { get; set; }

    	[DataMember]
		public decimal FicheNetTotal { get; set; }

    	[DataMember]
		public decimal LineExpensesTotal { get; set; }

    	[DataMember]
		public decimal FicheTotalAcc { get; set; }

    	[DataMember]
		public string CurrencyName { get; set; }

    	[DataMember]
		public string CardNumber { get; set; }

    	[DataMember]
		public string CardName { get; set; }

    	[DataMember]
		public string StatusName { get; set; }

    }
            }
            