using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ERPService
{
    [DataContract]
    public class TestClass
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime Date_ { get; set; }
    }
}