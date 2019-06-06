using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace ERPService.Models
{
	[DataContract]
	[Table("[User]")]
	public class User

    {
                    

    	[DataMember]
		[Key]
		public int Id { get; set; }

    	[DataMember]
		public string Login { get; set; }

    	[DataMember]
		public string UserName { get; set; }

    	[DataMember]
		public string PassHash { get; set; }

    	[DataMember]
		public bool IsActive { get; set; }

    	[DataMember]
		public bool IsAdmin { get; set; }

    }
            }
            