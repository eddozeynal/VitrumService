using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("ItemView")]
	public class ItemView

    {
                    

    	[DataMember]
		public int Id { get; set; }

    	[DataMember]
		public byte ItemTypeId { get; set; }

    	[DataMember]
		public string ItemCode { get; set; }

    	[DataMember]
		public string ItemName { get; set; }

    	[DataMember]
		public string Group1 { get; set; }

    	[DataMember]
		public string Group2 { get; set; }

    	[DataMember]
		public string Group3 { get; set; }

    	[DataMember]
		public string Group4 { get; set; }

    	[DataMember]
		public int CreatedBy { get; set; }

    	[DataMember]
		public DateTime CreatedDate { get; set; }

    	[DataMember]
		public decimal Length { get; set; }

    	[DataMember]
		public decimal Width { get; set; }

    	[DataMember]
		public byte LineCalcTypeId { get; set; }

    	[DataMember]
		public string ItemTypeName { get; set; }

    	[DataMember]
		public decimal DefaultSalePrice { get; set; }

    	[DataMember]
		public decimal DefaultPurchasePrice { get; set; }

        [DataMember]
        public bool IsLineService { get; set; }

    }
}
            