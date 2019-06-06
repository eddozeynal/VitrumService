using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("LoginSession")]
	public class LoginSession

    {
                    

    	[DataMember]
		[Key]
		public int Id { get; set; }

    	[DataMember]
		public int UserId { get; set; }

		public DateTime CreatedDate { get; set; }

    	[DataMember]
		public string Guid { get; set; }

    }
            }
            