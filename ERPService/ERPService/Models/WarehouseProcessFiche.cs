using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("WarehouseProcessFiche")]
	public class WarehouseProcessFiche

    {
                    

    	[DataMember]
		[Key]
		public int Id { get; set; }

    	[DataMember]
		public int ProcessId { get; set; }

    	[DataMember]
		public int FicheId { get; set; }

    }
            }
            