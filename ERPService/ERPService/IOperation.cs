using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ERPService
{
    public interface IOperation<T>
    {
        [DataMember]
        bool Successful { get; set; }
        [DataMember]
        T Value { get; set; }
        [DataMember]
        string Fail { get; set; }
    }
}
