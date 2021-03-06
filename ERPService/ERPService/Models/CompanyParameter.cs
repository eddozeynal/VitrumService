using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("CompanyParameter")]
	public class CompanyParameter

    {
                    

    	[DataMember]
		public string ParameterKey { get; set; }

    	[DataMember]
		public string ParameterDescription { get; set; }

    	[DataMember]
		public string ParameterValue { get; set; }

    	[DataMember]
		public bool CanAdminChange { get; set; }

    	[DataMember]
		public bool CanUserChange { get; set; }

    	[DataMember]
		public bool Visible { get; set; }

    }
            }
            