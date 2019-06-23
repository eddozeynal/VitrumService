using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("FicheReportLineView")]
	public class FicheReportLineView

    {
                    

    	[DataMember]
		public int Id { get; set; }

    	[DataMember]
		public int FicheId { get; set; }

    	[DataMember]
		public byte ItemTypeId { get; set; }

    	[DataMember]
		public int ItemId { get; set; }

    	[DataMember]
		public int LineNumber { get; set; }

    	[DataMember]
		public string Note { get; set; }

    	[DataMember]
		public decimal Quantity { get; set; }

    	[DataMember]
		public decimal ShippedQuantity { get; set; }

    	[DataMember]
		public decimal Length { get; set; }

    	[DataMember]
		public decimal Width { get; set; }

    	[DataMember]
		public decimal ItemPrice { get; set; }

    	[DataMember]
		public decimal LinePrice { get; set; }

    	[DataMember]
		public decimal LineTotal { get; set; }

    	[DataMember]
		public decimal LineDiscount { get; set; }

    	[DataMember]
		public decimal LineNetTotal { get; set; }

    	[DataMember]
		public decimal LineExpense { get; set; }

    	[DataMember]
		public decimal LineTotalAcc { get; set; }

    	[DataMember]
		public bool IsCustomerItem { get; set; }

    	[DataMember]
		public bool IsSketched { get; set; }

    	[DataMember]
		public bool IsTemplated { get; set; }

    	[DataMember]
		public string ItemName { get; set; }

    	[DataMember]
		public string ItemCode { get; set; }

    	[DataMember]
		public string ItemTypeName { get; set; }

    	[DataMember]
		public decimal ServiceLineNetTotalSum { get; set; }

    	[DataMember]
		public decimal? LineFinalSum { get; set; }

    	[DataMember]
		public DateTime CreatedDate { get; set; }

    	[DataMember]
		public decimal CurrencyRate { get; set; }

        [DataMember]
        public string Ficheno { get; set; }
    }
            }
            