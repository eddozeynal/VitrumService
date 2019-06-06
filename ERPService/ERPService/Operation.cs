using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ERPService
{
    [DataContract]
    public class Operation<T>
    {
        [DataMember]
        public bool Successful { get; set; }
        [DataMember]
        public T Value { get; set; }
        [DataMember]
        public string Fail { get; set; }
    }

}